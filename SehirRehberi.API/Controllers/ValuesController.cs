using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SehirRehberi.API.Data;

namespace SehirRehberi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
   // [ApiController]
    public class ValuesController : ControllerBase
    {

        private DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult GetValues()
        {
            var values = _context.Values.ToList();
            return Ok(values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(v => v.Id == id);
            return Ok(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
