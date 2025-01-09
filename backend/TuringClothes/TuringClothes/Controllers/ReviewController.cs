using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Security.Claims;
using TuringClothes.Database;
using TuringClothes.Dtos;
using TuringClothes.Repository;
using TuringClothes.Services;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly MyDatabase _context;
        private readonly ReviewService _reviewService;


        public ReviewController(MyDatabase context, ReviewService reviewService)
        {
            _context = context;
            _reviewService = reviewService;
        }


        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewResultDto>>> GetReviewsByProduct(long productId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .OrderByDescending(r => r.DateTime)
                .Select(r => new ReviewResultDto
                {
                    ProductId = r.ProductId,
                    Texto = r.Texto,
                    Name = r.User.Name,
                    Surname = r.User.Surname,
                    Rating = r.Rating,
                    DateTime = r.DateTime
                }).ToListAsync();

            return Ok(reviews);
        }


        [Authorize]
        [HttpPost("AddReview")]
        public async Task<ActionResult> AddReview([FromBody] ReviewDto reviewDto)
        {
            var userIdClaim = User.FindFirst("id")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("El usuario no está autenticado.");
            }
            var rating = await _reviewService.GetRatingReview(reviewDto.Texto);
            var review = new Review
            {
                ProductId = reviewDto.ProductId,
                Texto = reviewDto.Texto,
                Rating = rating,
                DateTime = DateTime.UtcNow,
                UserId = userId,
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(review);
        }
        
        [HttpGet("getAverageRating")]
        public async Task<ActionResult<double>> GetAverageRatingForProduct(int productId)
        {
            var averageRating = await _reviewService.GetAverageRatingForProduct(productId);
            return Ok(averageRating);
        }

        
    }

}