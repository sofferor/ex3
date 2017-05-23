using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
using MazeLib;


namespace GUI.Model {
    class SinglePlayerModel : PlayerModel {
        public event EventHandler<Maze> MazeGenerated;   
        

        public SinglePlayerModel() : base() {
        }

        public void GenerateMaze(string name, int rows, int cols) {
            Send("generate " + name+" "+rows.ToString()+" "+ cols.ToString());
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            OnMazeGenerated(maze);

        }

        protected virtual void OnMazeGenerated(Maze m) {
            MazeGenerated?.Invoke(this, m);
        }
    }
}
