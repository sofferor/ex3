using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class Searcher<T> : ISearcher<T> {

        protected SimplePriorityQueue<State<T>> pQueue;
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

        protected void PushPQueue(State<T> s) {
            pQueue.Enqueue(s, s.Cost);
        }

        public abstract Solution Search(ISearchable<T> searchable);

        public int GetNumberOfNodesEvaluated() {
            return evaluateNodes;
        }
    }
}
