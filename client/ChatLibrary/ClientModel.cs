using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;

namespace ChatLibrary {
    public class ClientModel : INotifyPropertyChanged
    {
        private ClientStringSocket socket;

        private string messageBoard;
        public string MessageBoard {
            get { return messageBoard; }
            set { messageBoard = value; OnPropertyChanged("MessageBoard"); }
        }

        private string currentMessage;
        public string CurrentMessage {
            get { return currentMessage; }
            set { currentMessage = value; OnPropertyChanged("CurrentMessage"); }
        }

        public bool Connected {
            get { return socket != null && socket.Connected; }
        }

        public void Connect() {
            socket = new ClientStringSocket("127.0.0.1", 6660);
            Send();
            messageBoard = $"Welcome: ${ currentMessage }";

            var thread = new Thread(GetMessage);
            thread.Start();
        }

        private void GetMessage() {
            while(true) {
                string msg = socket.ReadString();
                MessageBoard += "\r\n" + msg;
            }
        }

        public void Send() {
            socket.WriteString(currentMessage);
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
