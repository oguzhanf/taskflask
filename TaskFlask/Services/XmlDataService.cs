using System.Xml;
using System.Xml.Serialization;
using TaskFlask.Models;

namespace TaskFlask.Services
{
    /// <summary>
    /// Service for handling XML serialization and deserialization of task data
    /// </summary>
    public class XmlDataService
    {
        private readonly string _filePath;
        private readonly XmlSerializer _serializer;

        public string FilePath => _filePath;

        public XmlDataService(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _serializer = new XmlSerializer(typeof(TaskCollection));
        }

        /// <summary>
        /// Loads task collection from XML file
        /// </summary>
        /// <returns>TaskCollection loaded from file, or new empty collection if file doesn't exist</returns>
        public async Task<TaskCollection> LoadTasksAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    // Create new empty collection if file doesn't exist
                    var newCollection = new TaskCollection();
                    await SaveTasksAsync(newCollection);
                    return newCollection;
                }

                using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                var collection = (TaskCollection?)_serializer.Deserialize(fileStream);
                return collection ?? new TaskCollection();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to load tasks from {_filePath}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Saves task collection to XML file
        /// </summary>
        /// <param name="taskCollection">Collection to save</param>
        public async Task SaveTasksAsync(TaskCollection taskCollection)
        {
            if (taskCollection == null)
                throw new ArgumentNullException(nameof(taskCollection));

            try
            {
                // Ensure directory exists
                var directory = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Create backup of existing file
                await CreateBackupAsync();

                // Write to temporary file first
                var tempFilePath = _filePath + ".tmp";
                
                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = Environment.NewLine,
                    Encoding = System.Text.Encoding.UTF8
                };

                using (var writer = XmlWriter.Create(tempFilePath, settings))
                {
                    _serializer.Serialize(writer, taskCollection);
                }

                // Replace original file with temporary file
                if (File.Exists(_filePath))
                {
                    File.Delete(_filePath);
                }
                File.Move(tempFilePath, _filePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save tasks to {_filePath}: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Creates a backup of the current data file
        /// </summary>
        private async Task CreateBackupAsync()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var backupPath = _filePath + ".backup";
                    await Task.Run(() => File.Copy(_filePath, backupPath, true));
                }
            }
            catch (Exception ex)
            {
                // Log but don't fail the save operation
                System.Diagnostics.Debug.WriteLine($"Failed to create backup: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates if the XML file is valid and can be loaded
        /// </summary>
        /// <returns>True if file is valid, false otherwise</returns>
        public bool ValidateFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return false;

                using var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
                var collection = (TaskCollection?)_serializer.Deserialize(fileStream);
                return collection != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Exports tasks to a different file path
        /// </summary>
        /// <param name="exportPath">Path to export to</param>
        /// <param name="taskCollection">Collection to export</param>
        public async Task ExportTasksAsync(string exportPath, TaskCollection taskCollection)
        {
            if (string.IsNullOrEmpty(exportPath))
                throw new ArgumentNullException(nameof(exportPath));
            
            if (taskCollection == null)
                throw new ArgumentNullException(nameof(taskCollection));

            var exportService = new XmlDataService(exportPath);
            await exportService.SaveTasksAsync(taskCollection);
        }

        /// <summary>
        /// Gets file information
        /// </summary>
        /// <returns>FileInfo object or null if file doesn't exist</returns>
        public FileInfo? GetFileInfo()
        {
            return File.Exists(_filePath) ? new FileInfo(_filePath) : null;
        }
    }
}
