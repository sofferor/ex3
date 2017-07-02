using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebApp.Models {
    /// <summary>
    /// Class Move.
    /// </summary>
    public class Move {

        //members
        /// <summary>
        /// The name
        /// </summary>
        private string name;
        /// <summary>
        /// The direction
        /// </summary>
        private string direction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Move"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="direction">The direction.</param>
        public Move(string name, string direction) {
            this.name = name;
            this.direction = direction;
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJson() {
            JObject jMove = new JObject();
            jMove["Name"] = name;
            jMove["Direction"] = direction;
            return jMove.ToString();
        }

        public string getMazeName() {
            return name;
        }
    }
}