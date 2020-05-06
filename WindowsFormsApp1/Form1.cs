using System;
using System.Globalization;
using System.Windows.Forms;
using AutoUpdater;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //uncomment below line to see russian version


            AutoUpdater.AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("ru");

            // If you want to open download page when user click on download button uncomment below line.


            AutoUpdater.AutoUpdater.OpenDownloadPage = true;

            //Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.

            AutoUpdater.AutoUpdater.LetUserSelectRemindLater = false;
            AutoUpdater.AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
            AutoUpdater.AutoUpdater.RemindLaterAt = 2;

            //Want to handle update logic yourself then uncomment below line.

            AutoUpdater.AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;

            AutoUpdater.AutoUpdater.Start("file:///D:/project/c%23/terra/20200122/updater/test/WindowsFormsApp1/WindowsFormsApp1/update.xml");
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    var dialogResult =
                        MessageBox.Show(
                            string.Format(
                                "There is new version {0} avilable. You are using version {1}. Do you want to update the application now?",
                                args.CurrentVersion, args.InstalledVersion), @"Update Available",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                    if (dialogResult.Equals(DialogResult.Yes))
                    {
                        try
                        {
                            //You can use Download Update dialog used by AutoUpdater.NET to download the update.

                            AutoUpdater.AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@"There is no update avilable please try again later.", @"No update available",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show(
                       @"There is a problem reaching update server please check your internet connection and try again later.",
                       @"Update check failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AutoUpdater.AutoUpdater.Start("file:///D:/project/c%23/terra/20200122/updater/test/WindowsFormsApp1/WindowsFormsApp1/update.xml");
        }
    }
}
