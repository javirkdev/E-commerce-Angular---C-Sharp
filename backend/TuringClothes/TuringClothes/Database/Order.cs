
using Stripe.Checkout;

namespace TuringClothes.Database
{
    public class Order
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string PaymentMethod { get; set; }
        public string? TransactionStatus { get; set; }
        public string Email { get; set; }
        public long? TotalPrice { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
