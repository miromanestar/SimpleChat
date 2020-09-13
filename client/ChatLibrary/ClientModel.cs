using System;
using System.ComponentModel;
using System.Net.Sockets;

namespace ChatLibrary {
    public class ClientModel : INotifyPropertyChanged
    {
        private TcpClient socket;
        private NetworkStream stream;

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

        private bool connected;
        public bool Connected {
            get { return connected; }
            set { connected = value; OnPropertyChanged("Connected"); }
        }

        public ClientModel() {
            connected = false;
        }

        #region INotifyPropertyChanged Code
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
