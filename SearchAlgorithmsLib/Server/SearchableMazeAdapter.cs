using System.Collections.Generic;
using MazeLib;
using SearchAlgorithmsLib;

namespace Server {
    /// <summary>
    /// Class SearchableMazeAdapter.
    /// </summary>
    /// <seealso cref="SearchAlgorithmsLib.Searchable{MazeLib.Position}" />
    public class SearchableMazeAdapter : Searchable<Position> {

        //members/// <summary>
        /// The maze
        /// </summary>
        private Maze maze;

        //property
        /// <summary>
        /// Gets my maze.
        /// </summary>
        /// <value>My maze.</value>
        public Maze MyMaze {
            get {
                return maze;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchableMazeAdapter"/> class.
        /// </summary>
        public SearchableMazeAdapter() {
            maze = new Maze();
            InitialState = State<Position>.StatePool.GetState(maze.InitialPos);
            GoalState = State<Position>.StatePool.GetState(maze.GoalPos);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchableMazeAdapter"/> class.
        /// </summary>
        /// <param name="other">The other.</param>
        public SearchableMazeAdapter(Maze other) {
            maze = other;
            InitialState = State<Position>.StatePool.GetState(maze.InitialPos);
            GoalState = State<Position>.StatePool.GetState(maze.GoalPos);
        }

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>List&lt;State&lt;T&gt;&gt;.</returns>
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

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        public override void Clean() {
            Position p = new Position(0, 0);
            for (int i = 0; i < maze.Rows; i++) {
                p.Row = i;
                for (int j = 0; j < maze.Cols; j++) {
                    p.Col = j;
                    State<Position>.StatePool.GetState(p).clean();
                }
            }
        }
    }
}
