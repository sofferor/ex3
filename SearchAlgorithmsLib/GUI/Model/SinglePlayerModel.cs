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
            connecter.Initialize(Properties.Settings.Default.ServerIP);
        }

        public void Send(string message) {
            connecter.Send(message);
        }

        public string Recieve() {
            return connecter.Recieve();
        }
    }
}
