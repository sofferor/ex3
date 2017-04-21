using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    class Model {

        //members
        private IController controller;
        private Dictionary<string, SearchableMazeAdapter> searchableMazes;
        private Dictionary<string, Dictionary<Algoritem, Solution<Position>>> solutions;
        ISearcher<Position> BFSSearcher;
        ISearcher<Position> DFSSearcher;

        //constructor
        public Model() {
            BFSSearcher = new BFS<Position>();
            DFSSearcher = new DFS<Position>();
        }

        public SearchableMazeAdapter GenerateMaze(string name, int rows, int cols) {

            //if the maze is allready exist we return it.
            if (searchableMazes.ContainsKey(name)) {
                return searchableMazes[name];
            }

            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;

            //adapt the maze.
            SearchableMazeAdapter searchableMaze = new SearchableMazeAdapter(maze);
            searchableMazes.Add(name, searchableMaze);

            return searchableMaze;
        }

        Solution<Position> Solve(string name, Algoritem algoritem) {

            //first of all - checcking if this maze exist
            if (!searchableMazes.ContainsKey(name)) {
                return null;
            }

            //check if there is already a solution.
            if (solutions.ContainsKey(name)) {
                //check if the solution is from the right algoritem(BFS/DFS).
                if ((solutions[name]).ContainsKey(algoritem)) {
                    return (solutions[name])[algoritem];
                }
            }

            //and if not contain, we solve the maze
            Solution<Position> solution;
            if (algoritem == Algoritem.BFS) {
                solution = BFSSearcher.Search(searchableMazes[name]);
            } else {
                solution = DFSSearcher.Search(searchableMazes[name]);
            }

            //check if need to add a new dictionary(if there was no solution at all(DFS and BFS)).
            if (!solutions.ContainsKey(name)) {
                Dictionary<Algoritem, Solution<Position>> d = new Dictionary<Algoritem, Solution<Position>>;
                solutions.Add(name, d);
            }
            //add the solution
            solutions[name].Add(algoritem, solution);
            //return the solution
            return solution;
        }

    }
}
