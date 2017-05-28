using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model {
    /// <summary>
    /// Class MultiPlayerModel.
    /// </summary>
    /// <seealso cref="GUI.Model.PlayerModel" />
    public class MultiPlayerModel : PlayerModel {
        /// <summary>
        /// Occurs when [maze generated].
        /// </summary>
        public event EventHandler<Maze> MazeGenerated;
        /// <summary>
        /// Occurs when [new position].
        /// </summary>
        public event EventHandler<Position> NewPos;
        /// <summary>
        /// Occurs when [other new position].
        /// </summary>
        public event EventHandler<Position> OtherNewPos;
        /// <summary>
        /// Occurs when [list of games].
        /// </summary>
        public event EventHandler<string> ListOfGames;
        /// <summary>
        /// The other position
        /// </summary>
        private Position otherPos;
        /// <summary>
        /// The list of games string
        /// </summary>
        private string listOfGamesString;
        /// <summary>
        /// The game
        /// </summary>
        private string game;
        /// <summary>
        /// The message
        /// </summary>
        private string message;
        /// <summary>
        /// The listen
        /// </summary>
        private Task listen;


        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerModel"/> class.
        /// </summary>
        public MultiPlayerModel() : base() {
            connector.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message {
            get => message;
            set {
                message = value;
                NotifyPropertyChanged("Message");
            }
        }

        /// <summary>
        /// Gets or sets the list of games string.
        /// </summary>
        /// <value>The list of games string.</value>
        public string ListOfGamesString {
            get => listOfGamesString;
            set => listOfGamesString = value;
        }

        /// <summary>
        /// Gets or sets the other position.
        /// </summary>
        /// <value>The other position.</value>
        public Position OtherPos {
            get => otherPos;
            set => otherPos = value;
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void GenerateMaze(string name, int rows, int cols) {
            Send("generate " + name + " " + rows.ToString() + " " + cols.ToString());
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            OnMazeGenerated(maze);
        }

        /// <summary>
        /// Starts the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public void Start(string name, int rows, int cols) {
            Send("start " + name + " " + rows.ToString() + " " + cols.ToString());
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            otherPos = curPos;
            OnMazeGenerated(maze);

            //listen to the other player moves
            connector.Listen();
        }

        /// <summary>
        /// Joins the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Join(string name) {
            Send("join " + name);
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            otherPos = curPos;
            OnMazeGenerated(maze);

            //listen to the other player moves
            connector.Listen();
        }

        /// <summary>
        /// Asks the list of games.
        /// </summary>
        public void AskListOfGames() {
            Send("list");
            listOfGamesString = Receive();
            OnListOfGames(listOfGamesString);
        }

        /// <summary>
        /// Called when [list of games].
        /// </summary>
        /// <param name="g">The g.</param>
        protected virtual void OnListOfGames(string g) {
            ListOfGames?.Invoke(this, g);
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
            string dir = "";
            switch (direction) {
                case Direction.Down: {
                        if (curPos.Row + 1 < maze.Rows && maze[curPos.Row + 1, curPos.Col] != CellType.Wall) {
                            curPos = new Position(curPos.Row + 1, curPos.Col);
                            dir = "down";
                        }
                        break;
                    }
                case Direction.Left: {
                        if (curPos.Col - 1 >= 0 && maze[curPos.Row, curPos.Col - 1] != CellType.Wall) {
                            curPos = new Position(curPos.Row, curPos.Col - 1);
                            dir = "left";
                        }
                        break;
                    }
                case Direction.Right: {
                        if (curPos.Col + 1 < maze.Cols && maze[curPos.Row, curPos.Col + 1] != CellType.Wall) {
                            curPos = new Position(curPos.Row, curPos.Col + 1);
                            dir = "right";
                        }
                        break;
                    }
                case Direction.Up: {
                        if (curPos.Row - 1 >= 0 && maze[curPos.Row - 1, curPos.Col] != CellType.Wall) {
                            curPos = new Position(curPos.Row - 1, curPos.Col);
                            dir = "up";
                        }
                        break;
                    }
                case Direction.Unknown:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (maze.GoalPos.Equals(maze.InitialPos)) {
                OnNewPos(new Position(-2, -2));
                return;
            }

            //if there was actual move, send the move to the other player.
            if (!dir.Equals("")) {
                Send("play " + dir);
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
        /// Called when [other new position].
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void OnOtherNewPos(Position e) {
            OtherNewPos?.Invoke(this, e);
        }


        /// <summary>
        /// Moves the other player.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// direction - null
        /// or
        /// direction - null
        /// </exception>
        public void MoveOtherPlayer(Direction direction) {
            Position tempPosition = new Position(otherPos.Row, otherPos.Col);
            string dir = "";
            switch (direction) {
                case Direction.Down: {
                        if (otherPos.Row + 1 < maze.Rows && maze[otherPos.Row + 1, otherPos.Col] != CellType.Wall) {
                            otherPos = new Position(otherPos.Row + 1, otherPos.Col);
                            dir = "down";
                        }
                        break;
                    }
                case Direction.Left: {
                        if (otherPos.Col - 1 >= 0 && maze[otherPos.Row, otherPos.Col - 1] != CellType.Wall) {
                            otherPos = new Position(otherPos.Row, otherPos.Col - 1);
                            dir = "left";
                        }
                        break;
                    }
                case Direction.Right: {
                        if (otherPos.Col + 1 < maze.Cols && maze[otherPos.Row, otherPos.Col + 1] != CellType.Wall) {
                            otherPos = new Position(otherPos.Row, otherPos.Col + 1);
                            dir = "right";
                        }
                        break;
                    }
                case Direction.Up: {
                        if (otherPos.Row - 1 >= 0 && maze[otherPos.Row - 1, otherPos.Col] != CellType.Wall) {
                            otherPos = new Position(otherPos.Row - 1, otherPos.Col);
                            dir = "up";
                        }
                        break;
                    }
                case Direction.Unknown:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (maze.GoalPos.Equals(maze.InitialPos)) {
                OnOtherNewPos(new Position(-2, -2));
                return;
            }
            OnOtherNewPos(tempPosition.Equals(otherPos) ? new Position(-1, -1) : otherPos);
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close() {
            connector.stop = true;
            connector.Send("close " + maze.Name);
        }
    }
}
