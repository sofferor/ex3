using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher : ISearcher {
        private SimplePriorityQueue<State<string>> pQueue;
        private int evaluatedNodes;

        public Searcher() {
            pQueue = new SimplePriorityQueue<State<string>>();
            this.evaluatedNodes = 0;
        }

        public abstract Solution Search(ISearchable searchable);

        public int GetNumberOfNodesEvaluated() {
            return evaluatedNodes;
        }

        public State<string> PopPQueue() {
            evaluatedNodes++;
            State<T> temp = pQueue.First;
            return ;
        }
    }
}
