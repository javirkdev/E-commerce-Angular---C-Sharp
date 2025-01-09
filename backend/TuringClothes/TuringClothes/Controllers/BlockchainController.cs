using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using TuringClothes.Dtos;
using TuringClothes.Services.Blockchain;

namespace TuringClothes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private readonly BlockchainService _blockchainService;

        public BlockchainController(BlockchainService blockchainService)
        {
            _blockchainService = blockchainService;
        }

        //[HttpGet]
        //public Task<Erc20ContractDto> GetContractInfoAsync([FromQuery] ContractInfoRequest data)
        //{
        //    return _blockchainService.GetContractInfoAsync(data.NetworkUrl, data.ContractAddress);
        //}


        [HttpGet("GetEthPrice")]
        public Task<PurchaseInfoDto> GetEthPrice(long id)
        {
            return _blockchainService.GetEthereumPrice(id);
        }


        [HttpPost("transaction")]
        public Task<EthereumTransaction> CreateTransaction([FromBody] CreateTransactionRequest data)
        {
            
            return _blockchainService.CreateEthTransaction(data);
        }

        [HttpPost("check")]
        public Task<bool> CheckTransactionAsync([FromBody] CheckTransactionRequest data)
        {
            return _blockchainService.CheckTransactionAsync(data);
        }
    }
}
