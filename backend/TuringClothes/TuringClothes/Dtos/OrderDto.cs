
using Stripe.Checkout;

namespace TuringClothes.Dtos
{
    public class OrderDto
    {
        public long OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public int Total { get; set; }
    }
}
