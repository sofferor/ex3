using MazeLib;
using System;
using System.Collections.Generic;

namespace SearchAlgorithmsLib {
    public class State<T> {

        //members
        private T s;
        private float cost;
        private State<T> cameFrom;


        //properties
        public T S {
            get => s;
        }
        public float Cost {
            get => cost;
            set => cost = value;
        }
        public State<T> CameFrom {
            get => cameFrom;
            set => cameFrom = value;
        }

        //constructor.
        private State(T s) {
            this.s = s;
            cameFrom = null;
            cost = 0;
        }

        public bool Equals(State<T> other) {
            return s.Equals(other.s);
        }

        public override string ToString() {
            return s.ToString();
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        public void clean() {
            cameFrom = null;
            cost = 0;
        }

        public static class StatePool {

            static Dictionary<T, State<T>> sp = new Dictionary<T, State<T>>();

            public static State<T> GetState(T t) {

                if (sp.ContainsKey(t)) {//to varify if needed function equal to Position(expect of Object)
                    return sp[t];
                }

                State<T> s = new State<T>(t);
                sp.Add(t, s);
                return s;
            }
        }

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