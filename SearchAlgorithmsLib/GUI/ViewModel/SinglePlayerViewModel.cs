using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GUI.Model;
using MazeLib;

namespace GUI.ViewModel {
    public class SinglePlayerViewModel : ViewModel {
        public event EventHandler<Key> AutoPress; 
        private SinglePlayerModel model;

        private string mazeString;

        public SinglePlayerViewModel(PlayerModel model) : base(model) {
            this.model = model as SinglePlayerModel;
            
            this.model.MazeGenerated += GenMaze;
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
            EventHandler<Position> move = null;
            move = delegate (Object sender, Position pos) {
                if (pos.Row == -1) {
                    return;
                } else if (pos.Row == -2) {
                    NotifyPropertyChanged("wonMaze");
                    this.model.MazeGenerated -= GenMaze;
                    model.NewPos -= move;
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
                    this.model.MazeGenerated -= GenMaze;
                    model.NewPos -= move;
                }
            };
            this.model.NewPos += move;
            model.Move(direction);
            
        }

        public void Restart() {
            this.model.RestartGame();
        }

        public void SolveMaze() {
            Restart();
            EventHandler<Key> solve = null;
            solve = delegate (object sender, Key key) {
                if (key == Key.None) {
                    model.MovePLayer -= solve;
                } else { 
                    OnAutoPress(key);
                }
            };
            model.MovePLayer += solve;
            model.SolveMaze();
            
        }

        protected virtual void OnAutoPress(Key e) {
            AutoPress?.Invoke(this, e);
        }

        private void GenMaze(Object sender, Maze maze) {
            MazeString = maze.ToString();
            NotifyPropertyChanged("mazeGenerated");
        }
    }
}
