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

        public IPEndPoint Ep {
            get => ep;
            set => ep = value;
        }

        public void Initialize(string IP, int port) {

            try {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), port);
                Ep = ep;

                client = new TcpClient();
                client.Connect(ep);
                stream = client.GetStream();
                writer = new BinaryWriter(stream);
                reader = new BinaryReader(stream);
            }
            catch (Exception e) {
                NotifyPropertyChanged("lostConnection");
            }
        }

        public void Send(string message) {
            try {
                writer.Write(message);
                writer.Flush();
            }
            catch (Exception e) {
                NotifyPropertyChanged("lostConnection");
            }
        }

        public string Receive() {

            try {
                return reader.ReadString();
            }
            catch (Exception e) {
                NotifyPropertyChanged("lostConnection");
                return "lostConnection";
            }
        }

        public void Listen() {
            stop = false;
            //define task to listen to the other player moves
            new Task(() => {
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
                        NotifyPropertyChanged("lostConnection");
                        stop = true;
                        return;
                    }
                }
            }).Start();
        }

        protected virtual void NotifyPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Connect() {
            try {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                NetworkStream stream = client.GetStream();
                writer = new BinaryWriter(stream);
                reader = new BinaryReader(stream);
            }
            catch (Exception e) {
                NotifyPropertyChanged("lostConnection");
            }
        }
    }
}
