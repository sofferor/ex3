using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client {
    public class Connecter {
        public Connecter() {

        }

        public void Initialize(string IP) {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), int.Parse(ConfigurationManager.AppSettings["Port"]));
        }

        public void Send(string message) {
            
        }

        public string Recieve() {
            return "S";
        }
    }
}
