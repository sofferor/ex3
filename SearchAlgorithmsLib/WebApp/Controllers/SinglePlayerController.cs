// ***********************************************************************
// Assembly         : WebApp
// Author           : Haim
// Created          : 06-11-2017
//
// Last Modified By : Haim
// Last Modified On : 06-29-2017
// ***********************************************************************
// <copyright file="SinglePlayerController.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MazeLib;
using Newtonsoft.Json.Linq;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// Class SinglePlayerController.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class SinglePlayerController : ApiController
    {
        /// <summary>
        /// The model
        /// </summary>
        static Model model = new Model();
        // GET: api/SinglePlayer
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <returns>JObject.</returns>
        public JObject GetMaze(string name, int rows, int cols) {
            Maze maze = model.GenerateMaze(name, rows, cols).MyMaze;
            return JObject.Parse(maze.ToJSON());
        }

        /// <summary>
        /// Gets the solve.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="algName">Name of the alg.</param>
        /// <returns>JObject.</returns>
        [Route("api/SinglePlayer/{name}/{algName}")]
        public JObject GetSolve(string name, string algName) {
            switch (algName) {
                case "BFS":
                    return JObject.Parse(model.Solve(name, Algoritem.BFS).ToJson());
                case "DFS":
                    return JObject.Parse(model.Solve(name, Algoritem.DFS).ToJson());
                default:
                    break;
            }
            return new JObject("error");
        }

        // POST: api/SinglePlayer
        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SinglePlayer/5
        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SinglePlayer/5
        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}
