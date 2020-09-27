using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ZPD_Lab_1_3.Contracts;

namespace ZPD_Lab_1_3.Algorithms
{
    public class SubKeyGeneratorB : ISubKeyGenerator
    {
        private bool[] sequence = new bool[8] { true, true, false, false, false, false, false, false };
        public BitArray GenerateSubkey(BitArray key, int round)
        {
            List<bool> subkey = new List<bool>();

            bool[] keySequence = new bool[8];

            for (int i = 0; i < 8; i++)
            {
                keySequence[i] = key[round + i];
            }

            Scrambler scrambler = new Scrambler(sequence, keySequence);

            for(int i = 0; i < 32; i++)
            {
                bool firstElement =  scrambler.Scramble(i)[0];
                subkey.Add(firstElement);
            }
            subkey.Reverse();

            return new BitArray(subkey.ToArray());
        }
    }
}
