using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// Class Searchable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="SearchAlgorithmsLib.ISearchable{T}" />
    public abstract class Searchable<T> : ISearchable<T> {
        /// <summary>
        /// The initial state
        /// </summary>
        private State<T> initialState;
        /// <summary>
        /// The goal state
        /// </summary>
        private State<T> goalState;

        /// <summary>
        /// Initializes a new instance of the <see cref="Searchable{T}"/> class.
        /// </summary>
        /// <param name="initialState">The initial state.</param>
        /// <param name="goalState">State of the goal.</param>
        public Searchable(State<T> initialState, State<T> goalState) {
            this.initialState = initialState;
            this.goalState = goalState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Searchable{T}"/> class.
        /// </summary>
        public Searchable() {
            initialState = null;
            goalState = null;
        }

        //properties
        /// <summary>
        /// Gets or sets the initial state.
        /// </summary>
        /// <value>The initial state.</value>
        public State<T> InitialState {
            get => initialState;
            set => initialState = value;
        }
        /// <summary>
        /// Gets or sets the state of the goal.
        /// </summary>
        /// <value>The state of the goal.</value>
        public State<T> GoalState {
            get => goalState;
            set => goalState = value;
        }

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>State&lt;T&gt;.</returns>
        public State<T> getInitialState() {
            return initialState;
        }

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns>State&lt;T&gt;.</returns>
        public State<T> getGoalState() {
            return goalState;
        }

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>List&lt;State&lt;T&gt;&gt;.</returns>
        public abstract List<State<T>> getAllPossibleStates(State<T> s);

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        public abstract void Clean();
    }
}
