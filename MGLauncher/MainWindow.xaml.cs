using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MGLauncher
{
    public partial class MainWindow : Window
    {
        bool keepLauncherOpen = false;

        string gameArgs = string.Empty;

        string gamePath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            OnComponentInitialized();
        }

        void OnComponentInitialized()
        {
            string launchPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            launchPath = Path.GetFullPath(Path.Combine(launchPath, @"..\..\..\MGTest\bin\Debug\net6.0-windows\MGTest.exe"));

            //C:\Users\The1Wolfcast\source\repos\MGTest\MGLauncher\bin\Debug
            //start here ^

            //C:\Users\The1Wolfcast\source\repos\MGTest\MGTest\bin\Debug\net6.0-windows\MGTest.exe
            //get to here ^

            txtbxGamePath.Text = launchPath;
        }

        private void btnLaunchGame_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo game = new ProcessStartInfo();

            game.Arguments = gameArgs;

            game.FileName = gamePath;

            game.WindowStyle = ProcessWindowStyle.Normal;

            game.CreateNoWindow = false;

            int exitCode;

            if(File.Exists(gamePath))
            {
                if (!keepLauncherOpen)
                {
                    this.ShowInTaskbar = false;
                    this.WindowState = WindowState.Minimized;
                }

                using (Process proc = Process.Start(game))
                {
                    proc.WaitForExit();

                    if (!keepLauncherOpen)
                    {
                        this.ShowInTaskbar = true;
                        this.WindowState = WindowState.Normal;
                    }

                    exitCode = proc.ExitCode;

                    if (exitCode != 0)
                    {
                        MessageBox.Show($"An error occured! Exit Code {exitCode}", "Game Crashed", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Game closed with Exit Code {exitCode}", "Game Closed", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
            }

            else
            {
                MessageBox.Show($"Game is not located at: {gamePath}", "Game not found", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }


        private void txtbxArgs_Changed(object sender, TextChangedEventArgs e)
        {
            //Use "crash" to test out the error messagebox
            gameArgs = txtbxArgs.Text;
        }

        private void chkLauncherOpen_Checked(object sender, RoutedEventArgs e) 
        {
            keepLauncherOpen = !keepLauncherOpen;
        }

        private void txtbxGamePath_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtbxGamePath.Text == string.Empty || txtbxGamePath.Text == null)
            {
                btnLaunchGame.IsEnabled = false;
            }
            else
            {
                gamePath = txtbxGamePath.Text;
                btnLaunchGame.IsEnabled = true;
            }
            
        }
        private void btnBrowsePath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".exe";
            dlg.Filter = "Executable File (.exe)|*.exe";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                txtbxGamePath.Text = filename;
            }
        }
    }
}
