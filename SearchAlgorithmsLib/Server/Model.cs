using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server {
    public class Model : IModel {

        //members
        private IController controller;
        private Dictionary<string, SearchableMazeAdapter> searchableMazes;
        private Dictionary<string, Dictionary<Algoritem, Solution<Position>>> solutions;
        private Dictionary<string, Game> games;
        ISearcher<Position> BFSSearcher;
        ISearcher<Position> DFSSearcher;      

        //constructor
        public Model() {

            searchableMazes = new Dictionary<string, SearchableMazeAdapter>();
            solutions = new Dictionary<string, Dictionary<Algoritem, Solution<Position>>>();
            games = new Dictionary<string, Game>();

            BFSSearcher = new BFS<Position>();
            DFSSearcher = new DFS<Position>();
        }

        public void SetController(IController c) {
            controller = c;
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

        public Solution<Position> Solve(string name, Algoritem algoritem) {

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
                solution.Name = searchableMazes[name].MyMaze.Name;
            } else {
                solution = DFSSearcher.Search(searchableMazes[name]);
                solution.Name = searchableMazes[name].MyMaze.Name;
            }

            //check if need to add a new dictionary(if there was no solution at all(DFS and BFS)).
            if (!solutions.ContainsKey(name)) {
                Dictionary<Algoritem, Solution<Position>> d = new Dictionary<Algoritem, Solution<Position>>();
                solutions.Add(name, d);
            }
            //add the solution
            solutions[name].Add(algoritem, solution);
            //return the solution
            return solution;
        }

        public string CloseGame(string name) {
            return new JObject().ToString();
        }

        public string GamesList() {
            JObject listOfGames = new JObject();
            foreach (string str in games.Keys) {
                listOfGames[""] = str;
            }
            return listOfGames.ToString();
        }

    }
}
