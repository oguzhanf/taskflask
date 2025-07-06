# TaskFlask - Windows Desktop Task Management Application

TaskFlask is a comprehensive Windows desktop task management application built with .NET 8 and WinForms. It provides a complete solution for managing tasks with cloud synchronization capabilities through manual file placement in cloud-synced folders.

## 🚀 Features

### Core Task Management
- **Complete CRUD Operations**: Create, read, update, and delete tasks
- **Rich Task Properties**: 
  - Title and description
  - Due dates with overdue detection
  - Priority levels (Low, Medium, High, Critical)
  - Status tracking (Not Started, In Progress, Completed, Cancelled, On Hold)
  - Categories/tags for organization
  - Creation and modification timestamps

### Advanced Filtering & Sorting
- **Multiple Filter Options**:
  - All tasks
  - By status (Not Started, In Progress, Completed)
  - Overdue tasks
  - High priority tasks
  - Due today
  - Due this week
- **Flexible Sorting**: Sort by title, due date, priority, status, created date, or modified date
- **Show/Hide Completed**: Toggle visibility of completed tasks
- **Real-time Statistics**: View completion percentage, overdue count, and task distribution

### Cloud Synchronization Strategy
- **Automatic Cloud Service Detection**: Detects OneDrive, Google Drive, and Dropbox installations
- **Priority-based Detection**: OneDrive → Google Drive → Dropbox
- **Manual File Placement**: Users place the XML database file in cloud-synced folders
- **No API Dependencies**: Works without cloud service APIs or internet connectivity
- **Cross-device Sync**: Automatic synchronization when cloud services sync the file

### Data Storage & Backup
- **XML-based Database**: Human-readable, portable XML format
- **Local Storage**: Primary storage on local machine
- **Automatic Backups**: Creates backup files before major operations
- **Data Validation**: Ensures data integrity during load/save operations

## 🛠️ Technology Stack

- **.NET 8**: Latest .NET framework for modern performance
- **Windows Forms**: Native Windows desktop UI framework
- **XML Serialization**: Built-in .NET XML handling for data persistence
- **C# 12**: Latest C# language features

## 📋 System Requirements

- **Operating System**: Windows 10 or later
- **.NET 8 Runtime**: Automatically included with the application
- **Memory**: 50MB RAM minimum
- **Storage**: 10MB disk space
- **Optional**: OneDrive, Google Drive, or Dropbox for cloud synchronization

## 🚀 Installation & Setup

### First-Time Setup
1. **Launch Application**: Run TaskFlask.exe
2. **Setup Wizard**: The application will automatically detect installed cloud services
3. **Choose Storage Location**:
   - **Cloud Sync**: Select detected cloud service folder for automatic synchronization
   - **Local Storage**: Choose custom local directory
4. **Database Creation**: Application creates the initial XML database file

### Cloud Service Detection
The application automatically detects cloud services in this priority order:
1. **OneDrive**: Checks registry and default locations
2. **Google Drive**: Detects both classic and File Stream versions
3. **Dropbox**: Reads configuration files and default locations

## 📁 Project Structure

```
TaskFlask/
├── Forms/                  # UI Forms
│   ├── MainForm.cs        # Main application window
│   ├── SetupWizardForm.cs # First-run setup wizard
│   └── TaskEditForm.cs    # Task creation/editing dialog
├── Models/                # Data models
│   ├── TaskItem.cs        # Task entity with properties
│   ├── TaskCollection.cs  # Collection of tasks with statistics
│   ├── TaskEnums.cs       # Enumerations for priority, status, etc.
│   └── CloudService.cs    # Cloud service information
├── Services/              # Business logic services
│   ├── XmlDataService.cs  # XML serialization/deserialization
│   ├── CloudServiceDetector.cs # Cloud service detection
│   ├── SettingsService.cs # Application settings management
│   ├── BackupService.cs   # Backup and restore functionality
│   └── ImportExportService.cs # Import/export in various formats
└── Utils/                 # Utility classes
    └── TaskFilterHelper.cs # Filtering and sorting utilities
```

## 🎯 Key Features Implemented

### ✅ Completed Features
- [x] Complete .NET WinForms project structure
- [x] Cloud service detection system (OneDrive, Google Drive, Dropbox)
- [x] Initial setup wizard with cloud integration
- [x] XML data models with serialization
- [x] Main application window with task list
- [x] Task CRUD operations (Create, Read, Update, Delete)
- [x] Advanced filtering and sorting capabilities
- [x] Application settings and configuration management
- [x] Task statistics and progress tracking
- [x] Auto-save functionality
- [x] Window state persistence

### 🔄 Future Enhancements
- [ ] Import/Export functionality (CSV, JSON, XML)
- [ ] Backup and restore features
- [ ] Search functionality
- [ ] Task categories management
- [ ] Keyboard shortcuts
- [ ] Dark theme support
- [ ] Task templates
- [ ] Recurring tasks
- [ ] Task attachments
- [ ] Notification system

## 🏗️ Building from Source

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code
- Git

### Build Steps
```bash
# Clone the repository
git clone <repository-url>
cd taskflask/TaskFlask

# Restore dependencies
dotnet restore

# Build the application
dotnet build

# Run the application
dotnet run
```

## 📊 Application Architecture

### Data Flow
1. **Startup**: Application checks for first-run and shows setup wizard if needed
2. **Initialization**: Loads settings and initializes data service with database path
3. **Data Loading**: XML data service loads tasks from the specified file location
4. **UI Binding**: Tasks are bound to the UI with filtering and sorting applied
5. **User Interaction**: CRUD operations update the task collection
6. **Auto-save**: Changes are automatically saved at regular intervals
7. **Shutdown**: Window state and settings are persisted

### Cloud Synchronization Workflow
1. **Detection**: Application detects installed cloud services
2. **User Choice**: User selects cloud folder or custom location
3. **File Placement**: XML database is created in the chosen location
4. **Automatic Sync**: Cloud service automatically syncs the file across devices
5. **Cross-device Access**: Application on other devices accesses the same file

## 🔧 Configuration

### Settings Location
- **Settings File**: `%APPDATA%\TaskFlask\settings.json`
- **Database Location**: User-specified during setup
- **Backup Location**: `{DatabaseDirectory}\Backups\`

### Customizable Settings
- Database file path
- Window size and position
- Auto-save interval
- Default filters and sorting
- Backup retention policy

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📞 Support

For support and questions, please open an issue in the GitHub repository.

---

**TaskFlask** - Simple, powerful task management for Windows with seamless cloud synchronization.
