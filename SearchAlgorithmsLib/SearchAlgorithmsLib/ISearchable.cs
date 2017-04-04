using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public interface ISearchable {
        State<T> GetInitialState<T>();
        State<T> GetGoalState<T>();
        List<State<T>> GetAllPossibleStates<T>(State<T> s);

    }
}