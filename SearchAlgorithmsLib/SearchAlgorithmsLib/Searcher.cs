using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T> {

        private SimplePriorityQueue<State<T>> pQueue;
        private int evaluateNodes;

        //property size of queue.
        public int QueueSize {
            get {
                return pQueue.Count;
            }
        }

        public Searcher() {
            pQueue = new SimplePriorityQueue<State<T>>();
            evaluateNodes = 0;
        }

        protected State<T> PopPQueue() {
            evaluateNodes++;
            return pQueue.Dequeue();
        }

        Solution Search(ISearchable<T> searchable) {
            return new Solution();
        }

        public int GetNumberOfNodesEvaluated() {
            return evaluateNodes;
        }
    }
}
