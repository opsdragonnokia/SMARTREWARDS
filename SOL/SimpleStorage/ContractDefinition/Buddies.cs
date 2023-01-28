using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Sol.Contracts.SimpleStorage.ContractDefinition
{
    public partial class Buddies : BuddiesBase { }

    public class BuddiesBase 
    {
        [Parameter("string", "Seller1", 1)]
        public virtual string Seller1 { get; set; }
        [Parameter("string", "Seller2", 2)]
        public virtual string Seller2 { get; set; }
    }
}
