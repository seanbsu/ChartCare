using ChartCareData.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ChartCareData.Models
{
    public class PlanFeatures
    {
        [Required]
        public required int FeatureId { get; set; }
        [Required]
        public required int PlanId { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public virtual Features Feature { get; set; } = null!;
        [ForeignKey(nameof(PlanId))]
        public virtual PricingPlan PricingPlan { get; set; } = null!;
    }
}
