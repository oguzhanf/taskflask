using TaskFlask.Forms;
using TaskFlask.Services;

namespace TaskFlask;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        try
        {
            // Initialize settings service
            var settingsService = new SettingsService();

            // Check if this is the first run
            if (settingsService.Settings.IsFirstRun || string.IsNullOrEmpty(settingsService.Settings.DatabaseFilePath))
            {
                // Show setup wizard
                using var setupWizard = new SetupWizardForm();
                if (setupWizard.ShowDialog() == DialogResult.OK)
                {
                    // Save the selected database path
                    settingsService.SetDatabasePathAsync(setupWizard.SelectedDatabasePath).Wait();
                }
                else
                {
                    // User cancelled setup, exit application
                    return;
                }
            }

            // Verify database file path is valid
            if (string.IsNullOrEmpty(settingsService.Settings.DatabaseFilePath))
            {
                MessageBox.Show("No database location configured. Please restart the application.",
                    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Start the main application
            Application.Run(new MainForm(settingsService));
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while starting the application: {ex.Message}",
                "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}