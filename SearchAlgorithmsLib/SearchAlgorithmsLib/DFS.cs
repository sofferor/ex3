using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// Class DFS.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.SearcherByStack{T}" />
    public class DFS<T> : SearcherByStack <T> {
        /// <summary>
        /// Initializes a new instance of the <see cref="DFS{T}"/> class.
        /// </summary>
        public DFS() { }

        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution&lt;T&gt;.</returns>
        public override Solution<T> Search(ISearchable<T> searchable) {
            stack.Push(searchable.getInitialState());
            HashSet<State<T>> visited = new HashSet<State<T>>();
            int moveCost = 1;

            while (StackSize > 0) {
                State<T> n = PopStack();
                
                if (n.Equals(searchable.getGoalState())) {
                    return BackTrace(searchable.getGoalState());
                }

                if (visited.Contains(n)) {
                    ///to decrease evluatednodes???
                    continue;
                }
                visited.Add(n);

                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors) {
                    if (visited.Contains(s)) {
                        continue;
                    }
                    s.CameFrom = n;
                    s.Cost = n.Cost + moveCost;
                    stack.Push(s);
                }
            }
            return null;
        }
    }
}