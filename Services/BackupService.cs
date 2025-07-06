using TaskFlask.Models;

namespace TaskFlask.Services
{
    /// <summary>
    /// Service for creating and managing backups of task data
    /// </summary>
    public class BackupService
    {
        private readonly string _dataFilePath;
        private readonly string _backupDirectory;

        public BackupService(string dataFilePath)
        {
            _dataFilePath = dataFilePath ?? throw new ArgumentNullException(nameof(dataFilePath));
            
            var dataDirectory = Path.GetDirectoryName(_dataFilePath);
            _backupDirectory = Path.Combine(dataDirectory ?? "", "Backups");
            
            if (!Directory.Exists(_backupDirectory))
            {
                Directory.CreateDirectory(_backupDirectory);
            }
        }

        /// <summary>
        /// Creates a backup of the current data file
        /// </summary>
        /// <param name="backupType">Type of backup (daily, manual, etc.)</param>
        /// <returns>Path to the created backup file</returns>
        public async Task<string> CreateBackupAsync(BackupType backupType = BackupType.Manual)
        {
            if (!File.Exists(_dataFilePath))
            {
                throw new FileNotFoundException("Data file not found for backup", _dataFilePath);
            }

            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            var backupFileName = $"TaskFlask_backup_{backupType}_{timestamp}.xml";
            var backupFilePath = Path.Combine(_backupDirectory, backupFileName);

            await Task.Run(() => File.Copy(_dataFilePath, backupFilePath, true));

            // Clean up old backups based on type
            await CleanupOldBackupsAsync(backupType);

            return backupFilePath;
        }

        /// <summary>
        /// Restores data from a backup file
        /// </summary>
        /// <param name="backupFilePath">Path to the backup file</param>
        /// <returns>True if restore was successful</returns>
        public async Task<bool> RestoreFromBackupAsync(string backupFilePath)
        {
            if (!File.Exists(backupFilePath))
            {
                throw new FileNotFoundException("Backup file not found", backupFilePath);
            }

            try
            {
                // Validate the backup file first
                var xmlService = new XmlDataService(backupFilePath);
                if (!xmlService.ValidateFile())
                {
                    throw new InvalidOperationException("Backup file is corrupted or invalid");
                }

                // Create a backup of current file before restoring
                if (File.Exists(_dataFilePath))
                {
                    var currentBackupPath = _dataFilePath + ".before_restore_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    await Task.Run(() => File.Copy(_dataFilePath, currentBackupPath, true));
                }

                // Restore from backup
                await Task.Run(() => File.Copy(backupFilePath, _dataFilePath, true));
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a list of available backup files
        /// </summary>
        /// <returns>List of backup file information</returns>
        public List<BackupInfo> GetAvailableBackups()
        {
            if (!Directory.Exists(_backupDirectory))
            {
                return new List<BackupInfo>();
            }

            var backupFiles = Directory.GetFiles(_backupDirectory, "TaskFlask_backup_*.xml");
            var backups = new List<BackupInfo>();

            foreach (var file in backupFiles)
            {
                try
                {
                    var fileName = Path.GetFileNameWithoutExtension(file);
                    var parts = fileName.Split('_');
                    
                    if (parts.Length >= 4)
                    {
                        var backupTypeStr = parts[2];
                        var timestampStr = parts[3];
                        
                        if (Enum.TryParse<BackupType>(backupTypeStr, true, out var backupType) &&
                            DateTime.TryParseExact(timestampStr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var date))
                        {
                            var fileInfo = new FileInfo(file);
                            backups.Add(new BackupInfo
                            {
                                FilePath = file,
                                BackupType = backupType,
                                CreatedDate = date,
                                FileSize = fileInfo.Length,
                                DisplayName = $"{backupType} - {date:MMM dd, yyyy} ({FormatFileSize(fileInfo.Length)})"
                            });
                        }
                    }
                }
                catch
                {
                    // Skip invalid backup files
                }
            }

            return backups.OrderByDescending(b => b.CreatedDate).ToList();
        }

        /// <summary>
        /// Cleans up old backup files based on retention policy
        /// </summary>
        /// <param name="backupType">Type of backup to clean up</param>
        private async Task CleanupOldBackupsAsync(BackupType backupType)
        {
            await Task.Run(() =>
            {
                var backups = GetAvailableBackups()
                    .Where(b => b.BackupType == backupType)
                    .OrderByDescending(b => b.CreatedDate)
                    .ToList();

                int maxBackups = backupType switch
                {
                    BackupType.Daily => 7,    // Keep 7 daily backups
                    BackupType.Manual => 10,  // Keep 10 manual backups
                    BackupType.Auto => 5,     // Keep 5 auto backups
                    _ => 5
                };

                var backupsToDelete = backups.Skip(maxBackups);
                foreach (var backup in backupsToDelete)
                {
                    try
                    {
                        File.Delete(backup.FilePath);
                    }
                    catch
                    {
                        // Ignore deletion errors
                    }
                }
            });
        }

        /// <summary>
        /// Formats file size for display
        /// </summary>
        private static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }
    }

    /// <summary>
    /// Types of backups
    /// </summary>
    public enum BackupType
    {
        Manual,
        Daily,
        Auto
    }

    /// <summary>
    /// Information about a backup file
    /// </summary>
    public class BackupInfo
    {
        public string FilePath { get; set; } = string.Empty;
        public BackupType BackupType { get; set; }
        public DateTime CreatedDate { get; set; }
        public long FileSize { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}
