using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server {
    public class Server {
        private int port;
        private TcpListener listener;
        private IClientHandler ch;

        public server(int port, IClientHandler ch) {
            this.port = port;
            this.ch = ch;
        }

        public void start() {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listener = new TcpListener(ep);
            Console.WriteLine("waiting for connections...");

            Task task = new Task(() => {
                while (true) {
                    try {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("got new connection");
                        ch.HandleClient(client);
                    } catch (SocketException) {
                        break;
                    }
                }
                Console.WriteLine("server stopped");
            });
            task.Start();
        }

        public void Stop() {
            listener.Stop();
        }
    }
}