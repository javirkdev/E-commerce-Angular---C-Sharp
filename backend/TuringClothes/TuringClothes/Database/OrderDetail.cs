namespace TuringClothes.Database
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public int Amount { get; set; }
        public int ProductPrice { get; set; }
    }
}
