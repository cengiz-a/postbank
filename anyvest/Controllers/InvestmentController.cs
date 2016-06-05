using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AnyVest.Models;

namespace AnyVest.Controllers
{
    [Route("api/action/[controller]")]
    public class InvestmentController : Controller
    {
        public IInvestmentRepository Investments;
        public IOfferRepository Offers;
        public InvestmentController(IInvestmentRepository investments, IOfferRepository offers) 
        {
            Investments = investments;
            Offers = offers;
        }
        
        public IEnumerable<Investment> GetAll()
        {
            return Investments.GetAll();
        }
        
        [HttpGet("{id}", Name = "GetInvestment")]
        public IActionResult GetById(string id)
        {
            var inv = Investments.Find(id);
            if(inv == null)
                return NotFound();
            return new ObjectResult(inv);
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] Investment inv)
        {
            if(inv == null)
                BadRequest();
            
            // TODO ein paar Berechnungen bzgl. des Crowdshare und Prüfung auf Abschluss.
            if(inv.Initiative)
            {
                if(inv.Name == string.Empty)
                    return BadRequest();
                return CreatedAtRoute("GetInvestment", new {controller = "Investment", id = inv.Id}, inv); 
            }
            else 
            {
                var o = Offers.Find(inv.OfferId);
                if(o != null){
                    // TODO Berechnungen für nicht Initiative
                    if(inv.Name == string.Empty)
                    {
                        var l = Investments.GetPendingByQuery(o.Name);
                        inv.Name = o.Name + " - Investment-#" + (l.Count()+1);
                        inv.InvestmentGoal = o.InvestmentGoal;
                        inv.LeastCrowdFraction = inv.Amount / o.InvestmentGoal * o.CrowdShare;
                        o.TotalCrowdInvestment += inv.Amount;
                    }
                    Investments.Add(inv);
                    return CreatedAtRoute("GetInvestment", new {controller = "Investment", id = inv.Id}, inv); 
                }
                    
            }
            return NotFound();   
        }
        
        [HttpPut("{id}")]
        public IActionResult Update (string id, [FromBody] Investment investment)
        {
            if(investment == null || id != investment.Id)
                return BadRequest();
            var inv = Investments.Find(id);
            if(inv == null)
                return NotFound();
            
            inv.Amount = investment.Amount;
            if(inv.Initiative){
                inv.LeastCrowdFraction = investment.LeastCrowdFraction;
                inv.Name = investment.Name;
                inv.InvestmentGoal = investment.InvestmentGoal;
            }
            
            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var i = Investments.Remove(id);
            var o = Offers.Find(i.OfferId);
            if(o != null)
                o.TotalCrowdInvestment -= i.Amount;
        }
    }
}