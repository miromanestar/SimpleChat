using System.Net.Sockets;
using System.Text;

namespace ChatLibrary {
    public class ClientStringSocket : TcpClient {
        private NetworkStream stream;

        public ClientStringSocket(string hostname, int port) :base(hostname, port) {
            stream = GetStream();
        }

        public string ReadString() {
            var bytes = new byte[16384];
            stream.Read(bytes, 0, ReceiveBufferSize);
            string msg = Encoding.ASCII.GetString(bytes);
            int index = msg.IndexOf('\0');
            return msg.Substring(0, index);
        }

        public void WriteString(string msg) {
            msg += '\0';
            byte[] bytes = Encoding.ASCII.GetBytes(msg);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
    }
}