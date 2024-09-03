using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using ChartCareMVC.Areas.Identity.Data;


namespace ChartCareMVC.Models
{
    public class Company
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Address { get; set; }
        [Required]
        public int PricingPlanID { get; set; }
        [Required]
        public required string  Email { get; set; }

        [ForeignKey(nameof(PricingPlanID))]
        public virtual PricingPlan PricingPlan { get; set; } = null!;
        
        public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
    }
}
