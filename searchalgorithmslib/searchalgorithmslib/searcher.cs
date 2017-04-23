using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// Class Searcher.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.ISearcher{T}" />
    public abstract class Searcher<T> : ISearcher<T> {

        /// <summary>
        /// The evaluate nodes
        /// </summary>
        protected int evaluateNodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Searcher{T}"/> class.
        /// </summary>
        public Searcher() {
            evaluateNodes = 0;
        }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution&lt;T&gt;.</returns>
        public abstract Solution<T> Search(ISearchable<T> searchable);

        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int GetNumberOfNodesEvaluated() {
            return evaluateNodes;
        }

        /// <summary>
        /// Backs the trace.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>Solution&lt;T&gt;.</returns>
        protected Solution<T> BackTrace(State<T> s) {
            List<State<T>> path = new List<State<T>>();
            do {
                path.Add(s);
                s = s.CameFrom;
            } while (s != null);
            path.Reverse();

            return new Solution<T>(path, evaluateNodes);
        }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        virtual public void clean() {
            evaluateNodes = 0;
        }
    }
}
