using Microsoft.AspNetCore.Mvc;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Sol.Contracts.SimpleStorage;
using Sol.Contracts.SimpleStorage.ContractDefinition;
using test.Controllers;
using System.IO;

namespace Rewards.Controllers;


[ApiExplorerSettings(IgnoreApi = true)]
[ApiController]
[Route("[controller]")]
public class DeployController : ControllerBase
{
    /// <summary>
    /// This deploys the solidity contract
    /// </summary>
    /// <param name="accountKey"></param>
    /// <param name="chainId"></param>
    /// <param name="ethUrl"></param>
    /// <returns></returns>
    [HttpGet("DeploySolContract")]
    ///

    public string DeployContract(string accountKey = "06a879209f4da09c8550a1aca977121833eee667ab27de58df9a10cda5a9bceb", long chainId = 3, string ethUrl = "https://ropsten.infura.io/v3/eb137044620044bda9c61bde107f1ff1")
    {
        Deploy(accountKey, chainId, ethUrl).GetAwaiter().GetResult();
        return "Hello";

    }

    [HttpGet("Deploy")]

    public static async Task Deploy(string accountKey= "06a879209f4da09c8550a1aca977121833eee667ab27de58df9a10cda5a9bceb", long chainId=3, string ethUrl= "https://ropsten.infura.io/v3/eb137044620044bda9c61bde107f1ff1")
    {
        /*
         string accountKey = "06a879209f4da09c8550a1aca977121833eee667ab27de58df9a10cda5a9bceb";
        int chainId = 3;
        string ethURL = "https://ropsten.infura.io/v3/eb137044620044bda9c61bde107f1ff1";
         
         
         */
        string ackKey = accountKey;
        try
        {
            string CFGFile = "pc.json";
            var account = new Account(ackKey, chainId);

            //Now let's create an instance of Web3 using our account pointing to our nethereum testchain 
            var web3 = new Web3(account, ethUrl);

            SimpleStorageService service;

            Console.WriteLine("Deploying...");
            var deployment = new SimpleStorageDeployment();
            var receipt = await SimpleStorageService.DeployContractAndWaitForReceiptAsync(web3, deployment);
            service = new SimpleStorageService(web3, receipt.ContractAddress);
            Console.WriteLine($"Contract Deployment Tx Status: {receipt.Status.Value}");
            Console.WriteLine($"Contract Address: {service.ContractHandler.ContractAddress}");

            System.IO.File.WriteAllText(CFGFile, @$"{{ ""contract"":""{service.ContractHandler.ContractAddress}"", ""ethURL"":""{ethUrl}"", ""key"":""{accountKey}"", ""chainId"":{chainId} }} ");

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }

}
