using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TagHelpers.Models;

namespace TagHelpers.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Add()
        {
            return View();
            //return View("AddWithTagHelpers");
        }

        [HttpPost]
        public IActionResult Add(Contact contact)
        {
            // TODO Save the contact.

            return RedirectToAction("Index", "Home");
        }
    }
}
