using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;


namespace GUI.Model {
    class SinglePlayerModel {

        private Connecter connecter;

        public SinglePlayerModel() {
            connecter = new Connecter();
            string s = Properties.Settings.Default.ServerIP;
            connecter.Initialize(Properties.Settings.Default.ServerIP, Properties.Settings.Default.ServerPort);
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
            connecter.Send(message);
        }

        public string Recieve() {
            return connecter.Receive();
        }
    }
}
