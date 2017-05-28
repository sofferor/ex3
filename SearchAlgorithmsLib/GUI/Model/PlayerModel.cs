using System.ComponentModel;
using System.Runtime.CompilerServices;
using Client;
using MazeLib;

namespace GUI.Model {
    /// <summary>
    /// Class PlayerModel.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class PlayerModel : INotifyPropertyChanged {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The connector
        /// </summary>
        protected Connector connector;
        /// <summary>
        /// The maze
        /// </summary>
        protected Maze maze;
        /// <summary>
        /// The current position
        /// </summary>
        protected Position curPos;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerModel"/> class.
        /// </summary>
        protected PlayerModel() {
            connector = new Connector();
            maze = null;
        }

        /// <summary>
        /// Initializes the specified ip.
        /// </summary>
        /// <param name="IP">The ip.</param>
        /// <param name="port">The port.</param>
        public void Initialize(string IP, int port) {
            connector.Initialize(IP, port);
        }

        /// <summary>
        /// Gets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        public string MazeName {
            get { return maze?.Name; }
        }

        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows {
            get {
                if (maze != null) {
                    return maze.Rows;
                }
                return -1;
            }

        }

        /// <summary>
        /// Gets the cols.
        /// </summary>
        /// <value>The cols.</value>
        public int Cols {
            get {
                if (maze != null) {
                    return maze.Cols;
                }
                return -1;
            }
        }

        /// <summary>
        /// Gets the goal position.
        /// </summary>
        /// <value>The goal position.</value>
        public Position GoalPos {
            get => maze.GoalPos;
        }

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string message) {
            connector.Send(message);
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Receive() {
            return connector.Receive();
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        public void Connect() {
            connector.Connect();
        }

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void NotifyPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}