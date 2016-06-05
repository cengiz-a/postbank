using System;

namespace AnyVest.Models {
    
    public class Action{
        public string Id {get; set;}
        public string Name {get; set;}
        public DateTime InsertionDate {get; set;}
        public double TotalCrowdInvestment {get; set;}
        public double InvestmentGoal {get;set;}
        public bool Completed {get;set;}
        
    }
}