using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChartCareMVC.Models
{
    public class CompaniesModel
    {
        [Required]
        public required string CompanyId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        [ForeignKey("Plan")]
        public string? PlanId { get; set; }

        public virtual PlanModel? Plan { get; set; }

        public string? AdminID {  get; set; }
    }
}
