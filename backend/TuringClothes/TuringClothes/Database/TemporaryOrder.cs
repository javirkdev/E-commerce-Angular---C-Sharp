using TuringClothes.Dtos;

namespace TuringClothes.Database
{
    public class TemporaryOrder
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public ICollection<TemporaryOrderDetail> Details { get; set; }
        public string? Wallet {  get; set; }
        public double? EthereumPrice { get; set; }
        public string HexEthereumPrice { get; set; }


        public int TotalPriceEur { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
