using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AnyVest.Models
{
    public class InvestmentRepository : IInvestmentRepository
    {
        static ConcurrentDictionary<string, Investment> _investments = new ConcurrentDictionary<string, Investment>();
        
        public InvestmentRepository()
        {
            var i = new Investment {
                Name = "Test",
                Amount = 1.00,
                CrowdUser = "Tester"
            };
            var y = new Investment {
                Name = "Test2",
                Amount = 1.00,
                CrowdUser = "Krümelmonster"
            };
            Add(i);
            Add(y);
        }
        
        public void Add(Investment inv)
        {
            inv.Id = Guid.NewGuid().ToString();
            _investments[inv.Id] = inv;
        }

        public Investment Find(string id)
        {
            Investment i;
            _investments.TryGetValue(id, out i);
            return i;
        }

        public IEnumerable<Investment> GetAll()
        {
            return _investments.Values;
        }

        public IEnumerable<Investment> Query(string q)
        {
            return _investments.Values.Where((i) => i.Name.Contains(q.ToLower()));
        }

        public Investment Remove(string id)
        {
            Investment i;
            _investments.TryRemove(id, out i);
            return i;
        }

        public void Update(Investment inv)
        {
            _investments[inv.Id] = inv;
        }
        
        public IEnumerable<Investment> GetPending()
        {
            return _investments.Values.Where((i) => i.Completed == false);
        }
        
        public IEnumerable<Investment> GetPendingByQuery(string q)
        {
            return _investments.Values.Where((i) => i.Completed == false && i.Name.Contains(q.ToLower()));
        }
    }

    public class OfferRepository : IOfferRepository
    {
        static ConcurrentDictionary<string, Offer> _offers = new ConcurrentDictionary<string, Offer>();
        public void Add(Offer offer)
        {
            offer.Id = Guid.NewGuid().ToString();
            _offers[offer.Id] = offer;
        }

        public Offer Find(string id)
        {
            Offer o;
            _offers.TryGetValue(id, out o);
            return o;
        }

        public IEnumerable<Offer> GetAll()
        {
            return _offers.Values;
        }

        public Offer Remove(string id)
        {
            Offer o;
            _offers.TryRemove(id, out o);
            return o;
        }

        public void Update(Offer offer)
        {
            _offers[offer.Id] = offer;
        }

        public IEnumerable<Offer> Query(string q)
        {
            return _offers.Values.Where((o) => o.Name.Contains(q.ToLower()));
        }
        
        public IEnumerable<Offer> GetPending()
        {
            return _offers.Values.Where((o) => o.Completed == false);
        }
        
        public IEnumerable<Offer> GetPendingByQuery(string q)
        {
            return _offers.Values.Where((o) => o.Completed == false && o.Name.Contains(q.ToLower()));
        }
    }
}