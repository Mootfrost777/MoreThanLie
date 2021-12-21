using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace MoreThanLight_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string FileTransferDataName = "../../../../MainGame/bin/Debug/netcoreapp3.1/DataTransfer.txt";
        public static string FileName = "accounts.txt";
        public static string ExeName = "../../../../MainGame/bin/Debug/netcoreapp3.1/MainGame.exe";

        public static bool IsEnterBtn = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            Processing LogClick = new Processing(FileName);
            if (IsEnterBtn)
            {
                if (LogClick.IsEquals(Login, Password) == false ||
                    Login.Text.IndexOf(' ') >= 0 ||
                    Login.Text.IndexOf(' ') >= 0) // Нет такого аккаунта или криво ввели
                {
                    LogClick.SetZero(Login, Password);
                    ErrText.Visibility = Visibility.Visible;
                    SuccessText.Visibility = Visibility.Hidden;
                    Launch.IsEnabled = false;
                }
                else // Все ок
                {
                    IsEnterBtn = false;
                    LogClick.DisplayAccountName(Enter, Login, AccountDisplayName);
                    ErrText.Visibility = Visibility.Hidden;
                    SuccessText.Visibility = Visibility.Visible;
                    Launch.IsEnabled = true;
                    TransferName();
                    LogClick.SetZero(Login, Password);
                }
            }
            else
            {
                IsEnterBtn = true;
                LogClick.DeleteAccountName(Enter, Login, AccountDisplayName);
                LogClick.SetZero(Login, Password);
                Launch.IsEnabled = false;
                ErrText.Visibility = Visibility.Hidden;
                SuccessText.Visibility = Visibility.Visible;
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Processing RegClick = new Processing(FileName);
                if (RegClick.IsEquals(Login, Password) == true ||
                    Login.Text.IndexOf(' ') >= 0 ||
                    Login.Text.IndexOf(' ') >= 0) // Уже есть такой аккаунт или криво ввели
                {
                    RegClick.SetZero(Login, Password);
                    ErrText.Visibility = Visibility.Visible;
                    SuccessText.Visibility = Visibility.Hidden;
                    Launch.IsEnabled = false;
                }
                else // Все ок
                {
                    IsEnterBtn = false;
                    RegClick.DisplayAccountName(Enter, Login, AccountDisplayName);
                    ErrText.Visibility = Visibility.Hidden;
                    SuccessText.Visibility = Visibility.Visible;
                    Launch.IsEnabled = true;
                    TransferName();
                    StreamWriter sw = new StreamWriter(FileName, true);
                    sw.WriteLine(Login.Text + " " + Password.Text);
                    sw.Close();
                    RegClick.SetZero(Login, Password);
                }
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(ExeName);
        }

        public void TransferName()
        {
            StreamWriter sw = new StreamWriter(FileTransferDataName);
            sw.WriteLine(Login.Text + " " + Password.Text);
            sw.Close();
        }
    }

    class Processing
    {
        public string FileName { get; set; }

        public Processing(string FileName)
        {
            this.FileName = FileName;
        }
        
        public bool IsEquals(TextBox Login, TextBox Password)
        {
            string[] lines = File.ReadAllLines(FileName);
            string[] split = new string[1];
            foreach (string line in lines)
            {
                split = line.Split(' ');
                if (split[0] == Login.Text && split[1] == Password.Text)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetZero(TextBox Login, TextBox Password)
        {
            Login.Text = "";
            Password.Text = "";
        }

        public void DisplayAccountName(Button Enter, TextBox Name, TextBlock AccountDisplayName)
        {
            Enter.Content = "Log out";
            AccountDisplayName.Text = Name.Text;
        }

        public void DeleteAccountName(Button Enter, TextBox Name, TextBlock AccountDisplayName)
        {
            Enter.Content = "Log in";
            AccountDisplayName.Text = "";
        }
    }
}
