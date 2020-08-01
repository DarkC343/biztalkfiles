using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SquareThis.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SquareController : ControllerBase
    {
        [HttpGet("{n}")]
        public ActionResult<int> SquareThisNumber(int n)
        {
            return Ok(n * n);
        }
    }
}