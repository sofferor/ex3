using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public interface ISearchable<T> {

        State<T> getInitialState();

        State<T> getGoalState();

        List<State<T>> getAllPossibleStates(State<T> s);

        void Clean();
    }
}