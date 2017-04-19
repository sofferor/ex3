using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public class BFS<T> : SearcherByPriorityQueue<T> {
        public BFS() { }

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