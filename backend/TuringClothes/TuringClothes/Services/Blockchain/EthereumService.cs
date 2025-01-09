using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using System.Numerics;
using TuringClothes.Repository;

namespace TuringClothes.Services.Blockchain
{
    public class EthereumService
    {
        private const string ADDRESS_TO_SEND = "0x5FEB7276Af5FA6b8acA27B03441D9Fe2C5FA6426";
        private const int POLLY_INTERVAL_MS = 1000;
        private const int GAS = 30_000;
        private const int TRANSACTION_SUCCESS_STATUS = 1;
        private const string NETWORK_URL = "https://otter.bordel.wtf/erigon";
        private readonly Web3 _web3;
        private readonly TemporaryOrderRepository _temporaryOrderRepository;

        public EthereumService(TemporaryOrderRepository temporaryOrderRepository)
        {
            _web3 = new Web3(NETWORK_URL);
            _web3.TransactionReceiptPolling.SetPollingRetryIntervalInMilliseconds(POLLY_INTERVAL_MS);
            _temporaryOrderRepository = temporaryOrderRepository;
        }

        public BigInteger ToWei(double amount)
        {
            return Web3.Convert.ToWei(amount);
        }

        public BigInteger ToWei(decimal amount)
        {
            return Web3.Convert.ToWei(amount);
        }

        public HexBigInteger GetGas()
        {
            return new HexBigInteger(GAS);
        }

        public async Task<HexBigInteger> GetGasPriceAsync()
        {
            return await _web3.Eth.GasPrice.SendRequestAsync();
        }

        public async Task<bool> CheckTransactionAsync(string txHash, long temporaryOrderId)
        {
            bool result;
            var temporaryOrder = await _temporaryOrderRepository.GetTemporaryOrder(temporaryOrderId);

            try
            {
                Transaction transaction = await _web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(txHash);
                TransactionReceipt txReceipt = await _web3.TransactionReceiptPolling.PollForReceiptAsync(txHash);
                Console.WriteLine($"Transaction From: {transaction.From}");
                Console.WriteLine($"Transaction To: {transaction.To}");
                Console.WriteLine($"Transaction Value: {transaction.Value.HexValue}");
                Console.WriteLine($"Expected Wallet: {temporaryOrder.Wallet}");
                Console.WriteLine($"Expected Address To Send: {ADDRESS_TO_SEND}");
                Console.WriteLine($"Expected Ethereum Price: {temporaryOrder.HexEthereumPrice}");

                result = txReceipt.Status.Value == TRANSACTION_SUCCESS_STATUS
                    && Equals(transaction.From, temporaryOrder.Wallet)
                    && Equals(transaction.To, ADDRESS_TO_SEND)
                    && Equals(transaction.Value.HexValue, temporaryOrder.HexEthereumPrice);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        private bool Equals(string hex1, string hex2)
        {
            return hex1.Equals(hex2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
