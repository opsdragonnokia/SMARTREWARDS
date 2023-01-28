using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Sol.Contracts.SimpleStorage.ContractDefinition
{
    public partial class Customer : CustomerBase { }

    public class CustomerBase 
    {
        [Parameter("string", "Name", 1)]
        public virtual string Name { get; set; }
    }
}
