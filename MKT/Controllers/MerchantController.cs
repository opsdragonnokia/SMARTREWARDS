using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Sol.Contracts.SimpleStorage.ContractDefinition;
using test.Controllers;

namespace Rewards.Controllers;

[ApiController]
[Route("[controller]")]
public class MerchantController : ControllerBase
{

    [HttpGet("retrieveMerchantList")]
    public string[] retrieveMerchantList()
    {
        var service = SimpleStorageConsole.Program.ConnectContract();
        List<Seller> userList = service.ListSellersQueryAsync().GetAwaiter().GetResult().ReturnValue1;
        List<string> response = new List<string>();
        foreach (Seller u in userList)
        {
            response.Add(u.Name);
        }
        response.Sort();
        return response.ToArray();
    
        
    }
    [HttpGet("retrieveMerchantPoints")]
    public List<Seller>  retrieveMerchantPoints()
    {
        var service = SimpleStorageConsole.Program.ConnectContract();
        return service.ListSellersQueryAsync().GetAwaiter().GetResult().ReturnValue1;

        
    }

    [HttpGet("fullInventory")]
    public Inventory[] fullInventory()
    {
        List<Inventory> list = new List<Inventory>();

        using (var command = Utility.CreateDBConnection())
        {
            command.CommandText = $"select merchantID, itemID, itemName, stock, price, rewards from Inventory order by merchantID, itemID";
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read()) //Read one row at a time from DB
                {
                    Inventory item = new Inventory();
                    item.MerchantId = reader.GetString(0);
                    item.ItemId = reader.GetString(1);
                    item.ItemName = reader.GetString(2);
                    //item.Quantity = reader.GetInt16(3);
                    item.Price = reader.GetFloat(4);
                    item.PointUnitValue = reader.GetInt16(5);
                    list.Add(item);

                }

            }

        }
        return list.ToArray();


    }

    [HttpGet("retrieveInventory")]
    public Inventory[] retrieveInventory(string merchantId)
    {
        List<Inventory> list = new List<Inventory>();

        using (var command = Utility.CreateDBConnection())
        {
            command.CommandText = $"select merchantID, itemID, itemName, stock, price, rewards from Inventory where merchantID='{merchantId}'";
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read()) //Read one row at a time from DB
                {
                    Inventory item = new Inventory();
                    item.MerchantId = reader.GetString(0);
                    item.ItemId = reader.GetString(1);
                    item.ItemName = reader.GetString(2);
                    //item.Quantity = reader.GetInt16(3);
                    item.Price = reader.GetFloat(4);
                    item.PointUnitValue = reader.GetInt16(5);
                    list.Add(item);
                    
                }

            }

        }
        return list.ToArray();


    }

}

#pragma warning disable  CS8618
public class Inventory
{
    public string MerchantId { get; set; }
    public string ItemId
    {
        get;
        set;
    }
    public string ItemName { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public int PointUnitValue { get; set; }
    public int PointBalance
    {
        get
        {
            return Quantity * PointUnitValue;
        }
    }
}

public class MerchantAccount
{
    public string MerchantID { get; set; }
   
    public int Points { get; set; }
}

