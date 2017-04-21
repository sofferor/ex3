using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests {
    class Program {
        static void Main(string[] args) {

            //generate the maze.
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(30, 30);
            //adapt the maze.
            SearchableMazeAdapter searchableMaze = new SearchableMazeAdapter(maze);

            //printing the maze.
            Console.Write(searchableMaze.MyMaze.ToString());

            Solution<Position> BFSSolution;
            Solution<Position> DFSSolution;

            //solving by BFS.
            ISearcher<Position> searcher = new BFS<Position>();
            BFSSolution = searcher.Search(searchableMaze);
            BFSSolution.Name = searchableMaze.MyMaze.Name;
            searchableMaze.Clean();

            //solving by DFS.
            searcher = new DFS<Position>();
            DFSSolution = searcher.Search(searchableMaze);
            DFSSolution.Name = searchableMaze.MyMaze.Name;

            //printing the solutions.
            Console.WriteLine(BFSSolution.EvaluatedNodes);
            Console.WriteLine(DFSSolution.EvaluatedNodes);
        }
    }
}
