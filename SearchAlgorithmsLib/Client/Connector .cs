using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client {
    public class Connector : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private IPEndPoint ep;///maybe public
        private BinaryWriter writer;
        private BinaryReader reader;
        NetworkStream stream;
        TcpClient client;
        bool stop = false;

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
            ep = new IPEndPoint(IPAddress.Parse(IP), port);

            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
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

        public void Listen() {
            stop = false;
            //define task to listen to the other player moves
            Task listen = new Task(() => {
                while (!stop) {
                    try {
                        reader = new BinaryReader(stream);
                        string flow = reader.ReadString();
                        if (flow.Contains("wait")) {
                            continue;
                        }
                        //Console.WriteLine("{0}", flow);
                        NotifyPropertyChanged(flow);
                        if (stop || flow.Contains("close")) {
                            stop = true;
                            break;
                        }
                    } catch (Exception e) {
                        client = new TcpClient();
                        client.Connect(ep);
                        stream = client.GetStream();
                        writer = new BinaryWriter(stream);
                        reader = new BinaryReader(stream);
                    }
                }
            });
        }

        protected virtual void NotifyPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
