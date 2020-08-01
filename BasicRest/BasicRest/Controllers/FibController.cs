using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicRest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasicRest.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FibController : ControllerBase
    {
        private static BasicContext db = new BasicContext();
        [HttpGet("all")]
        public ActionResult<IEnumerable<BasicFib>> GetAllFibs()
        {
            var responses =
                from record in db.BasicFib
                select new BasicFib { BasicFibId = record.BasicFibId, Input = record.Input, Fib = record.Fib };
            if (responses != null) return Ok(responses.ToArray());
            else
                return BadRequest(new BasicResponseTemplate
                {
                    Success = false,
                    Error = "Empty list"
                });
        }

        [HttpPost("new")]
        public ActionResult<int> NewFib([FromBody] int n)
        {
            var foundFib = db.BasicFib.Where(c => c.Input == n).FirstOrDefault();
            if (foundFib == null)
            {
                int result = GetNthFib_func(n);
                var newFib = new BasicFib { Input = n, Fib = result };
                db.BasicFib.Add(newFib);
                db.SaveChanges();
                return Ok(result);
            }
            return Ok(foundFib.Fib);
        }

        [HttpGet("{n}")]
        public ActionResult<int> GetFib(int n)
        {
            var foundFib = db.BasicFib.Where(c => c.Input == n).FirstOrDefault();
            if (foundFib != null)
            {
                return Ok(foundFib.Fib);
            }
            else return BadRequest(new BasicResponseTemplate
            {
                Success = false,
                Error = "Not computed yet"
            });
        }

        public static int GetNthFib_func(int n)
        {
            int number = n - 1;
            int[] Fib = new int[number + 1];
            Fib[0] = 0;
            Fib[1] = 1;
            for (int i = 2; i <= number; i++)
            {
                Fib[i] = Fib[i - 2] + Fib[i - 1];
            }
            return Fib[number];
        }
    }
}