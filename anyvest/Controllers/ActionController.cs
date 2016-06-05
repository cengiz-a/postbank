using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AnyVest.Models;

namespace AnyVest.Controllers
{
    [Route("api/[controller]")]
    public class ActionController : Controller
    {
        public IInvestmentRepository Investments;
        public IOfferRepository Offers;
        public ActionController(IInvestmentRepository investments, IOfferRepository offers) 
        {
            Investments = investments;
            Offers = offers;
        }
        
        public IEnumerable<Action> GetAll()
        {
            var a = new List<Action>();
            a.AddRange(Investments.GetAll());
            a.AddRange(Offers.GetAll());
            return a;
        }
        
        [HttpGet("{id}", Name = "GetAction")]
        public IActionResult GetById(string id)
        {
            var inv = Investments.Find(id);
            var off = Offers.Find(id);
            if(inv == null && off == null)
                return NotFound();
            if(off != null)
                return new ObjectResult(off);
            return new ObjectResult(inv);
        }
        
        [HttpGet("pending/query/{query}", Name = "GetPendingActionsByQuery")]
        public IEnumerable<Action> GetPendingByQuery(string query)
        {
            var a = new List<Action>();
            a.AddRange(Investments.GetPendingByQuery(query));
            a.AddRange(Offers.GetPendingByQuery(query)); 
            return a;
        }
        
        [HttpGet("pending", Name = "GetPendingActions")]
        public IEnumerable<Action> GetPending()
        {
            var a = new List<Action>();
            a.AddRange(Investments.GetPending());
            a.AddRange(Offers.GetPending()); 
            return a;
        }
        
        [HttpGet("query/{query}", Name = "GetActionsByQuery")]
        public IEnumerable<Action> GetByQuery(string query)
        {
            var a = new List<Action>();
            a.AddRange(Investments.Query(query));
            a.AddRange(Offers.Query(query));            
            return a;
        }
    }
}