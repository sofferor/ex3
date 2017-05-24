using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client;
using MazeLib;


namespace GUI.Model {
    class SinglePlayerModel : PlayerModel {
        public event EventHandler<Maze> MazeGenerated;   
        

        public SinglePlayerModel() : base() {
        }

        public void GenerateMaze(string name, int rows, int cols) {
            Send("generate " + name+" "+rows.ToString()+" "+ cols.ToString());
            string mazeString = Receive();
            maze = Maze.FromJSON(mazeString);
            curPos = maze.InitialPos;
            OnMazeGenerated(maze);

        }

        protected virtual void OnMazeGenerated(Maze m) {
            MazeGenerated?.Invoke(this, m);
        }

        public Position Move(Direction direction) {
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
            return curPos;
        }
    }
}
