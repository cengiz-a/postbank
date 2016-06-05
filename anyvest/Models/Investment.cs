using System.Collections.Generic;

namespace AnyVest.Models {
    public class Investment : Action {
        
        public string OfferId {get; set;}
        public bool Initiative {get; set;}
        public double Amount {get; set;}
        public double LeastCrowdFraction {get; set;}
        public string CrowdUser {get; set;}
        public bool PaymentReceived {get; set;}
    }
    
    public interface IInvestmentRepository
    {
        void Add(Investment inv);
        IEnumerable<Investment> GetAll();
        Investment Find(string id);
        IEnumerable<Investment> Query(string q);
        Investment Remove(string id);
        IEnumerable<Investment> GetPending();
        IEnumerable<Investment> GetPendingByQuery(string q);
        void Update(Investment id);
    }
}