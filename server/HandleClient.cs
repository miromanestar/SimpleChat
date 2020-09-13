using System;
using System.Collections;
using System.Net.Sockets;
using System.Threading;

namespace server {
    public class HandleClient {
        private TcpClient clientSocket;
        private string clientNumber;
        private Hashtable clients;

        public void StartClient(TcpClient _clientSocket, string _clientNumber, Hashtable _clients) {
            clientSocket = _clientSocket;
            clientNumber = _clientNumber;
            clients = _clients;

            var thread = new Thread(DoChat);
            thread.Start();
        }

        private void DoChat() {
            var receivedBytes = new byte[16384];
            int requestCount = 0;
            while(true) {
                try {
                    requestCount++;

                    string data = Program.ParseStream(clientSocket);

                    Console.WriteLine($"From Client { clientNumber }: { data }");
                    Program.Broadcast(data, clientNumber, true);
                } catch(Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}