using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Class SearcherByPriorityQueue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public abstract class SearcherByPriorityQueue<T> : Searcher<T> {

        /// <summary>
        /// The p queue
        /// </summary>
        protected SimplePriorityQueue<State<T>> pQueue;

        //property size of queue.
        /// <summary>
        /// Gets the size of the queue.
        /// </summary>
        /// <value>The size of the queue.</value>
        public int QueueSize {
            get {
                return pQueue.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearcherByPriorityQueue{T}"/> class.
        /// </summary>
        public SearcherByPriorityQueue() {
            pQueue = new SimplePriorityQueue<State<T>>();
        }

        /// <summary>
        /// Pops the p queue.
        /// </summary>
        /// <returns>State&lt;T&gt;.</returns>
        protected State<T> PopPQueue() {
            evaluateNodes++;
            return pQueue.Dequeue();
        }

        /// <summary>
        /// Pushes the p queue.
        /// </summary>
        /// <param name="s">The s.</param>
        protected void PushPQueue(State<T> s) {
            pQueue.Enqueue(s, s.Cost);
        }

        /// <summary>
        /// ps the qcontain.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool PQcontain(State<T> s) {
            return pQueue.Contains(s);
        }

        /// <summary>
        /// Updates the priority.
        /// </summary>
        /// <param name="s">The s.</param>
        public void UpdatePriority(State<T> s) {
            pQueue.UpdatePriority(s, s.Cost);
        }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        public override void clean() {
            base.clean();
            pQueue.Clear();
        }
    }
}
