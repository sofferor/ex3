using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public abstract class Searchable<T> : ISearchable<T> {
        public State<T> getInitialState() {
            throw new System.NotImplementedException();
        }

        public State<T> getGoalState() {
            throw new System.NotImplementedException();
        }

        public List<State<T>> getAllPossibleStates(State<T> s) {
            throw new System.NotImplementedException();
        }
    }
}