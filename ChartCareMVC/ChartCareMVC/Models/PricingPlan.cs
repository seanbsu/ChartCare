﻿using System.Diagnostics.CodeAnalysis;

namespace ChartCareMVC.Models
{
    public enum Plan
        {
            Free, Standard, Premium
        }
    public class PricingPlan
    {
        
        public int ID { get; set; }
        public Plan PlanName { get; set; }
        public  required string PlanNameString { get; set; }
        public float PlanPrice { get; set; }

        
        public ICollection<Company>? Companies { get; set; }
    }
}
