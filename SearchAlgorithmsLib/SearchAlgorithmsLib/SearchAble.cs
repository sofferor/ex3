using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib {
    public abstract class Searchable<T> : ISearchable<T> {
        public State<T> getInitialState() {
            throw new NotImplementedException();
        }

        public State<T> getGoalState() {
            throw new NotImplementedException();
        }

        public List<State<T>> getAllPossibleStates(State<T> s) {
            throw new NotImplementedException();
        }
    }
}
