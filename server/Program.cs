using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server {
    class Program {
        public static Hashtable clients = new Hashtable();

        static void Main(string[] args) {
            var serverSocket = new TcpListener(IPAddress.Any, 6660);
            var clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine("Server started");

            int counter = 0;
            while(true) {
                counter++;

                clientSocket = serverSocket.AcceptTcpClient();

                string data = ParseStream(clientSocket);

                clients.Add(data, clientSocket);
                Broadcast($"{ data } joined", data, false);
                Console.WriteLine($"{ data } joined the server.");

                var client = new HandleClient();
                client.StartClient(clientSocket, data, clients);
            }
        }

        public static void Broadcast(string msg, string username, bool flag) {
            foreach(DictionaryEntry item in clients) {
                var broadcastSocket = (TcpClient)item.Value;
                NetworkStream stream = broadcastSocket.GetStream();
                byte[] broadcastBytes = flag ? Encoding.ASCII.GetBytes($"{ username }: { msg }") : Encoding.ASCII.GetBytes($"Anonymous: { msg }");

                stream.Write(broadcastBytes, 0, broadcastBytes.Length);
                stream.Flush();
            }
        }

        public static string ParseStream(TcpClient clientSocket) {
            var receivedBytes = new byte[16384];
            NetworkStream stream = clientSocket.GetStream();
            stream.Read(receivedBytes, 0, clientSocket.ReceiveBufferSize);
            string data = Encoding.ASCII.GetString(receivedBytes);
            data = data.Substring(0, data.IndexOf('\0'));

            return data;
        }
    }
}
