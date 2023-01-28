using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Sol.Contracts.SimpleStorage.ContractDefinition;
using test.Controllers;

namespace Rewards.Controllers;
#pragma warning disable  CS8618

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
   

    [HttpGet("retrieveUserList")]
    public string[] getUsersETH()
    {
        var service = SimpleStorageConsole.Program.ConnectContract();
        var xxx = service.ListCustomersQueryAsync().GetAwaiter().GetResult();


        List<Customer> userList = service.ListCustomersQueryAsync().GetAwaiter().GetResult().ReturnValue1;
        List<string> response = new List<string>();
        foreach (Customer u in userList)
        {
            response.Add(u.Name);
        }
        response.Sort();
        return response.ToArray();
    }

    [HttpGet("retrieveUserTXN")]
    public List<Transaction> retrieveUserTransactions(string userId)
    {

        var service = SimpleStorageConsole.Program.ConnectContract();
        var cc = service.ListTransactionsQueryAsync().GetAwaiter().GetResult().ReturnValue1;
        List<Transaction> result = new List<Transaction>();
        foreach (var res in cc)
        {
            if (string.Compare(res.UserName, userId, true) == 0)
            {
                result.Add(res);
            }
        }

        return result;
       
    }
  

    [HttpGet("retrieveUserAccount")]
    public List<CustomerPoints> retrieveUserAccount(string inputUserID)
    {
        var service = SimpleStorageConsole.Program.ConnectContract();
        var cc = service.ListCustomerPointsQueryAsync().GetAwaiter().GetResult().ReturnValue1;
        List<CustomerPoints> result = new List<CustomerPoints>();
        foreach (var res in cc)
        {
            if (string.Compare(res.UserName, inputUserID, true) == 0)
            {
                result.Add(res);
            }
        }

        return result;
    }
    
   

   
   



}



