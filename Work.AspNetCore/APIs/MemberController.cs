using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Work.AspNetCore.APIs
{
    [Produces("application/json")]
    [Route("api/Member")]
    public class MemberController : Controller
    {

        public void MyMethod()
        {
            throw new NotImplementedException();
        }
    }
}