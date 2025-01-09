namespace TuringClothes.Dtos
{
    public class CheckTransactionRequest
    {
        public string Hash { get; set; }
        public long TemporaryOrderId { get; set; }
        public string Wallet { get; set; }
        public string PaymentMethod { get; set; }
    }
}
