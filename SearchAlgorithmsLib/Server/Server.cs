using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server {
    /// <summary>
    /// Class Server.
    /// </summary>
    public class Server {
        /// <summary>
        /// The port
        /// </summary>
        private int port;
        /// <summary>
        /// The listener
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// The ch
        /// </summary>
        private IClientHandler ch;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ch">The ch.</param>
        public Server(int port, IClientHandler ch) {
            this.port = port;
            this.ch = ch;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void start() {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            listener.Start();

            Task task = new Task(() => {
                while (true) {
                    try {
                        TcpClient client = listener.AcceptTcpClient();
                        ch.HandleClient(client);
                    } catch (SocketException) {
                        break;
                    }
                }
                Console.WriteLine("server stopped");
            });
            task.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop() {
            listener.Stop();
        }
    }
}