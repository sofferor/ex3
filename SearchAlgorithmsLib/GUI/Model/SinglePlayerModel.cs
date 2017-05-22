using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;


namespace GUI.Model {
    class SinglePlayerModel {

        private Connector connector;

        public SinglePlayerModel() {
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
    }
}
