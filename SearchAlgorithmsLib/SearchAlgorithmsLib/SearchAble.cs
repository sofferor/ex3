using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    public abstract class Searchable<T> : ISearchable<T> {

        private State<T> initialState;
        private State<T> goalState;

        public Searchable(State<T> initial, State<T> goal) {
            initialState = initial;
            goalState = goal;
        }

        public State<T> getInitialState() {
            return initialState;
        }

        public State<T> getGoalState() {
            return goalState;
        }

        public abstract List<State<T>> getAllPossibleStates(State<T> s);
    }
}
