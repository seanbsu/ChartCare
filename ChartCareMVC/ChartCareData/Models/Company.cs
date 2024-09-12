using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using ChartCareData.Identity.Data;


namespace ChartCareData.Models
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
        public int PlanID { get; set; }
        [Required]
        public required string  Email { get; set; }

        [ForeignKey(nameof(PlanID))]
        public virtual PricingPlan PricingPlan { get; set; } = null!;
        
        public virtual ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
    }
}
