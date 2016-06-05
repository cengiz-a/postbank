using System.Collections.Generic;

namespace AnyVest.Models
{
    public class Offer : Action {
        public double CrowdShare {get;set;}
        public string Owner {get; set;}
    }
    
    public interface IOfferRepository
    {
        void Add(Offer inv);
        IEnumerable<Offer> GetAll();
        Offer Find(string id);
        IEnumerable<Offer> Query(string q);
        IEnumerable<Offer> GetPending();
        IEnumerable<Offer> GetPendingByQuery(string q);
        Offer Remove(string id);
        void Update(Offer id);
    }
}