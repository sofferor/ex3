using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class SearcherByPriorityQueue<T> : Searcher<T> {

        protected SimplePriorityQueue<State<T>> pQueue;

        //property size of queue.
        public int QueueSize {
            get {
                return pQueue.Count;
            }
        }

        public SearcherByPriorityQueue() {
            pQueue = new SimplePriorityQueue<State<T>>();
        }

        protected State<T> PopPQueue() {
            evaluateNodes++;
            return pQueue.Dequeue();
        }

        protected void PushPQueue(State<T> s) {
            pQueue.Enqueue(s, s.Cost);
        }

        public bool PQcontain(State<T> s) {
            return pQueue.Contains(s);
        }

        public void UpdatePriority(State<T> s) {
            pQueue.UpdatePriority(s, s.Cost);
        }

        
    }
}
