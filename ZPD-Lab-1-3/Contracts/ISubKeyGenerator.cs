using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace ZPD_Lab_1_3.Contracts
{
    public interface ISubKeyGenerator
    {
        public BitArray GenerateSubkey(BitArray key, Scrambler scrambler);
    }
}
