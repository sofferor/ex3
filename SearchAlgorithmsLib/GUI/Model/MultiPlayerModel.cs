using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model {
    public class MultiPlayerModel : PlayerModel {
        public event EventHandler<Maze> MazeGenerated;
        private Position otherPos;
        private string games;

        public MultiPlayerModel() : base() { }

        public void GenerateMaze(string name, int rows, int cols) {
            Send("generate " + name + " " + rows.ToString() + " " + cols.ToString());
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            OnMazeGenerated(maze);
        }

        public void Start(string name, int rows, int cols) {
            Send("start " + name + " " + rows.ToString() + " " + cols.ToString());
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            otherPos = curPos;
            OnMazeGenerated(maze);
        }

        public void Join(string name) {
            Send("join " + name);
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            otherPos = curPos;
            OnMazeGenerated(maze);
        }

        protected virtual void OnMazeGenerated(Maze m) {
            MazeGenerated?.Invoke(this, m);
        }

        public string GameAt(int i) {
            string[] gameList;
            if (games != null) {
                gameList = games.Replace("[", "").Replace("]", "").Replace("\n", "").Split(',');
                return gameList[i];
            }
            return null;
        }
    }
}
