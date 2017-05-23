using GUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace GUI.ViewModel {
    public class SinglePlayerViewModel : ViewModel {
        
        private string mazeString;

        public SinglePlayerViewModel(PlayerModel model) : base(model) {
            this.model.NewMaze += delegate(Object sender, Maze maze) {
                maze.ToString();
            };
        }

        public void generateMaze(string name, int rows, int cols) {
            model.generateMaze(name, rows, cols);
        }
    }
}
