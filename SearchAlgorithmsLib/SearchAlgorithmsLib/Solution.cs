using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace SearchAlgorithmsLib {
    public class Solution<T> {

        //members
        private List<State<T>> path;
        private int evaluatedNodes;
        private string name;

        //property
        public int EvaluatedNodes {
            get => evaluatedNodes;
        }
        public string Name {
            get => name;
            set => name = value;
        }
        public ReadOnlyCollection<State<T>> Path => path.AsReadOnly();

        //constructor
        public Solution(List<State<T>> p, int evaluate) {
            path = p;
            evaluatedNodes = evaluate;
            name = "";
        }

        public string ToJson() {
            JObject JsonSolution = new JObject();

            //convert the path to string
            string pathString = "";
            foreach (State<T> s in path) {
                pathString += s.ToJson();
            }

            JsonSolution["Name"] = name;
            JsonSolution["Solution"] = pathString;
            JsonSolution["NodesEvaluated"] = evaluatedNodes;
            return JsonSolution.ToString();
        }

        public ReadOnlyCollection<State<T>> Path => path.AsReadOnly();
    }
}