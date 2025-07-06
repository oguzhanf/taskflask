namespace TaskFlask.Models
{
    /// <summary>
    /// Represents a cloud storage service
    /// </summary>
    public enum CloudServiceType
    {
        OneDrive,
        GoogleDrive,
        Dropbox
    }

    /// <summary>
    /// Represents information about a detected cloud service
    /// </summary>
    public class CloudService
    {
        public CloudServiceType Type { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SyncFolderPath { get; set; } = string.Empty;
        public bool IsInstalled { get; set; }
        public bool IsRunning { get; set; }
        public string DisplayName => $"{Name} ({SyncFolderPath})";

        public CloudService(CloudServiceType type, string name, string syncFolderPath, bool isInstalled, bool isRunning = false)
        {
            Type = type;
            Name = name;
            SyncFolderPath = syncFolderPath;
            IsInstalled = isInstalled;
            IsRunning = isRunning;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
