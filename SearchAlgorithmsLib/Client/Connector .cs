using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client {
    public class Connector {
        private BinaryWriter writer;
        private BinaryReader reader;
        public Connector() { }

        public BinaryReader Reader {
            get => reader;
            set => reader = value;
        }

        public BinaryWriter Writer {
            get => writer;
            set => writer = value;
        }

        public void Initialize(string IP, int port) {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), port);

            TcpClient client = new TcpClient();
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
        }

        public void Send(string message) {
            writer.Write(message);
            writer.Flush();
        }

        public string Receive() {
            return reader.ReadString();
        }
    }
}
