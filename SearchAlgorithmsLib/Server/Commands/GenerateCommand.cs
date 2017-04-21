﻿using MazeLib;
using System.Net.Sockets;
using Tests;

namespace Server {
    public class GenerateCommand : ICommand {

        //members
        private IModel model;  

        //constructor
        public GenerateCommand(IModel model) {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null) {

            string name = args[0];
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);

            SearchableMazeAdapter searchableMaze = model.GenerateMaze(name, rows, cols);
            return searchableMaze.MyMaze.ToJSON();
        }
    }
}