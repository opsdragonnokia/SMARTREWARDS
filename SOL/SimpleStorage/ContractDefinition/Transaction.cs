using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Sol.Contracts.SimpleStorage.ContractDefinition
{
    public partial class Transaction : TransactionBase { }

    public class TransactionBase 
    {
        [Parameter("string", "UserName", 1)]
        public virtual string UserName { get; set; }
        [Parameter("string", "SellerName", 2)]
        public virtual string SellerName { get; set; }
        [Parameter("string", "ItemID", 3)]
        public virtual string ItemID { get; set; }
        [Parameter("uint32", "Points", 4)]
        public virtual uint Points { get; set; }
    }
}
