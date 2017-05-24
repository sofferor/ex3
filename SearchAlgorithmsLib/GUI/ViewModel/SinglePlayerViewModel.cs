using System;
using System.ComponentModel;
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

        public void MovePlayer(Direction direction) {
            this.model.NewPos += delegate(Object sender, Position pos) {
                if (pos.Row == -1) {
                    return;
                } else if (pos.Row == -2) {
                    NotifyPropertyChanged("wonMaze");
                    return;
                }
                int mazeStringLen = mazeString.Length;
                char[] mazeStringArr = mazeString.ToCharArray();
                

                for (int i = 0; i < mazeStringLen; i++) {
                    switch (mazeStringArr[i]) {
                        case '*': {
                            mazeStringArr[i] = 'w';
                            break;
                        }
                        case 'w': {
                            mazeStringArr[i] = '0';
                            break;
                        }
                        case 'n': {
                            mazeStringArr[i] = 'w';
                            break;
                        }
                    }
                }

                int index = (pos.Row) * Cols + 2 * (pos.Row) + pos.Col;
                char ifEnd = mazeStringArr[index];
                mazeStringArr[index] = 'n';


                MazeString = new string(mazeStringArr);

                if (ifEnd == '#') {
                    NotifyPropertyChanged("wonMaze");
                }
            };
            model.Move(direction);
        }
    }
}
