using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json.Linq;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// Class Solution.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Solution<T> {

        //members
        /// <summary>
        /// The path
        /// </summary>
        private List<State<T>> path;
        /// <summary>
        /// The evaluated nodes
        /// </summary>
        private int evaluatedNodes;
        /// <summary>
        /// The name
        /// </summary>
        private string name;

        //property
        /// <summary>
        /// Gets the evaluated nodes.
        /// </summary>
        /// <value>The evaluated nodes.</value>
        public int EvaluatedNodes {
            get => evaluatedNodes;
        }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {
            get => name;
            set => name = value;
        }
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>The path.</value>
        public ReadOnlyCollection<State<T>> Path => path.AsReadOnly();

        //constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Solution{T}"/> class.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <param name="evaluate">The evaluate.</param>
        public Solution(List<State<T>> p, int evaluate) {
            path = p;
            evaluatedNodes = evaluate;
            name = "";
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <returns>System.String.</returns>
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
        
    }
}