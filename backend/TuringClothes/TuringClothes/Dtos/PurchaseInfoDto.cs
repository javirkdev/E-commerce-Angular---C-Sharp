using TuringClothes.Database;

namespace TuringClothes.Dtos
{
    public class PurchaseInfoDto
    {
        public TemporaryOrder TemporaryOrder { get; set; }
        public decimal TotalPrice { get; set; }

        public string PriceInWei { get; set; }
        public string EthereumPrice { get; set; }
    }
}
