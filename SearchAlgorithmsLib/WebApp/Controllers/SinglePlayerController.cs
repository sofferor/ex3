﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class SinglePlayerController : ApiController
    {
        // GET: api/SinglePlayer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SinglePlayer/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SinglePlayer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SinglePlayer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SinglePlayer/5
        public void Delete(int id)
        {
        }
    }
}
