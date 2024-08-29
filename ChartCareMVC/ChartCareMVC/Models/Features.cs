using ChartCareMVC.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace ChartCareMVC.Models
{
    public class Features
    {

        public required int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }

        public virtual ICollection<PlanFeatures> PlanFeatureLinks { get; set; } = null!;
    }
}
