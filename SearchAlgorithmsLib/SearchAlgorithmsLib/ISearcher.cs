namespace SearchAlgorithmsLib {
    public interface ISearcher {
        // the search method
        Solution Search(ISearchable searchable);
        // get how many nodes were evaluated by the algorithm
        int GetNumberOfNodesEvaluated();
    }
}