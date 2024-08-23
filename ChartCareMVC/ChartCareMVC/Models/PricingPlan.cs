using System.Diagnostics.CodeAnalysis;

namespace ChartCareMVC.Models
{
    public enum Plan
        {
            Free, Standard, Premium
        }
    public class PricingPlan
    {
        
        public int ID { get; set; }
    }
}
