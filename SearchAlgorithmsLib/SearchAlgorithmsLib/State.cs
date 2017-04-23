using MazeLib;
using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    /// <summary>
    /// Class State.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T> {

        //members
        /// <summary>
        /// The s
        /// </summary>
        private T s;
        /// <summary>
        /// The cost
        /// </summary>
        private float cost;
        /// <summary>
        /// The came from
        /// </summary>
        private State<T> cameFrom;


        //properties
        /// <summary>
        /// Gets the s.
        /// </summary>
        /// <value>The s.</value>
        public T S {
            get => s;
        }
        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>The cost.</value>
        public float Cost {
            get => cost;
            set => cost = value;
        }
        /// <summary>
        /// Gets or sets the came from.
        /// </summary>
        /// <value>The came from.</value>
        public State<T> CameFrom {
            get => cameFrom;
            set => cameFrom = value;
        }

        //constructor.
        /// <summary>
        /// Initializes a new instance of the <see cref="State{T}"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        private State(T s) {
            this.s = s;
            cameFrom = null;
            cost = 0;
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(State<T> other) {
            return s.Equals(other.s);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() {
            return s.ToString();
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        public void clean() {
            cameFrom = null;
            cost = 0;
        }

        /// <summary>
        /// Class StatePool.
        /// </summary>
        public static class StatePool {

            /// <summary>
            /// The sp
            /// </summary>
            static Dictionary<T, State<T>> sp = new Dictionary<T, State<T>>();

            /// <summary>
            /// Gets the state.
            /// </summary>
            /// <param name="t">The t.</param>
            /// <returns>State&lt;T&gt;.</returns>
            public static State<T> GetState(T t) {

                if (sp.ContainsKey(t)) {//to varify if needed function equal to Position(expect of Object)
                    return sp[t];
                }

                State<T> s = new State<T>(t);
                sp.Add(t, s);
                return s;
            }
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() {
            string j;
            Position pos = (Position) (Object) s;

            if (cameFrom == null) {
                j = "";
            }
            else {

                Position cameFromPos = (Position)(Object)cameFrom.S;

                //if this state has no dad it is the initial state.
                if (pos.Col < cameFromPos.Col) {
                    //go left
                    j = "0";
                } else if (pos.Col > cameFromPos.Col) {
                    //go right
                    j = "1";
                } else if (pos.Row > cameFromPos.Row) {
                    //go up
                    j = "2";
                } else {
                    //go down
                    j = "3";
                }
            }
            

            
            return j;
        }
    }
}