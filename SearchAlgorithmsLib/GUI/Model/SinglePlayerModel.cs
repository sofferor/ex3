using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;
using Client;
using MazeLib;
using Newtonsoft.Json.Linq;


namespace GUI.Model {
    /// <summary>
    /// Class SinglePlayerModel.
    /// </summary>
    /// <seealso cref="GUI.Model.PlayerModel" />
    public class SinglePlayerModel : PlayerModel {
        /// <summary>
        /// Occurs when [maze generated].
        /// </summary>
        public event EventHandler<Maze> MazeGenerated;
        /// <summary>
        /// Occurs when [new position].
        /// </summary>
        public event EventHandler<Position> NewPos;
        /// <summary>
        /// Occurs when [move p layer].
        /// </summary>
        public event EventHandler<Key> MovePLayer;


        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerModel"/> class.
        /// </summary>
        public SinglePlayerModel() : base() { }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void GenerateMaze(string name, int rows, int cols) {
            Send("generate " + name+" "+rows.ToString()+" "+ cols.ToString());
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            OnMazeGenerated(maze);

        }

        /// <summary>
        /// Called when [maze generated].
        /// </summary>
        /// <param name="m">The m.</param>
        protected virtual void OnMazeGenerated(Maze m) {
            MazeGenerated?.Invoke(this, m);
        }

        /// <summary>
        /// Moves the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// direction - null
        /// or
        /// direction - null
        /// </exception>
        public void Move(Direction direction) {
            Position tempPosition = new Position(curPos.Row, curPos.Col);
            switch (direction) {
                case Direction.Down: {
                    if (curPos.Row + 1 < maze.Rows && maze[curPos.Row + 1, curPos.Col] != CellType.Wall) {
                        curPos = new Position(curPos.Row + 1, curPos.Col);
                    }
                    break;
                }
                case Direction.Left: {
                    if (curPos.Col - 1 >= 0 && maze[curPos.Row, curPos.Col - 1] != CellType.Wall) {
                        curPos = new Position(curPos.Row, curPos.Col - 1);
                    }
                    break;
                }
                case Direction.Right: {
                    if (curPos.Col + 1 < maze.Cols && maze[curPos.Row, curPos.Col + 1] != CellType.Wall) {
                        curPos = new Position(curPos.Row, curPos.Col + 1);
                    }
                    break;
                }
                case Direction.Up: {
                    if (curPos.Row - 1 >= 0 && maze[curPos.Row - 1, curPos.Col] != CellType.Wall) {
                        curPos = new Position(curPos.Row - 1, curPos.Col);
                    }
                    break;
                }
                case Direction.Unknown:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (maze.GoalPos.Equals(maze.InitialPos)) {
                OnNewPos(new Position(-2,-2));
                return;
            }
            OnNewPos(tempPosition.Equals(curPos) ? new Position(-1, -1) : curPos);
        }

        /// <summary>
        /// Called when [new position].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnNewPos(Position e) {
            NewPos?.Invoke(this, e);
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame() {
            curPos = maze.InitialPos;
            OnMazeGenerated(maze);
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        public void SolveMaze() {
            Connect();
            Send("solve "+maze.Name+" "+Properties.Settings.Default.SearchAlgorithm);
            string check = Receive();
            JObject solveJobject = JObject.Parse(check);
            string solutionString = (string) solveJobject["Solution"];
            CharEnumerator solEnumerator = solutionString.GetEnumerator();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += delegate(object sender, EventArgs args) {
                if (solEnumerator.MoveNext()) {
                    switch (solEnumerator.Current) {
                        case '0':
                            OnMovePLayer(Key.Left);
                            break;
                        case '1':
                            OnMovePLayer(Key.Right);
                            break;
                        case '2':
                            OnMovePLayer(Key.Down);
                            break;
                        case '3':
                            OnMovePLayer(Key.Up);
                            break;
                        default:
                            throw new Exception("problem with solveString");
                    }
                } else {
                    dt.Stop();
                    OnMovePLayer(Key.None);
                }
            };

            dt.Interval = TimeSpan.FromSeconds(0.2);
            dt.Start();
        }

        /// <summary>
        /// Called when [move p layer].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnMovePLayer(Key e) {
            MovePLayer?.Invoke(this, e);
        }
    }
}
