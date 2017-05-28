using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GUI.Model;
using MazeLib;

namespace GUI.ViewModel {
    /// <summary>
    /// Class SinglePlayerViewModel.
    /// </summary>
    /// <seealso cref="GUI.ViewModel.ViewModel" />
    public class SinglePlayerViewModel : ViewModel {
        /// <summary>
        /// Occurs when [automatic press].
        /// </summary>
        public event EventHandler<Key> AutoPress;
        /// <summary>
        /// The model
        /// </summary>
        private SinglePlayerModel model;

        /// <summary>
        /// The maze string
        /// </summary>
        private string mazeString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerViewModel"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public SinglePlayerViewModel(PlayerModel model) : base(model) {
            this.model = model as SinglePlayerModel;
            
            this.model.MazeGenerated += GenMaze;
        }

        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        public string MazeString {
            get => mazeString;
            set {
                mazeString = value;
                NotifyPropertyChanged("MazeString");
            }
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void GenerateMaze(string name, int rows, int cols) {
            model.GenerateMaze(name, rows, cols);
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direction">The direction.</param>
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

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart() {
            this.model.RestartGame();
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
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

        /// <summary>
        /// Called when [automatic press].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnAutoPress(Key e) {
            AutoPress?.Invoke(this, e);
        }

        /// <summary>
        /// Gens the maze.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="maze">The maze.</param>
        private void GenMaze(Object sender, Maze maze) {
            MazeString = maze.ToString();
            NotifyPropertyChanged("mazeGenerated");
        }
    }
}
