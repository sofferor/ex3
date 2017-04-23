using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

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

        //constructor
        public Solution(List<State<T>> p, int evaluate) {
            path = p;
            evaluatedNodes = evaluate;
            name = "";
        }

        public string ToJson() {
            JObject solution = new JObject();
            solution["Name"] = name;
            solution["Solution"] = path;
            solution["NodesEvaluated"] = evaluatedNodes;
            return solution.ToString();
        }

        public ReadOnlyCollection<State<T>> Path => path.AsReadOnly();
    }
}