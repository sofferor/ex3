using System;
using System.Net.Sockets;
using MazeLib;
using SearchAlgorithmsLib;

namespace Server {
    public class SolveCommand : ICommand {

        //members
        private IModel model;

        //constructor
        public SolveCommand(IModel model) {
            this.model = model;
        }

        public string Execute(string[] args, TcpClient client = null) {

            string name = args[0];
            Algoritem algoritem;
            try {
                algoritem = (Algoritem)int.Parse(args[1]);
            } catch {
                throw new InvalidCastException();
            }

            Solution<Position> solution = model.Solve(name, algoritem);
            return solution.ToJSON();
        }
    }
}