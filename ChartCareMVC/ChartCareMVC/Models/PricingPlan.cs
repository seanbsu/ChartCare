using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChartCareMVC.Models
{
    public enum Plan
        {
            Free, Standard, Premium
        }
    public class PricingPlan
    {
        
        public  int ID { get; set; }
        [Required]
        public required Plan PlanName { get; set; }
        [Required]
        public  required string PlanNameString { get; set; }
        [Required]
        public required float PlanPrice { get; set; }

        
        public virtual ICollection<Company>? Companies { get; set; }
        public virtual ICollection<PlanFeatures> PlanFeatureLinks { get; set; } = new List<PlanFeatures>();
    }
}
