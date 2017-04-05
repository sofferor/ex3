using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {

    class DFS<T> : Searcher<T> {

        public DFS() { }

        public override Solution<T> Search(ISearchable<T> searchable) {
            PushPQueue(searchable.getInitialState());
            HashSet<State<T>> visited = new HashSet<State<T>>();
            int moveCost = -1;

            while (QueueSize > 0) {
                State<T> n = PopPQueue();
                
                if (visited.Contains(n)) {
                    continue;
                }

                if (n.Equals(searchable.getGoalState())) {
                    return BackTrace(searchable.getGoalState());
                }

                visited.Add(n);

                List<State<T>> succerssors = searchable.getAllPossibleStates(n);
                foreach (State<T> s in succerssors) {

                    if (visited.Contains(s)) {
                        continue;
                    }

                    s.CameFrom = n;
                    s.Cost = n.Cost + moveCost;
                    PushPQueue(s);
                }
            }
            return null;
        }

    }
}
