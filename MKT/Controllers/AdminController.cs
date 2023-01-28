using Microsoft.AspNetCore.Mvc;
using Nethereum.Web3.Accounts;

using test.Controllers;

namespace Rewards.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet("addCustomer")]
    public string AddCustomer(string customerId)
    {
        var service = SimpleStorageConsole.Program.ConnectContract();
        return service.AddCustomerRequestAsync(customerId).GetAwaiter().GetResult();
        
    }
    [HttpGet("addSeller")]
    public string AddSeller(string sellerId, uint points)
    {
        var service = SimpleStorageConsole.Program.ConnectContract();
        return service.AddSellerRequestAsync(sellerId, points).GetAwaiter().GetResult();
        
    }

    


}



