using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public class State<T> {
        private T s;
        private float cost;
        private State<T> cameFrom;

        public float Cost {
            get => cost;
            set => cost = value;
        }

        public State<T> CameFrom {
            get => cameFrom;
            set => cameFrom = value;
        }

        public State(T s) {
            this.s = s;
            cameFrom = null;
        }

        public bool Equals(State<T> other) {
            return String.Intern(ToString()) == String.Intern(other.ToString());
        }

        public override string ToString() {
            return s.ToString();
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        public static class StatePool {
            p HashSet<State<T>> sp;
        }
    }
}