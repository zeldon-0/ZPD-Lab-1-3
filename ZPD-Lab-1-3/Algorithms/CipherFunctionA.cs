using System;
using System.Collections;
using System.Text;
using ZPD_Lab_1_3.Contracts;

namespace ZPD_Lab_1_3.Algorithms
{
    public class CipherFunctionA : ICipherFunction
    {
        public BitArray CipherFunction(BitArray leftHalf, BitArray subKey, Scrambler scrambler, int round)
        {
            return subKey;
        }
    }
}
