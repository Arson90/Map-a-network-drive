using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Mapowanie_Dysku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text.Length > 0 && txtPassword.Password.Length > 0)
            {
                lblMessage.Content = "";
                getMapDrive(txtUserName.Text.Trim(), txtPassword.Password.Trim()); //remove white spaces
            }
            else
            {
                lblMessage.Foreground = Brushes.Red;
                lblMessage.Content = "User name and password is required...";
            }
        }


        private void getMapDrive(string user, string pwd) //get user data
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/C net use P: \\NETWORK_PATH /user:DOMAIN_NAME\" + user + " " + pwd;

                Process.Start(startInfo);

            }
            catch (Exception err)
            {

                lblMessage.Content = err.Message;

            }
            finally {
                System.Threading.Thread.Sleep(500);
                checkMapDrive();
            }

        }

        private void checkMapDrive()
        {

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/C net use";
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                var proc = Process.Start(startInfo);
                string s = proc.StandardOutput.ReadToEnd();

                if (s.Contains("P:"))
                {
                    Process.Start("explorer.exe");
                }
                else {
                    lblMessage.Foreground = Brushes.Red;
                    lblMessage.Content = "User or password is invalid !";
                }


            }
            catch (Exception err)
            {
                lblMessage.Content = err.Message;
            }


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = @"/C net use P: /delete";

                Process.Start(startInfo);
                Environment.Exit(0);
            }
            catch (Exception err)
            {
                lblMessage.Content = err.Message;
            }

        }
    }
}
