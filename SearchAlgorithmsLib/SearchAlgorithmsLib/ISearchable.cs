using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// Interface ISearchable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearchable<T> {

        /// <summary>
        /// Gets the initial state.
        /// </summary>
        /// <returns>State&lt;T&gt;.</returns>
        State<T> getInitialState();

        /// <summary>
        /// Gets the state of the goal.
        /// </summary>
        /// <returns>State&lt;T&gt;.</returns>
        State<T> getGoalState();

        /// <summary>
        /// Gets all possible states.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>List&lt;State&lt;T&gt;&gt;.</returns>
        List<State<T>> getAllPossibleStates(State<T> s);

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        void Clean();
    }
}