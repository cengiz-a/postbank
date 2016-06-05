using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnyVest.Models;

namespace AnyVest.Controllers
{
    [Route("api/action/[controller]")]
    public class OfferController : Controller
    {
        public IOfferRepository Offers;
        
        public OfferController(IOfferRepository offers)
        {
            Offers = offers;
        }
        
        public IEnumerable<Offer> GetAll()
        {
            return Offers.GetAll();
        }
        
        [HttpGet("{id}", Name = "GetOffer")]
        public IActionResult GetById(string id)
        {
            var off = Offers.Find(id);
            if(off == null)
                return NotFound();
            return new ObjectResult(off);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] Offer off)
        {
            if(off == null)
                BadRequest();
            
            // TODO ein paar Berechnungen bzgl. des Crowdshare und Prüfung auf Abschluss.
            Offers.Add(off);
            return CreatedAtRoute("GetOffer", new {controller = "Offer", id = off.Id}, off);   
        }
        
        [HttpPut("{id}")]
        public IActionResult Update (string id, [FromBody] Offer off)
        {
            if(off == null || id != off.Id)
                return BadRequest();
            var offer = Offers.Find(id);
            if(offer == null)
                return NotFound();
            offer.Name = off.Name;
            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Offers.Remove(id);
        }
    }
}