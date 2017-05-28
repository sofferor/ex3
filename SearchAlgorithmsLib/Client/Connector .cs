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
    /// <summary>
    /// Class Connector.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class Connector : INotifyPropertyChanged {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// The ep
        /// </summary>
        private IPEndPoint ep;
        /// <summary>
        /// The writer
        /// </summary>
        private BinaryWriter writer;
        /// <summary>
        /// The reader
        /// </summary>
        private BinaryReader reader;
        /// <summary>
        /// The stream
        /// </summary>
        private NetworkStream stream;
        /// <summary>
        /// The client
        /// </summary>
        private TcpClient client;
        /// <summary>
        /// The stop
        /// </summary>
        public bool stop = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connector"/> class.
        /// </summary>
        public Connector() { }

        /// <summary>
        /// Gets or sets the reader.
        /// </summary>
        /// <value>The reader.</value>
        public BinaryReader Reader {
            get => reader;
            set => reader = value;
        }

        /// <summary>
        /// Gets or sets the writer.
        /// </summary>
        /// <value>The writer.</value>
        public BinaryWriter Writer {
            get => writer;
            set => writer = value;
        }

        /// <summary>
        /// Gets or sets the ep.
        /// </summary>
        /// <value>The ep.</value>
        public IPEndPoint Ep {
            get => ep;
            set => ep = value;
        }

        /// <summary>
        /// Initializes the specified ip.
        /// </summary>
        /// <param name="IP">The ip.</param>
        /// <param name="port">The port.</param>
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

        /// <summary>
        /// Sends the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Send(string message) {
            try {
                writer.Write(message);
                writer.Flush();
            }
            catch (Exception e) {
                NotifyPropertyChanged("lostConnection");
            }
        }

        /// <summary>
        /// Receives this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Receive() {

            try {
                return reader.ReadString();
            }
            catch (Exception e) {
                NotifyPropertyChanged("lostConnection");
                return "lostConnection";
            }
        }

        /// <summary>
        /// Listens this instance.
        /// </summary>
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

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void NotifyPropertyChanged(string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
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
