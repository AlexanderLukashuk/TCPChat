using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Server
    {
        static TcpListener tcpListener; // сервер для прослушивания
        List<ConnectedClient> clients = new List<ConnectedClient>(); // все подключения
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 62227;

        public void AddConnection(ConnectedClient client)
        {
            clients.Add(client);
        }

        public void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            ConnectedClient client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
            {
                clients.Remove(client);
            }
        }

        // прослушивание входящих подключений
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(ipAddress, port);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    ConnectedClient client = new ConnectedClient(tcpClient, this);
                    //Thread clientThread = new Thread(new ThreadStart(clientObject.Process));
                    //clientThread.Start();

                    Task.Run(() =>
                    {
                        client.Start();
                    });
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Disconnect();
            }
        }

        // трансляция сообщения подключенным клиентам
        public void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id != id) // если id клиента не равно id отправляющего
                {
                    clients[i].Stream.Write(data, 0, data.Length); //передача данных
                }
            }
        }

        public void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
            Environment.Exit(0); //завершение процесса
        }
    }
}
