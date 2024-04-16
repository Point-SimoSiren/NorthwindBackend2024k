using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindBackend.Models;
using System.Diagnostics.Eventing.Reader;

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


        // Uuden asiakkaan lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Customer customer)
        {
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return Ok("Lisättiin asiakas: " + customer.CompanyName);
            }
            catch (Exception ex)
            {
                return BadRequest("Jotain meni pieleen. Lue lisää: " + ex.InnerException);
            }
        }

        // Poistotoiminnon toteuttaminen
        [HttpDelete("{id}")]
        public ActionResult DeleteById(string id) {
            var asiakas = db.Customers.Find(id); // asiakas poimitaan käsittelyyn
            if (asiakas != null)
            {
                db.Customers.Remove(asiakas); // poisto
                db.SaveChanges(); // tallennus

                //return Ok("Poistettu asiakas " + asiakas.CompanyNam);
                return Ok($"Poistettu asiakas {asiakas.CompanyName}");
            }
            return NotFound("Asiakasta id:llä " + id + " ei löydy.");
            }


        // Muokkaustoiminto
        [HttpPut("{id}")]
        public ActionResult EditById(string id, [FromBody] Customer cust)
        {
            var asiakas = db.Customers.Find(id); // asiakas poimitaan käsittelyyn
            if (asiakas != null)
            {
                // Asetetaan ominaisuuksien arvot asikkaalle
                asiakas.CompanyName = cust.CompanyName;
                asiakas.ContactName = cust.ContactName;
                asiakas.Address = cust.Address;
                asiakas.City = cust.City;
                asiakas.Country = cust.Country;
                asiakas.Phone = cust.Phone;
                asiakas.PostalCode = cust.PostalCode;
                asiakas.Region = cust.Region;
                asiakas.Fax = cust.Fax;
                asiakas.ContactTitle = cust.ContactTitle;
                
                db.SaveChanges(); // tallennus

                //return Ok("Poistettu asiakas " + asiakas.CompanyNam);
                return Ok($"Muokattu asiakasta {asiakas.CompanyName}");
            }
            return NotFound("Asiakasta id:llä " + id + " ei löydy.");

        }





    }
}
