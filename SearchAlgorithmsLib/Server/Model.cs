using MazeGeneratorLib;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    class Model {

        //members
        private IController controller;

        //constructor
        public Model() { }

        public Maze GenerateMaze(string name, int rows, int cols) {
            
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            return maze;
        }

    }
}
