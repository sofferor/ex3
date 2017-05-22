using System.ComponentModel;
using System.Runtime.CompilerServices;
using Client;

namespace GUI.Model {
    public abstract class PlayerModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private Connector connector;

        protected PlayerModel() {
            connector = new Connector();
            connector.Initialize(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
        }

        public string MazeName {
            get { return Properties.Settings.Default.MazeName; }
            set { Properties.Settings.Default.MazeName = value; }
        }

        public int Rows {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }

        public int Cols {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }

        public void Send(string message) {
            connector.Send(message);
        }

        public string Receive() {
            return connector.Receive();
        }

        protected virtual void NotifyPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}