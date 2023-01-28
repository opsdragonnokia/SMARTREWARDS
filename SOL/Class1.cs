// using EthereumSmartContracts.Contracts.SimpleStorage;
// using EthereumSmartContracts.Contracts.SimpleStorage.ContractDefinition;
using Nethereum.Signer;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Sol.Contracts.SimpleStorage;
using Sol.Contracts.SimpleStorage.ContractDefinition;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleStorageConsole
{
    public class Program
    {

        

        public static SimpleStorageService ConnectContract()
        { 
            return getService();
        }


       

       


       
        
        public static string getActiveConfig()
        {
            string scontent = "";
            if (File.Exists(CFGFile))
            {
                scontent = File.ReadAllText(CFGFile);
                return scontent;
            }
            return null;

        }
        public static Config getDeployment()
        {
            string scontent = "";
            if (File.Exists(CFGFile))
            {
                scontent = File.ReadAllText(CFGFile);
                Config o = (Config )JsonSerializer.Deserialize(scontent, typeof(Config));

                return o;
            }
            return null;

        }
        static SimpleStorageService getService()
        {
            Config c = getDeployment();
            if (c == null) throw new Exception("Unable to get configuration");
            var account = new Account(c.accountKey, c.chainId);
            var web3 = new Web3(account, c.ethURL);
            return  new SimpleStorageService(web3, c.contract);
        }
        
        static string CFGFile = "pc.json";

      

        public class Config
        {
            public string contract {get; set;}  
            [System.Text.Json.Serialization.JsonPropertyName("key")]
            public string accountKey {get; set;}    
            public long chainId {get; set;}
            public string ethURL {get; set;}
        }

       
    }
}