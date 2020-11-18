using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для ClientChatWindow.xaml
    /// </summary>
    public partial class ClientChatWindow : Window
    {
        private readonly ObservableCollection<string> messages = new ObservableCollection<string>();
        private string username;
        private string ipAddress = "127.0.0.1";
        private int port = 62227;
        static TcpClient client;
        //static NetworkStream stream;

        public ClientChatWindow(string name)
        {
            InitializeComponent();

            username = name;
            chatBox.ItemsSource = messages;
            //stream = client.GetStream();
        }

        private async void OnReady(object sender, RoutedEventArgs e)
        {
            client = new TcpClient();

            try
            {
                client.Connect(ipAddress, port);

                NetworkStream stream = client.GetStream();

                var data = Encoding.UTF8.GetBytes(username);

                await stream.WriteAsync(data, 0, data.Length);

                await Task.Run(() =>
                {
                    ReceiveMessage(stream);
                });

                while (true)
                {
                    string message = Console.ReadLine();
                    byte[] datagrams = Encoding.Unicode.GetBytes(message);
                    stream.Write(datagrams, 0, datagrams.Length);
                }

                Disconnect(stream);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private async void SendMessage(object sender, RoutedEventArgs e)
        {
            var recieveClient = new TcpClient();

            try
            {
                recieveClient.Connect(ipAddress, port);

                var sendStream = recieveClient.GetStream();

                var data = Encoding.UTF8.GetBytes(messageTextBox.Text);

                await sendStream.WriteAsync(data, 0, data.Length);

                sendStream.Close();
                recieveClient.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void ReceiveMessage(NetworkStream stream)
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[512]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, data.Length));
                    } while (stream.DataAvailable);

                    string message = builder.ToString();
                    messages.Add(message);
                    //Console.WriteLine(message);//вывод сообщения
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    Disconnect(stream);
                }
            }
        }

        static void Disconnect(NetworkStream stream)
        {
            if (stream != null)
            {
                stream.Close();
            }

            if (client != null)
            {
                client.Close();
            }

            Environment.Exit(0); //завершение процесса
        }
    }
}
