using TuringClothes.Database;

namespace TuringClothes.Dtos
{
    public class ReviewResultDto
    {
        public long ProductId { get; set; }
        public string Texto { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateTime { get; set; }
        public int Rating { get; set; }
    }
}
