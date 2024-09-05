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
        public  string? AbbreviatedDescription { get; set; }

        public virtual ICollection<PlanFeatures> PlanFeatureLinks { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is Features other)
            {
                return Name == other.Name &&
                       Description == other.Description &&
                       AbbreviatedDescription == other.AbbreviatedDescription;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, AbbreviatedDescription);
        }
    }
}
