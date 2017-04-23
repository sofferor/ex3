namespace SearchAlgorithmsLib {
    /// <summary>
    /// Interface ISearcher
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISearcher<T> {

        // the search method
        /// <summary>
        /// Searches the specified searchable.
        /// </summary>
        /// <param name="searchable">The searchable.</param>
        /// <returns>Solution&lt;T&gt;.</returns>
        Solution<T> Search(ISearchable<T> searchable);

        // get how many nodes were evaluated by the algorithm
        /// <summary>
        /// Gets the number of nodes evaluated.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int GetNumberOfNodesEvaluated();

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        void clean();
    }
}