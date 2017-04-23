using Newtonsoft.Json.Linq;
using System;
using System.Net.Sockets;

namespace Server {
    /// <summary>
    /// Class PlayCommand.
    /// </summary>
    /// <seealso cref="Server.ICommand" />
    public class PlayCommand : ICommand {

        //members
        /// <summary>
        /// The model
        /// </summary>
        private IModel model;

        //constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <exception cref="System.ArgumentNullException">model</exception>
        public PlayCommand(IModel model) {
            if (model == null) throw new ArgumentNullException(nameof(model));
            this.model = model;
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>System.String.</returns>
        public string Execute(string[] args, TcpClient client = null) {

            string step = args[0];
            model.Play(step, client);
            return "wait";
        }
    }
}