using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    public abstract class SearcherByStack<T> : Searcher<T> {

        protected Stack<State<T>> stack;

        //property size of queue.
        public int StackSize {
            get {
                return stack.Count;
            }
        }

        protected State<T> PopStack() {
            evaluateNodes++;
            return stack.Pop();
        }
    }
}
