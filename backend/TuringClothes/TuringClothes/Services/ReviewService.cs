using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ML;
using TuringClothes.Database;

namespace TuringClothes.Services
{
    public class ReviewService
    {
        private readonly MyDatabase _myDatabase;
        private readonly PredictionEnginePool<ModelInput, ModelOutput> _predictionEnginePool;


        public ReviewService(MyDatabase context, PredictionEnginePool<ModelInput, ModelOutput> predictionEnginePool)
            {
                _myDatabase = context;
                
                _predictionEnginePool = predictionEnginePool;
            }

        public async Task<double> GetAverageRatingForProduct(int productId)
        {
            var reviews = await _myDatabase.Reviews
        .Where(r => r.ProductId == productId)
        .ToListAsync();

            if (!reviews.Any())
            {
                return 0;
            }
            var averageRating = reviews.Average(r => r.Rating);

            var starRating = (averageRating + 1) + 2.5;

            if (starRating < 0) starRating = 0;
            if (starRating > 5) starRating = 5;

            return starRating;
        }
            public async Task<int> GetRatingReview(string reviewText)
            {
                var input = new ModelInput { Text = reviewText };
                var result = _predictionEnginePool.Predict(input);

            return (int)Math.Round(result.PredictedLabel);
            }

        
    }

    }

