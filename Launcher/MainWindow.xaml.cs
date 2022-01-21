using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string name;
        public MainWindow()
        {
            InitializeComponent();

            int server_port = 8005;
            string server_ip_adress = "25.52.41.179";
            IPAddress server_ip = IPAddress.Parse(server_ip_adress);

            IPEndPoint endPoint = new IPEndPoint(server_ip, server_port);

            Socket socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(endPoint);
            byte[] msg = Encoding.ASCII.GetBytes($"User{name}");
            socket.Send(msg);

            for (int i = 0; i < 10; i++)
            {
                Image image = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri("/logo.png", UriKind.Relative);
                bi3.EndInit();
                image.Source = bi3;
                image.Width = 50;
                Label label = new Label();
                label.Content = i;
                StackPanel sp = new StackPanel();
                sp.Margin = new Thickness(5);
                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(image);
                sp.Children.Add(label);
                lb_avalible.Items.Add(sp);
            }
        }

    }
}
