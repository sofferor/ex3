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

        public void Send(string message) {
            connecter.Send(message);
        }

        public string Recieve() {
            return connecter.Recieve();
        }
    }
}
