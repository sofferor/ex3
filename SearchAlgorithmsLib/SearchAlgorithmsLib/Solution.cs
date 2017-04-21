using System.Collections.Generic;

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

        }
    }
}