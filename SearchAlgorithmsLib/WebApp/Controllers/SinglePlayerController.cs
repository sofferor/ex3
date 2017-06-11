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
    public class SinglePlayerController : ApiController
    {
        Model model = new Model();
        // GET: api/SinglePlayer
        public JObject GetMaze(string name, int rows, int cols) {
            Maze maze = model.GenerateMaze(name, rows, cols).MyMaze;
            return JObject.Parse(maze.ToJSON());
        }

        // GET: api/SinglePlayer/5
        public string GetMaze(int id)
        {
            return "value";
        }

        // POST: api/SinglePlayer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SinglePlayer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SinglePlayer/5
        public void Delete(int id)
        {
        }
    }
}
