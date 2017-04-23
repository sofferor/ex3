using System.Collections.Generic;
using MazeLib;
using SearchAlgorithmsLib;

namespace Server {
    public class SearchableMazeAdapter : Searchable<Position> {

        //members
        private Maze maze;

        //property
        public Maze MyMaze {
            get {
                return maze;
            }
        }

        public SearchableMazeAdapter() {
            maze = new Maze();
            InitialState = State<Position>.StatePool.GetState(maze.InitialPos);
            GoalState = State<Position>.StatePool.GetState(maze.GoalPos);
        }

        public SearchableMazeAdapter(Maze other) {
            maze = other;
            InitialState = State<Position>.StatePool.GetState(maze.InitialPos);
            GoalState = State<Position>.StatePool.GetState(maze.GoalPos);
        }

        public override List<State<Position>> getAllPossibleStates(State<Position> s) {
            List<State<Position>> possibleStates = new List<State<Position>>();

            int col = s.S.Col;
            int row = s.S.Row;
            //check if each nighber is exist and free.
            if (col > 0 && maze[row, col - 1] == CellType.Free) {
                possibleStates.Add(State<Position>.StatePool.GetState(new Position(row, col - 1)));
            }
            if (row < maze.Rows - 1 && maze[row + 1, col] == CellType.Free) {
                possibleStates.Add(State<Position>.StatePool.GetState(new Position(row + 1, col)));
            }
            if (col < maze.Cols - 1 && maze[row, col + 1] == CellType.Free) {
                possibleStates.Add(State<Position>.StatePool.GetState(new Position(row, col + 1)));
            }
            if (row > 0 && maze[row - 1, col] == CellType.Free) {
                possibleStates.Add(State<Position>.StatePool.GetState(new Position(row - 1, col)));
            }

            return possibleStates;
        }

        public override void Clean() {
            Position p = new Position(0, 0);
            for (int i = 0; i < maze.Rows; i++) {
                p.Row = i;
                for (int j = 0; i < maze.Cols; i++) {
                    p.Col = j;
                    State<Position>.StatePool.GetState(p).clean();
                }
            }
        }
    }
}
