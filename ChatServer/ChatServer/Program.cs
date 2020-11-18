using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        static Server server;

        static async Task Main(string[] args)
        {
            try
            {
                server = new Server();
                await Task.Run(() =>
                 {
                     server.Listen();
                 });
            }
            catch(Exception exception)
            {
                server.Disconnect();
                Console.WriteLine(exception.Message);
            }

            //var ipAddress = IPAddress.Parse("127.0.0.1");
            //var port = 62227;
            ////List<NetworkStream> clients = new List<NetworkStream>();
            ////TcpClient[] clients = new TcpClient[2];
            //List<ConnectedClient> clients = new List<ConnectedClient>();
            //List<TcpClient> tcpClients = new List<TcpClient>();
            //int clientIndex = 0;

            //var listener = new TcpListener(ipAddress, port);

            //try
            //{
            //    listener.Start();

            //    while (true)
            //    {
            //        var incomingConnection = await listener.AcceptTcpClientAsync();

            //        //clients[clientIndex] = incomingConnection;

            //        _ = Task.Run(() =>
            //        {
            //            var networkStream = incomingConnection.GetStream();
            //            tcpClients.Add(incomingConnection);
            //            ConnectedClient client = new ConnectedClient();
            //            clients.Add(client);
            //            //var networkStream = clients[clientIndex].GetStream();
            //            //clients.Add(networkStream);

            //            var stringBuilder = new StringBuilder();

            //            do
            //            {
            //                var buffer = new byte[512];

            //                networkStream.Read(buffer, 0, buffer.Length);
            //                stringBuilder.Append(Encoding.UTF8.GetString(buffer));
            //            } while (networkStream.DataAvailable);

            //            Console.WriteLine($"Сообщение - {stringBuilder.ToString()}");

            //            //byte[] data = Encoding.UTF8.GetBytes(stringBuilder.ToString());

            //            //for (int i = 0; i < clients.Count; i++)
            //            //{
            //            //    if (clients[i].Id != client.Id)
            //            //    {
            //            //        clients[i].Stream.Write(data, 0, data.Length);
            //            //    }
            //            //}

            //            //foreach (var cl in clients)
            //            //{
            //            //    if (cl.Id != client.Id)
            //            //    {
            //            //        var data = Encoding.UTF8.GetBytes(stringBuilder.ToString());

            //            //        do
            //            //        {
            //            //            var buffer = new byte[512];
            //            //            networkStream.WriteAsync(buffer, 0, buffer.Length);

            //            //        } while (networkStream.);


            //            //    }
            //            //}

            //            networkStream.Close();
            //            incomingConnection.Close();

            //        });
            //        clientIndex++;
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.Message);
            //}

            //Console.ReadLine();
        }
    }
}
