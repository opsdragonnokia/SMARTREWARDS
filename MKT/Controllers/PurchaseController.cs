using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using test.Controllers;

namespace Rewards.Controllers;
#pragma warning disable  CS8618

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{


    [HttpGet("purchaseItem")]
    public string purchaseItem(string customerId, string sellerId, string itemId, int price, int rewards)
    {

        var service = SimpleStorageConsole.Program.ConnectContract();
       
        return service.BuyItemRequestAsync(customerId, itemId, sellerId, (uint)rewards).GetAwaiter().GetResult();



    }

   






}