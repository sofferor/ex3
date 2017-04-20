using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public class DFS<T> : SearcherByStack <T> {
        public DFS() { }

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