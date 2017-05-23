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
        private Maze maze;

        public SinglePlayerModel() : base() {
        }

        public void GenerateMaze(string name, int rows, int cols) {
            Send(name+" "+rows.ToString()+" "+ cols.ToString());
            maze = Maze.FromJSON(Receive());
            OnMazeGenerated(maze);

        }

        protected virtual void OnMazeGenerated(Maze e) {
            MazeGenerated?.Invoke(this, e);
        }
    }
}
