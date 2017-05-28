using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model {
    public class MultiPlayerModel : PlayerModel {
        public event EventHandler<Maze> MazeGenerated;
        public event EventHandler<Position> NewPos;
        public event EventHandler<Position> OtherNewPos;
        public event EventHandler<string> ListOfGames;
        private Position otherPos;
        private string listOfGamesString;
        private string game;
        private string message;
        private Task listen;


        public MultiPlayerModel() : base() {
            connector.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public string Message {
            get => message;
            set {
                message = value;
                NotifyPropertyChanged("Message");
            }
        }

        public string ListOfGamesString {
            get => listOfGamesString;
            set => listOfGamesString = value;
        }

        public Position OtherPos {
            get => otherPos;
            set => otherPos = value;
        }

        public void GenerateMaze(string name, int rows, int cols) {
            Send("generate " + name + " " + rows.ToString() + " " + cols.ToString());
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            OnMazeGenerated(maze);
        }

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

        public void AskListOfGames() {
            Send("list");
            listOfGamesString = Receive();
            OnListOfGames(listOfGamesString);
        }

        protected virtual void OnListOfGames(string g) {
            ListOfGames?.Invoke(this, g);
        }

        protected virtual void OnMazeGenerated(Maze m) {
            MazeGenerated?.Invoke(this, m);
        }


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

        protected virtual void OnNewPos(Position e) {
            NewPos?.Invoke(this, e);
        }



        protected virtual void OnOtherNewPos(Position e) {
            OtherNewPos?.Invoke(this, e);
        }


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
    }
}
