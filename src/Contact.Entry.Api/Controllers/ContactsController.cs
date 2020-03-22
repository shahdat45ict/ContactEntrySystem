using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactEntry.Api.Model;
using ContactEntry.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactEntry.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: /Contacts 
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return _contactService.FindAll();
        }

        // GET: /Contacts/5
        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id)
        {
            var contact =  _contactService.FindOne(id);
            if (contact == null)
                return NotFound();
            return contact;
        }

        // PUT: /Contacts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public IActionResult Put(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }
            _contactService.Update(contact);
            return NoContent();
        }

        // POST: /Contacts
        [HttpPost]
        public ActionResult<Contact> Post(Contact contact)
        {
            _contactService.Insert(contact);
            return CreatedAtAction("Get", new { id = contact.Id }, contact);
        }

        // DELETE: /Contacts/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
           bool deleted =  _contactService.Delete(id);
            if (deleted)
                return NoContent();
            else
                return NotFound();
        }
    }
}
