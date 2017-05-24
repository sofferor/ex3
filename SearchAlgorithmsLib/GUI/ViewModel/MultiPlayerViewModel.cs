using GUI.Model;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModel {
    public class MultiPlayerViewModel : ViewModel {
        private MultiPlayerModel model;

        private string mazeString;
        private string otherMazeString;
        private int gameSelected;

        public MultiPlayerViewModel(PlayerModel model) : base(model) {
            this.model = model as MultiPlayerModel;
            this.model.MazeGenerated += delegate (Object sender, Maze maze) {
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

        public string OtherMazeString {
            get => otherMazeString;
            set {
                otherMazeString = value;
                NotifyPropertyChanged("OtherMazeString");
            }
        }

        public int GameSelected {
            get { return gameSelected; }
            set {
                gameSelected = value;
                MazeString = model.GameAt(gameSelected);
            }
        }

        public void GenerateMaze(string name, int rows, int cols) {
            model.GenerateMaze(name, rows, cols);
        }

        public void Start(string name, int rows, int cols) {
            model.Start(name, rows, cols);
        }
    }
}
