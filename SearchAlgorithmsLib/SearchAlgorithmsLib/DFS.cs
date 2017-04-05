﻿using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public class DFS<T> : Searcher<T> {
        public DFS() { }

        public override Solution<T> Search(ISearchable<T> searchable) {
            PushPQueue(searchable.getInitialState());
            HashSet<State<T>> visited = new HashSet<State<T>>();
            int moveCost = -1;

            while (QueueSize > 0) {
                State<T> n = PopPQueue();
                visited.Add(n);
                if (n.Equals(searchable.getGoalState())) {
                    return BackTrace(searchable.getGoalState());
                }

                if (visited.Contains(n)) {
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
                    PushPQueue(s);
                }
            }
            return null;
        }
    }
}