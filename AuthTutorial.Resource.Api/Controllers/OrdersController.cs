using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthTutorial.Resource.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthTutorial.Resource.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly BookStore Store;
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public OrdersController(BookStore store)
        {
            Store = store;
        }
        [HttpGet]
        [Authorize(Roles = "User"   )]
        [Route("")]
        public IActionResult GetVailableBooks()
        {
           if(!Store.Orders.ContainsKey(UserId)) return Ok(Enumerable.Empty<Book>());

            var orderedBookId = Store.Orders.Single(o => o.Key == UserId).Value;
            var orderedBooks = Store.Books.Where(b => orderedBookId.Contains(b.Id));

            return Ok(orderedBooks);
        }
    }
}
