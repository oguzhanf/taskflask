using Microsoft.Win32;
using System.Diagnostics;
using TaskFlask.Models;

namespace TaskFlask.Services
{
    /// <summary>
    /// Service for detecting installed cloud storage services
    /// </summary>
    public class CloudServiceDetector
    {
        /// <summary>
        /// Detects all available cloud services in priority order
        /// </summary>
        /// <returns>List of detected cloud services ordered by priority</returns>
        public List<CloudService> DetectCloudServices()
        {
            var services = new List<CloudService>();

            // Check in priority order: OneDrive, Google Drive, Dropbox
            var oneDrive = DetectOneDrive();
            if (oneDrive != null) services.Add(oneDrive);

            var googleDrive = DetectGoogleDrive();
            if (googleDrive != null) services.Add(googleDrive);

            var dropbox = DetectDropbox();
            if (dropbox != null) services.Add(dropbox);

            return services;
        }

        /// <summary>
        /// Detects OneDrive installation and sync folder
        /// </summary>
        private CloudService? DetectOneDrive()
        {
            try
            {
                // Check for OneDrive in registry
                using var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\OneDrive");
                if (key != null)
                {
                    var userFolder = key.GetValue("UserFolder") as string;
                    if (!string.IsNullOrEmpty(userFolder) && Directory.Exists(userFolder))
                    {
                        bool isRunning = IsProcessRunning("OneDrive");
                        return new CloudService(CloudServiceType.OneDrive, "OneDrive", userFolder, true, isRunning);
                    }
                }

                // Fallback: Check default OneDrive location
                var defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "OneDrive");
                if (Directory.Exists(defaultPath))
                {
                    bool isRunning = IsProcessRunning("OneDrive");
                    return new CloudService(CloudServiceType.OneDrive, "OneDrive", defaultPath, true, isRunning);
                }
            }
            catch (Exception ex)
            {
                // Log error but continue
                Debug.WriteLine($"Error detecting OneDrive: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Detects Google Drive installation and sync folder
        /// </summary>
        private CloudService? DetectGoogleDrive()
        {
            try
            {
                // Check for Google Drive in registry
                using var key = Registry.CurrentUser.OpenSubKey(@"Software\Google\Drive");
                if (key != null)
                {
                    var syncRootFolder = key.GetValue("SyncRootFolder") as string;
                    if (!string.IsNullOrEmpty(syncRootFolder) && Directory.Exists(syncRootFolder))
                    {
                        bool isRunning = IsProcessRunning("GoogleDriveFS");
                        return new CloudService(CloudServiceType.GoogleDrive, "Google Drive", syncRootFolder, true, isRunning);
                    }
                }

                // Check for Google Drive File Stream
                var fileStreamPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google", "Drive File Stream");
                if (Directory.Exists(fileStreamPath))
                {
                    // Look for mounted drive
                    var drives = DriveInfo.GetDrives();
                    foreach (var drive in drives)
                    {
                        if (drive.DriveType == DriveType.Network && drive.IsReady)
                        {
                            try
                            {
                                if (drive.VolumeLabel.Contains("Google", StringComparison.OrdinalIgnoreCase))
                                {
                                    bool isRunning = IsProcessRunning("GoogleDriveFS");
                                    return new CloudService(CloudServiceType.GoogleDrive, "Google Drive", drive.RootDirectory.FullName, true, isRunning);
                                }
                            }
                            catch
                            {
                                // Continue checking other drives
                            }
                        }
                    }
                }

                // Fallback: Check default Google Drive location
                var defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Google Drive");
                if (Directory.Exists(defaultPath))
                {
                    bool isRunning = IsProcessRunning("GoogleDriveFS");
                    return new CloudService(CloudServiceType.GoogleDrive, "Google Drive", defaultPath, true, isRunning);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error detecting Google Drive: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Detects Dropbox installation and sync folder
        /// </summary>
        private CloudService? DetectDropbox()
        {
            try
            {
                // Check Dropbox info.json file
                var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var dropboxInfoPath = Path.Combine(appDataPath, "Dropbox", "info.json");
                
                if (File.Exists(dropboxInfoPath))
                {
                    var infoContent = File.ReadAllText(dropboxInfoPath);
                    // Simple JSON parsing for path (avoiding external dependencies)
                    var pathStart = infoContent.IndexOf("\"path\":");
                    if (pathStart != -1)
                    {
                        var pathValueStart = infoContent.IndexOf("\"", pathStart + 7) + 1;
                        var pathValueEnd = infoContent.IndexOf("\"", pathValueStart);
                        if (pathValueStart > 0 && pathValueEnd > pathValueStart)
                        {
                            var dropboxPath = infoContent.Substring(pathValueStart, pathValueEnd - pathValueStart);
                            dropboxPath = dropboxPath.Replace("\\\\", "\\"); // Unescape backslashes
                            
                            if (Directory.Exists(dropboxPath))
                            {
                                bool isRunning = IsProcessRunning("Dropbox");
                                return new CloudService(CloudServiceType.Dropbox, "Dropbox", dropboxPath, true, isRunning);
                            }
                        }
                    }
                }

                // Fallback: Check default Dropbox location
                var defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Dropbox");
                if (Directory.Exists(defaultPath))
                {
                    bool isRunning = IsProcessRunning("Dropbox");
                    return new CloudService(CloudServiceType.Dropbox, "Dropbox", defaultPath, true, isRunning);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error detecting Dropbox: {ex.Message}");
            }

            return null;
        }

        /// <summary>
        /// Checks if a process is currently running
        /// </summary>
        private bool IsProcessRunning(string processName)
        {
            try
            {
                var processes = Process.GetProcessesByName(processName);
                return processes.Length > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
