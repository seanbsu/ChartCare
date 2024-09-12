using ChartCareData.Models;
namespace ChartCareData.Services.FeaturesService
{
    public interface IFeatureService
    {
        /// <summary>
        /// Asynchronously retrieves a list of all unique features from the database. 
        /// The method counts each employee account's features as a single unique feature.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{Result{List{Features}}}"/> representing the asynchronous operation. 
        /// The task result contains a <see cref="Result{List{Features}}"/> object, which encapsulates 
        /// the result of the operation. The result will include a list of <see cref="Features"/> objects 
        /// that represent the unique features found in the database.
        /// </returns>
        Task<Result<List<FeaturesViewModel>>> GetAllUniqueFeatures();
    }
}
