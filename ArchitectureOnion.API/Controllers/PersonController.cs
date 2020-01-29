using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchitectureOnion.Logic;
using ArchitectureOnion.Logic.Model;
using ArchitectureOnion.Logic.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectureOnion.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET: api/Person
        //[HttpGet]
        //public async Task<IEnumerable<Person>> Get()
        //{
        //}

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Person> Get(int id)
        {
            return await _personService.GetPerson(id);
        }

        // POST: api/Person
        //[HttpPost]
        //public async Task<int> Post([FromBody] Person person)
        //{
        //}

        // PUT: api/Person/5
        //[HttpPut("{id}")]
        //public async Task<int> Put(int id, [FromBody] Person person)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public async Task<int> Delete(int id)
        //{
        //}
    }
}
