using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthTutorial.Resource.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthTutorial.Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStore Store;

        public BooksController(BookStore store)
        {
            Store = store;
        }
        [Route("")]
        [HttpGet]
        
        public IActionResult GetVailableBooks()
        {
            return Ok(Store.Books);
        }
    }
}
