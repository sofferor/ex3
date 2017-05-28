using System.ComponentModel;
using System.Runtime.CompilerServices;
using Client;
using MazeLib;

namespace GUI.Model {
    public abstract class PlayerModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected Connector connector;
        protected Maze maze;
        protected Position curPos;

        protected PlayerModel() {
            connector = new Connector();
            maze = null;
        }

        public void Initialize(string IP, int port) {
            connector.Initialize(IP, port);
        }

        public string MazeName {
            get { return maze?.Name; }
        }

        public int Rows {
            get {
                if (maze != null) {
                    return maze.Rows;
                }
                return -1;
            }

        }

        public int Cols {
            get {
                if (maze != null) {
                    return maze.Cols;
                }
                return -1;
            }
        }

        public Position GoalPos {
            get => maze.GoalPos;
        }

        public void Send(string message) {
            connector.Send(message);
        }

        public string Receive() {
            return connector.Receive();
        }

        public void Connect() {
            connector.Connect();
        }

        protected virtual void NotifyPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}