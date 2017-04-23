using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// Class SearcherByStack.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.Searcher{T}" />
    public abstract class SearcherByStack<T> : Searcher<T> {

        //members
        /// <summary>
        /// The stack
        /// </summary>
        protected Stack<State<T>> stack;

        //properties
        /// <summary>
        /// Gets the size of the stack.
        /// </summary>
        /// <value>The size of the stack.</value>
        public int StackSize {
            get {
                return stack.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearcherByStack{T}"/> class.
        /// </summary>
        public SearcherByStack() {
            stack = new Stack<State<T>>();
        }

        /// <summary>
        /// Pops the stack.
        /// </summary>
        /// <returns>State&lt;T&gt;.</returns>
        protected State<T> PopStack() {
            evaluateNodes++;
            return stack.Pop();
        }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        public override void clean() {
            base.clean();
            stack.Clear();
        }
    }
}
