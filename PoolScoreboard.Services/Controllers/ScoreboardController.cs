using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PoolScoreboard.Services.Controllers
{
    public class ScoreboardController : ApiController
    {
        public string Get()
        {
            return "Hello World";
        }
    }
}
