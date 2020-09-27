using System;
using System.Collections;
using System.Text;

namespace ZPD_Lab_1_3.Contracts
{
    public interface ICipherFunction
    {
        public BitArray CipherFunction(BitArray leftHalf, BitArray subKey, Scrambler scrambler, int round);
    }
}
