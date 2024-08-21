using System.ComponentModel.DataAnnotations;

namespace ChartCareMVC.Models
{
    public class PlanModel
    {
        [Required]
        public required string PlanID { get; set; }

        public string? Description { get; set; }

        public float? MonthlyCost { get; set; }

        public virtual ICollection<CompaniesModel>? Companies { get; set; }
    }
}
