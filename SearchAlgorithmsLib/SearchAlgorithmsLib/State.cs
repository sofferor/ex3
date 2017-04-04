﻿namespace SearchAlgorithmsLib {
    public class State<T> {
        private T state;
        private double cost;
        private State<T> cameFrom;

        public State(T state) {
            this.state = state;
        }

        public bool Equals(State<T> s) {
            return state.Equals(s.state);
        }


    }
}