using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    public abstract class Searchable<T> : ISearchable<T> {
        private State<T> initialState;
        private State<T> goalState;

        public Searchable(State<T> initialState, State<T> goalState) {
            this.initialState = initialState;
            this.goalState = goalState;
        }

        public Searchable() {
            initialState = null;
            goalState = null;
        }

        //properties
        public State<T> InitialState {
            get => initialState;
            set => initialState = value;
        }
        public State<T> GoalState {
            get => goalState;
            set => goalState = value;
        }
      
        public State<T> getInitialState() {
            return initialState;
        }

        public State<T> getGoalState() {
            return goalState;
        }

        public abstract List<State<T>> getAllPossibleStates(State<T> s);

        public abstract void Clean();
    }
}
