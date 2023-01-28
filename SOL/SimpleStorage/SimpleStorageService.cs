using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Sol.Contracts.SimpleStorage.ContractDefinition;

namespace Sol.Contracts.SimpleStorage
{
    public partial class SimpleStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SimpleStorageDeployment>().SendRequestAndWaitForReceiptAsync(simpleStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SimpleStorageDeployment>().SendRequestAsync(simpleStorageDeployment);
        }

        public static async Task<SimpleStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, SimpleStorageDeployment simpleStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, simpleStorageDeployment, cancellationTokenSource);
            return new SimpleStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public SimpleStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<PointsTrackerOutputDTO> PointsTrackerQueryAsync(PointsTrackerFunction pointsTrackerFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<PointsTrackerFunction, PointsTrackerOutputDTO>(pointsTrackerFunction, blockParameter);
        }

        public Task<PointsTrackerOutputDTO> PointsTrackerQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var pointsTrackerFunction = new PointsTrackerFunction();
                pointsTrackerFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<PointsTrackerFunction, PointsTrackerOutputDTO>(pointsTrackerFunction, blockParameter);
        }

        public Task<TransactionsOutputDTO> TransactionsQueryAsync(TransactionsFunction transactionsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<TransactionsFunction, TransactionsOutputDTO>(transactionsFunction, blockParameter);
        }

        public Task<TransactionsOutputDTO> TransactionsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var transactionsFunction = new TransactionsFunction();
                transactionsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<TransactionsFunction, TransactionsOutputDTO>(transactionsFunction, blockParameter);
        }

        public Task<string> AddBuddyRequestAsync(AddBuddyFunction addBuddyFunction)
        {
             return ContractHandler.SendRequestAsync(addBuddyFunction);
        }

        public Task<TransactionReceipt> AddBuddyRequestAndWaitForReceiptAsync(AddBuddyFunction addBuddyFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addBuddyFunction, cancellationToken);
        }

        public Task<string> AddBuddyRequestAsync(string seller1, string seller2)
        {
            var addBuddyFunction = new AddBuddyFunction();
                addBuddyFunction.Seller1 = seller1;
                addBuddyFunction.Seller2 = seller2;
            
             return ContractHandler.SendRequestAsync(addBuddyFunction);
        }

        public Task<TransactionReceipt> AddBuddyRequestAndWaitForReceiptAsync(string seller1, string seller2, CancellationTokenSource cancellationToken = null)
        {
            var addBuddyFunction = new AddBuddyFunction();
                addBuddyFunction.Seller1 = seller1;
                addBuddyFunction.Seller2 = seller2;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addBuddyFunction, cancellationToken);
        }

        public Task<string> AddCustomerRequestAsync(AddCustomerFunction addCustomerFunction)
        {
             return ContractHandler.SendRequestAsync(addCustomerFunction);
        }

        public Task<TransactionReceipt> AddCustomerRequestAndWaitForReceiptAsync(AddCustomerFunction addCustomerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addCustomerFunction, cancellationToken);
        }

        public Task<string> AddCustomerRequestAsync(string name)
        {
            var addCustomerFunction = new AddCustomerFunction();
                addCustomerFunction.Name = name;
            
             return ContractHandler.SendRequestAsync(addCustomerFunction);
        }

        public Task<TransactionReceipt> AddCustomerRequestAndWaitForReceiptAsync(string name, CancellationTokenSource cancellationToken = null)
        {
            var addCustomerFunction = new AddCustomerFunction();
                addCustomerFunction.Name = name;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addCustomerFunction, cancellationToken);
        }

        public Task<string> AddSellerRequestAsync(AddSellerFunction addSellerFunction)
        {
             return ContractHandler.SendRequestAsync(addSellerFunction);
        }

        public Task<TransactionReceipt> AddSellerRequestAndWaitForReceiptAsync(AddSellerFunction addSellerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addSellerFunction, cancellationToken);
        }

        public Task<string> AddSellerRequestAsync(string name, uint points)
        {
            var addSellerFunction = new AddSellerFunction();
                addSellerFunction.Name = name;
                addSellerFunction.Points = points;
            
             return ContractHandler.SendRequestAsync(addSellerFunction);
        }

        public Task<TransactionReceipt> AddSellerRequestAndWaitForReceiptAsync(string name, uint points, CancellationTokenSource cancellationToken = null)
        {
            var addSellerFunction = new AddSellerFunction();
                addSellerFunction.Name = name;
                addSellerFunction.Points = points;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(addSellerFunction, cancellationToken);
        }

        public Task<string> BearTroubleQueryAsync(BearTroubleFunction bearTroubleFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BearTroubleFunction, string>(bearTroubleFunction, blockParameter);
        }

        
        public Task<string> BearTroubleQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<BearTroubleFunction, string>(null, blockParameter);
        }

        public Task<BuddiesOutputDTO> BuddiesQueryAsync(BuddiesFunction buddiesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<BuddiesFunction, BuddiesOutputDTO>(buddiesFunction, blockParameter);
        }

        public Task<BuddiesOutputDTO> BuddiesQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var buddiesFunction = new BuddiesFunction();
                buddiesFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<BuddiesFunction, BuddiesOutputDTO>(buddiesFunction, blockParameter);
        }

        public Task<string> BuyItemRequestAsync(BuyItemFunction buyItemFunction)
        {
             return ContractHandler.SendRequestAsync(buyItemFunction);
        }

        public Task<TransactionReceipt> BuyItemRequestAndWaitForReceiptAsync(BuyItemFunction buyItemFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyItemFunction, cancellationToken);
        }

        public Task<string> BuyItemRequestAsync(string customerName, string itemId, string sellerName, uint points)
        {
            var buyItemFunction = new BuyItemFunction();
                buyItemFunction.CustomerName = customerName;
                buyItemFunction.ItemId = itemId;
                buyItemFunction.SellerName = sellerName;
                buyItemFunction.Points = points;
            
             return ContractHandler.SendRequestAsync(buyItemFunction);
        }

        public Task<TransactionReceipt> BuyItemRequestAndWaitForReceiptAsync(string customerName, string itemId, string sellerName, uint points, CancellationTokenSource cancellationToken = null)
        {
            var buyItemFunction = new BuyItemFunction();
                buyItemFunction.CustomerName = customerName;
                buyItemFunction.ItemId = itemId;
                buyItemFunction.SellerName = sellerName;
                buyItemFunction.Points = points;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(buyItemFunction, cancellationToken);
        }

        public Task<string> ClearEverythingRequestAsync(ClearEverythingFunction clearEverythingFunction)
        {
             return ContractHandler.SendRequestAsync(clearEverythingFunction);
        }

        public Task<string> ClearEverythingRequestAsync()
        {
             return ContractHandler.SendRequestAsync<ClearEverythingFunction>();
        }

        public Task<TransactionReceipt> ClearEverythingRequestAndWaitForReceiptAsync(ClearEverythingFunction clearEverythingFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(clearEverythingFunction, cancellationToken);
        }

        public Task<TransactionReceipt> ClearEverythingRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<ClearEverythingFunction>(null, cancellationToken);
        }

        public Task<string> CustomersQueryAsync(CustomersFunction customersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CustomersFunction, string>(customersFunction, blockParameter);
        }

        
        public Task<string> CustomersQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var customersFunction = new CustomersFunction();
                customersFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<CustomersFunction, string>(customersFunction, blockParameter);
        }

        public Task<ListBuddiesOutputDTO> ListBuddiesQueryAsync(ListBuddiesFunction listBuddiesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListBuddiesFunction, ListBuddiesOutputDTO>(listBuddiesFunction, blockParameter);
        }

        public Task<ListBuddiesOutputDTO> ListBuddiesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListBuddiesFunction, ListBuddiesOutputDTO>(null, blockParameter);
        }

        public Task<ListCustomerPointsOutputDTO> ListCustomerPointsQueryAsync(ListCustomerPointsFunction listCustomerPointsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListCustomerPointsFunction, ListCustomerPointsOutputDTO>(listCustomerPointsFunction, blockParameter);
        }

        public Task<ListCustomerPointsOutputDTO> ListCustomerPointsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListCustomerPointsFunction, ListCustomerPointsOutputDTO>(null, blockParameter);
        }

        public Task<ListCustomersOutputDTO> ListCustomersQueryAsync(ListCustomersFunction listCustomersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListCustomersFunction, ListCustomersOutputDTO>(listCustomersFunction, blockParameter);
        }

        public Task<ListCustomersOutputDTO> ListCustomersQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListCustomersFunction, ListCustomersOutputDTO>(null, blockParameter);
        }

        public Task<ListSellersOutputDTO> ListSellersQueryAsync(ListSellersFunction listSellersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListSellersFunction, ListSellersOutputDTO>(listSellersFunction, blockParameter);
        }

        public Task<ListSellersOutputDTO> ListSellersQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListSellersFunction, ListSellersOutputDTO>(null, blockParameter);
        }

        public Task<ListTransactionsOutputDTO> ListTransactionsQueryAsync(ListTransactionsFunction listTransactionsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListTransactionsFunction, ListTransactionsOutputDTO>(listTransactionsFunction, blockParameter);
        }

        public Task<ListTransactionsOutputDTO> ListTransactionsQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ListTransactionsFunction, ListTransactionsOutputDTO>(null, blockParameter);
        }

        public Task<string> MainDeveloperQueryAsync(MainDeveloperFunction mainDeveloperFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MainDeveloperFunction, string>(mainDeveloperFunction, blockParameter);
        }

        
        public Task<string> MainDeveloperQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MainDeveloperFunction, string>(null, blockParameter);
        }

        public Task<string> RetributionQueryAsync(RetributionFunction retributionFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RetributionFunction, string>(retributionFunction, blockParameter);
        }

        
        public Task<string> RetributionQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<RetributionFunction, string>(null, blockParameter);
        }

        public Task<SellersOutputDTO> SellersQueryAsync(SellersFunction sellersFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<SellersFunction, SellersOutputDTO>(sellersFunction, blockParameter);
        }

        public Task<SellersOutputDTO> SellersQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var sellersFunction = new SellersFunction();
                sellersFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<SellersFunction, SellersOutputDTO>(sellersFunction, blockParameter);
        }

        public Task<string> UpdateSellerPointsRequestAsync(UpdateSellerPointsFunction updateSellerPointsFunction)
        {
             return ContractHandler.SendRequestAsync(updateSellerPointsFunction);
        }

        public Task<TransactionReceipt> UpdateSellerPointsRequestAndWaitForReceiptAsync(UpdateSellerPointsFunction updateSellerPointsFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateSellerPointsFunction, cancellationToken);
        }

        public Task<string> UpdateSellerPointsRequestAsync(string sellerName, uint points)
        {
            var updateSellerPointsFunction = new UpdateSellerPointsFunction();
                updateSellerPointsFunction.SellerName = sellerName;
                updateSellerPointsFunction.Points = points;
            
             return ContractHandler.SendRequestAsync(updateSellerPointsFunction);
        }

        public Task<TransactionReceipt> UpdateSellerPointsRequestAndWaitForReceiptAsync(string sellerName, uint points, CancellationTokenSource cancellationToken = null)
        {
            var updateSellerPointsFunction = new UpdateSellerPointsFunction();
                updateSellerPointsFunction.SellerName = sellerName;
                updateSellerPointsFunction.Points = points;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(updateSellerPointsFunction, cancellationToken);
        }
    }
}
