using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    public abstract class Searchable<T> : ISearchable<T> {
        private State<T> initialState;
        private State<T> goalState;

        protected Searchable(State<T> initialState, State<T> goalState) {
            this.initialState = initialState;
            this.goalState = goalState;
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
