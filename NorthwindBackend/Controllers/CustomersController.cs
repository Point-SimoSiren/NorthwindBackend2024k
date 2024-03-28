﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindBackend.Models;

namespace NorthwindBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Otetaan tietokantatoiminto käyttöön koko Controllerin tasolla
        NorthwindoriginalContext db = new NorthwindoriginalContext();

        // Metodi joka hakee kaikki asiakkaat
        // polku olisi: https://sivun-osoite.com/api/customers
        [HttpGet]
        public ActionResult GetAll()
        {
            var asiakkaat = db.Customers.ToList();
            return Ok(asiakkaat);
        }


        // Hae nimen osalla
        // polku olisi: https://sivun-osoite.com/api/customers/company/{hakusana}

        [HttpGet("company/{hakusana}")]
        public ActionResult GetByCompanyName(string hakusana)
        {
            var asiakkaat = db.Customers.Where(c => c.CompanyName.Contains(hakusana));
            // var asiakkaat = db.Customers.Where(c => c.CompanyName == hakusana); <--- perfect match

            return Ok(asiakkaat);
        }


        // Hae taulun pääavaimella eli id:llä
        // polku olisi esim: https://sivun-osoite.com/api/customers/ALFKI
        [HttpGet("{id}")]
        public ActionResult GetById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if (asiakas != null)
                {
                    return Ok(asiakas);
                }
                else
                {
                    return NotFound($"Id:llä {id} ei löytynyt yhtään asiakasta.");
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.InnerException);
            }
        }

    }
}
