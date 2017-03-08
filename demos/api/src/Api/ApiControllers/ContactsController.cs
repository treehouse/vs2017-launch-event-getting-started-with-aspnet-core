using Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.ApiControllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private static List<Contact> _contacts = new List<Contact>()
        {
            new Contact() { Id = 1, Name = "Joe Smith", Email = "joe@smith.com", Phone = "555-555-5555" },
            new Contact() { Id = 2, Name = "John Smith", Email = "john@smith.com", Phone = "555-555-5555" },
            new Contact() { Id = 3, Name = "Sally Smith", Email = "sally@smith.com", Phone = "555-555-5555" },
        };

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_contacts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            contact.Id = _contacts.Count + 1;

            _contacts.Add(contact);

            return CreatedAtAction("Get", new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Contact contact)
        {
            if (contact == null)
            {
                return BadRequest();
            }

            var index = _contacts.FindIndex(c => c.Id == id);
            if (index == -1)
            {
                return NotFound();
            }

            _contacts[index] = contact;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var index = _contacts.FindIndex(c => c.Id == id);
            if (index == -1)
            {
                return NotFound();
            }

            _contacts.RemoveAt(index);

            return NoContent();
        }
    }
}
