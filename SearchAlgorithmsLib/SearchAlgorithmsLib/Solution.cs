using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public class Solution<T> {
        private List<State<T>> path;

        public Solution(List<State<T>> p) {
            path = p;
        }
    }
}