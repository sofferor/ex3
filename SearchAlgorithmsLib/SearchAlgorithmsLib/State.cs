namespace SearchAlgorithmsLib {
    public class State<T> {
        private T state;
        private float cost;
        private State<T> cameFrom;

        public float Cost {
            get => cost;
        }

        public State(T state) {
            this.state = state;
        }

        public bool Equals(State<T> s) {
            return state.Equals(s.state);
        }

    }
}