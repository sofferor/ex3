using System;
using System.Threading.Tasks;
using GUI.Model;
using MazeLib;

namespace GUI.ViewModel {
    public class SinglePlayerViewModel : ViewModel {
        private SinglePlayerModel model;

        private string mazeString;

        public SinglePlayerViewModel(PlayerModel model) : base(model) {
            this.model = model as SinglePlayerModel;
            this.model.MazeGenerated += delegate(Object sender, Maze maze) {
                MazeString = maze.ToString();
                NotifyPropertyChanged("mazeGenerated");
            };
        }

        public string MazeString {
            get => mazeString;
            set {
                mazeString = value;
                NotifyPropertyChanged("MazeString");
            }
        }

        public void GenerateMaze(string name, int rows, int cols) {
            model.GenerateMaze(name, rows, cols);
        }
    }
}
