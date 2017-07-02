// ***********************************************************************
// Assembly         : WebApp
// Author           : Haim
// Created          : 07-02-2017
//
// Last Modified By : Haim
// Last Modified On : 07-02-2017
// ***********************************************************************
// <copyright file="MazeHub.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebApp.Models;
using Newtonsoft.Json.Linq;

namespace WebApp {
    /// <summary>
    /// Class MazeHub.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.SignalR.Hub" />
    public class MazeHub : Hub {
        /// <summary>
        /// The model
        /// </summary>
        static Model model = new Model();
        /// <summary>
        /// The connected users
        /// </summary>
        private static ConcurrentDictionary<string, List<string>> connectedUsers =
            new ConcurrentDictionary<string, List<string>>();

        /// <summary>
        /// Connects the specified maze name.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void Connect(string mazeName) {
            List<string> list = new List<string>(2);
            list.Insert(0, Context.ConnectionId);
            connectedUsers.GetOrAdd(mazeName, list);
        }

        /// <summary>
        /// Helloes this instance.
        /// </summary>
        public void Hello() {
            Clients.All.hello();
        }

        /// <summary>
        /// Sends the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public void Send(string name, string message) {
            Clients.All.broadcastMessage(name, message);
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="col">The col.</param>
        public void StartGame(string mazeName, int rows, int col) {
            model.Start(mazeName, rows, col, connectedUsers[mazeName][0]);
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="mazeName">Name of the maze.</param>
        public void JoinGame(string mazeName) {
            model.Join(mazeName, Context.ConnectionId);
            connectedUsers[mazeName].Insert(1, Context.ConnectionId);

            //send the maze to the two clients.
            JObject jMaze = JObject.Parse(model.GetMazeByName(mazeName).MyMaze.ToJSON());
            Clients.Client(connectedUsers[mazeName][0]).startFromJoin(jMaze);
            Clients.Client(connectedUsers[mazeName][1]).startFromJoin(jMaze);
        }

        /// <summary>
        /// Plays the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void play(string direction) {
            Move move = model.Play(direction, Context.ConnectionId);
            string otherPlayer;
            if (connectedUsers[move.GetMazeName()][0].Equals(Context.ConnectionId)) {
                otherPlayer = connectedUsers[move.GetMazeName()][1];
            } else {
                otherPlayer = connectedUsers[move.GetMazeName()][0];
            }
        }

        /// <summary>
        /// Closes the specified game name.
        /// </summary>
        /// <param name="gameName">Name of the game.</param>
        public void Close(string gameName) {
            
        }

        /// <summary>
        /// Games the list.
        /// </summary>
        public void GameList() {
            List<string> games = new List<string>();
            foreach (KeyValuePair<string, List<string>> entry in connectedUsers) {
                games.Add(entry.Key);
            }
            Clients.Client(Context.ConnectionId).GameList(games);
        }
    }
}