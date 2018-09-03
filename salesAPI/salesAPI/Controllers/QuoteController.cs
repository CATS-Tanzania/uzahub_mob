using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace salesAPI.Controllers
{
    public class QuoteController : ApiController
    {
        [ActionName("GetQuote")]
        public string GetQuote()
        {
            return "Hello NativeScript!";
        }
    }
}
