using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindBackend.Models;

namespace NorthwindBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {


        [HttpGet]
        public ActionResult SayHello() {
            NorthwindoriginalContext  db = new NorthwindoriginalContext();  // instanssi modelista
            var products = db.Products.ToList();
            return Ok(products);
        }


        [HttpGet("/shout/{feed}")]
        public ActionResult ShoutSomething(string feed)
        {
            return Ok(feed + "!!!!");
        }

    }
}



