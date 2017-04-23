using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// Class BFS.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.SearcherByPriorityQueue{T}" />
    public class BFS<T> : SearcherByPriorityQueue<T> {
        /// <summary>
        /// Initializes a new instance of the <see cref="BFS{T}" /> class.
        /// </summary>
        public BFS() { }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution&lt;T&gt;.</returns>
        public override Solution<T> Search(ISearchable<T> searchable) {

            PushPQueue(searchable.getInitialState());
            HashSet<State<T>> closed = new HashSet<State<T>>();
            int moveCost = 1;

            while (QueueSize > 0) {
                State<T> n = PopPQueue();
                closed.Add(n);
                if (n.Equals(searchable.getGoalState())) {
                    return BackTrace(searchable.getGoalState());
                }
                
                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors) {
                    if (closed.Contains(s)) {
                        continue;
                    }

                    if (!PQcontain(s)) {
                        s.CameFrom = n;
                        PushPQueue(s);
                    } else {
                        if (s.Cost > n.Cost + moveCost) {
                            s.CameFrom = n;
                            s.Cost = n.Cost + moveCost;
                            UpdatePriority(s);
                        }
                    }
                }
            }
            return null;
        }
    }
}