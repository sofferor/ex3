using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server {
    class Move {

        //members
        private string name;
        private string direction;

        public Move(string name, string direction) {
            this.name = name;
            this.direction = direction;
        }

        public string ToJson() {
            JObject jMove = new JObject();
            jMove["Name"] = name;
            jMove["Direction"] = direction;
            return jMove.ToString();
        }
    }
}
