using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ZPD_Lab_1_3.Contracts;

namespace ZPD_Lab_1_3.Algorithms
{
    public class SubKeyGeneratorA : ISubKeyGenerator
    {
        public BitArray GenerateSubkey(BitArray key, int round)
        {
            BitArray subKey = new BitArray(key);
            for (int i = 0; i < 32; i++)
            {
                if (i + round < key.Length)
                {
                    subKey[i] = key[round + i];
                }
                else
                {
                    subKey[i] = subKey[(round + i) % key.Length];
                }
            }
            return subKey;
        }
    }
}
