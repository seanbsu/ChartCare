namespace ChartCareMVC.Models
{
    public class FeaturesViewModel
    {
        public IEnumerable<Features> Features { get; set; } = new List<Features>();

        public required IEnumerable<string> FeatureList { get;  set; } 
      
    }

}
