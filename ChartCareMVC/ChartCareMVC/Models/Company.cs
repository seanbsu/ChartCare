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
        public int PlanID { get; set; }
        [Required]
        public required string  Email { get; set; }

        [Required]
        public required PricingPlan PricingPlan { get; set; }
        
        public  ICollection<CompanyUser> CompanyUsers { get; set; } = new List<CompanyUser>();
    }
}
