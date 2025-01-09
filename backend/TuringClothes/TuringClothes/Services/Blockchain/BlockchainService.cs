using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Stripe;
using Stripe.Checkout;
using System.Numerics;
using TuringClothes.Database;
using TuringClothes.Dtos;
using TuringClothes.Repository;

namespace TuringClothes.Services.Blockchain
{
    public class BlockchainService
    {
        private readonly TemporaryOrderRepository _temporaryOrderRepository;
        private readonly OrderRepository _orderRepository;
        private readonly UserRepository _userRepository;

        public BlockchainService(TemporaryOrderRepository temporaryOrderRepository, OrderRepository orderRepository, UserRepository userRepository)
        {
            _temporaryOrderRepository = temporaryOrderRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task<Erc20ContractDto> GetContractInfoAsync(string nodeUrl, string contractAddress)
        {
            Web3 web3 = new Web3(nodeUrl);
            Contract contract = web3.Eth.GetContract(ERC20ABI, contractAddress);

            string name = await contract.GetFunction("name").CallAsync<string>();
            string symbol = await contract.GetFunction("symbol").CallAsync<string>();
            int decimals = await contract.GetFunction("decimals").CallAsync<int>();
            BigInteger totalSupply = await contract.GetFunction("totalSupply").CallAsync<BigInteger>();

            return new Erc20ContractDto
            {
                Name = name,
                Symbol = symbol,
                Decimals = decimals,
                TotalSupply = totalSupply.ToString()
            };
        }
        public async Task<PurchaseInfoDto> GetEthereumPrice(long id)
        {
            var order = await _temporaryOrderRepository.GetTemporaryOrder(id);
            decimal price = 0;

            foreach (var item in order.Details)
            {
                price += item.Product.Price * item.Amount;
            }
            price = price / 100;
            CoinGeckoApi coinGeckoApi = new CoinGeckoApi();
            decimal ethEurPrice = await coinGeckoApi.GetEthereumPriceAsync();
            order.EthereumPrice = (double)ethEurPrice;
            await _temporaryOrderRepository.UpdateAsync(order);
            return new PurchaseInfoDto
            {
                TemporaryOrder = order,
                TotalPrice = price,
                EthereumPrice = ethEurPrice.ToString(),
                PriceInWei = (price / ethEurPrice).ToString()
            };

        }

        public async Task<EthereumTransaction> CreateEthTransaction(CreateTransactionRequest data)
        {
            var temporaryOrder = await _temporaryOrderRepository.GetTemporaryOrder(data.TemporaryOrderId);
            EthereumService ethereumService = new EthereumService(_temporaryOrderRepository);

            BigInteger etherValue = ethereumService.ToWei((double)((temporaryOrder.TotalPriceEur / 100) / temporaryOrder.EthereumPrice));

            HexBigInteger gas = ethereumService.GetGas();
            HexBigInteger gasPrice = await ethereumService.GetGasPriceAsync();
            temporaryOrder.HexEthereumPrice = new HexBigInteger(etherValue).HexValue;
            await _temporaryOrderRepository.UpdateAsync(temporaryOrder);
            return new EthereumTransaction
            {
                Value = new HexBigInteger(etherValue).HexValue,
                Gas = gas.HexValue,
                GasPrice = gasPrice.HexValue,
            };
        }

        public async Task<bool> CheckTransactionAsync(CheckTransactionRequest data)
        {
            TemporaryOrder temporaryOrder = await _temporaryOrderRepository.GetTemporaryOrder(data.TemporaryOrderId);
            temporaryOrder.Wallet = data.Wallet;
            await _temporaryOrderRepository.UpdateAsync(temporaryOrder);
            User user = await _userRepository.GetUserById(temporaryOrder.UserId);
            EthereumService ethereumService = new EthereumService(_temporaryOrderRepository);
            bool isTransactionValid = await ethereumService.CheckTransactionAsync(data.Hash, data.TemporaryOrderId);

            if (isTransactionValid)
            {
                var newOrder = await _orderRepository.CreateOrder(data.TemporaryOrderId, data.PaymentMethod, "", temporaryOrder.TotalPriceEur, user.Email);
                Console.WriteLine("Transacción válida");
                return true;
            }
            return false;
        }


        // Definición del ABI de ERC-20
        private static readonly string ERC20ABI = """
    [
        {
            'constant': true,
            'inputs': [],
            'name': 'name',
            'outputs': [
                {
                    'name': '',
                    'type': 'string'
                }
            ],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        },
        {
            'constant': true,
            'inputs': [],
            'name': 'symbol',
            'outputs': [
                {
                    'name': '',
                    'type': 'string'
                }
            ],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        },
        {
            'constant': true,
            'inputs': [],
            'name': 'decimals',
            'outputs': [
                {
                    'name': '',
                    'type': 'uint8'
                }
            ],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        },
        {
            'constant': true,
            'inputs': [],
            'name': 'totalSupply',
            'outputs': [
                {
                    'name': '',
                    'type': 'uint256'
                }
            ],
            'payable': false,
            'stateMutability': 'view',
            'type': 'function'
        }
    ]
    """;
    }
}
