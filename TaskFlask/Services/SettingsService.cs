using System.Configuration;
using System.Text.Json;

namespace TaskFlask.Services
{
    /// <summary>
    /// Service for managing application settings
    /// </summary>
    public class SettingsService
    {
        private readonly string _settingsFilePath;
        private AppSettings _settings;

        public AppSettings Settings => _settings;

        public SettingsService()
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appFolder = Path.Combine(appDataPath, "TaskFlask");
            
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            _settingsFilePath = Path.Combine(appFolder, "settings.json");
            _settings = LoadSettings();
        }

        /// <summary>
        /// Loads settings from file or creates default settings
        /// </summary>
        private AppSettings LoadSettings()
        {
            try
            {
                if (File.Exists(_settingsFilePath))
                {
                    var json = File.ReadAllText(_settingsFilePath);
                    var settings = JsonSerializer.Deserialize<AppSettings>(json);
                    return settings ?? new AppSettings();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading settings: {ex.Message}");
            }

            return new AppSettings();
        }

        /// <summary>
        /// Saves current settings to file
        /// </summary>
        public async Task SaveSettingsAsync()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                var json = JsonSerializer.Serialize(_settings, options);
                await File.WriteAllTextAsync(_settingsFilePath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to save settings: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Updates the database file path
        /// </summary>
        public async Task SetDatabasePathAsync(string path)
        {
            _settings.DatabaseFilePath = path;
            _settings.IsFirstRun = false;
            await SaveSettingsAsync();
        }

        /// <summary>
        /// Marks the first run as completed
        /// </summary>
        public async Task CompleteFirstRunAsync()
        {
            _settings.IsFirstRun = false;
            await SaveSettingsAsync();
        }
    }

    /// <summary>
    /// Application settings model
    /// </summary>
    public class AppSettings
    {
        public string DatabaseFilePath { get; set; } = string.Empty;
        public bool IsFirstRun { get; set; } = true;
        public int WindowWidth { get; set; } = 1000;
        public int WindowHeight { get; set; } = 700;
        public int WindowX { get; set; } = -1;
        public int WindowY { get; set; } = -1;
        public bool WindowMaximized { get; set; } = false;
        public string LastSelectedFilter { get; set; } = "All";
        public string LastSortBy { get; set; } = "CreatedDate";
        public bool SortDescending { get; set; } = true;
        public bool ShowCompletedTasks { get; set; } = true;
        public bool AutoSave { get; set; } = true;
        public int AutoSaveIntervalMinutes { get; set; } = 5;
        public string Theme { get; set; } = "System";
        public List<string> RecentCategories { get; set; } = new();
        public DateTime LastBackupDate { get; set; } = DateTime.MinValue;
        public bool CreateDailyBackups { get; set; } = true;
        public int MaxBackupFiles { get; set; } = 7;
    }
}
