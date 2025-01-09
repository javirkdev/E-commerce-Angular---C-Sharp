using System.ComponentModel.DataAnnotations.Schema;

namespace TuringClothes.Database
{

    public class Review
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public long UserId { get; set; }

        public string Texto { get; set; }

        public DateTime DateTime { get; set; }

        public int Rating { get; set; }

        public User User { get; set; }
    }
}
