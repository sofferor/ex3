using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
using MazeLib;


namespace GUI.Model {
    class SinglePlayerModel : PlayerModel {
        private Maze maze;

        public SinglePlayerModel() : base() {
        }

        public void generateMaze(string name, int rows, int cols) {
            Send(name+" "+rows.ToString()+" "+ cols.ToString());
            maze = Maze.FromJSON(Receive());

        }
    }
}
