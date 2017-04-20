using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    public abstract class Searcher<T> : ISearcher<T> {

        protected int evaluateNodes;

        public Searcher() {
            evaluateNodes = 0;
        }

        public abstract Solution<T> Search(ISearchable<T> searchable);

        public int GetNumberOfNodesEvaluated() {
            return evaluateNodes;
        }

        protected Solution<T> BackTrace(State<T> s) {
            List<State<T>> path = new List<State<T>>();
            do {
                path.Add(s);
                s = s.CameFrom;
            } while (s != null);
            path.Reverse();

            return new Solution<T>(path, evaluateNodes);
        }

        virtual public void clean() {
            evaluateNodes = 0;
        }
    }
}
