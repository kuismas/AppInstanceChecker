using System;
using System.Threading;
using System.Windows.Forms;

namespace Surveillance
{
    internal static class Program
    {
        // Define a simple form that serves as the application's main window  
        private class MainForm : Form
        {
            public MainForm()
            {
                Text = "Single Instance Checker"; // Set the form title  
                Size = new System.Drawing.Size(300, 200); // Set the form size  
                Label label = new Label()
                {
                    Text = "Application is running.",
                    Dock = DockStyle.Fill,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };
                Controls.Add(label); // Add label to the form  
            }
        }

        /// <summary>  
        /// The main entry point for the application.  
        /// </summary>  
        [STAThread]
        static void Main()
        {
            // Create a named mutex to detect if the application is already running  
            bool isOnlyInstance;
            using (Mutex mutex = new Mutex(true, "UniqueApp Name", out isOnlyInstance))
            {
                if (!isOnlyInstance)
                {
                    // If another instance is running, show a message and exit  
                    MessageBox.Show("The application is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the application  
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Create and run the main form  
                Application.Run(new MainForm());
            }
            // The mutex will be released when exiting the using block  
        }
    }
}