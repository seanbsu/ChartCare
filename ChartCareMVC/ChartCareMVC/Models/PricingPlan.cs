using System.Diagnostics.CodeAnalysis;

namespace ChartCareMVC.Models
{
    public enum Plan
        {
            Free, Standard, Premium
        }
    public class PricingPlan
    {
        
        public  int ID { get; set; }
        public required Plan PlanName { get; set; }
        public  required string PlanNameString { get; set; }
        public required float PlanPrice { get; set; }

        
        public ICollection<Company>? Companies { get; set; }
    }
}
