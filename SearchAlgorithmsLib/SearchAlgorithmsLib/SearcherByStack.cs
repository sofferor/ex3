using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    public abstract class SearcherByStack<T> : Searcher<T> {

        //members
        protected Stack<State<T>> stack;

        //properties
        public int StackSize {
            get {
                return stack.Count;
            }
        }

        public SearcherByStack() {
            stack = new Stack<State<T>>();
        }

        protected State<T> PopStack() {
            evaluateNodes++;
            return stack.Pop();
        }

        public override void clean() {
            base.clean();
            stack.Clear();
        }
    }
}
