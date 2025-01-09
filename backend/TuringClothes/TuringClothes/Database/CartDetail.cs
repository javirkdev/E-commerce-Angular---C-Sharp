namespace TuringClothes.Database
{
    public class CartDetail
    {
        public long Id { get; set; }
        public long? CartId { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public int Amount { get; set; }

    }
}
