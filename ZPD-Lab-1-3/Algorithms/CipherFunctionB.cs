using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ZPD_Lab_1_3.Contracts;

namespace ZPD_Lab_1_3.Algorithms
{
    public class CipherFunctionB : ICipherFunction
    {
        private bool[] sequence = new bool[16] { true, true, false, false, false, false, false, false, false, false, false, false, false, false, true, false };
        public BitArray CipherFunction(BitArray leftHalf, BitArray subKey, int round)
        {
            List<bool> generatedSequence = new List<bool>();
            Scrambler scrambler = new Scrambler(sequence, (bool[]) sequence.Clone());

            for (int i = 0; i < 32; i++)
            {
                bool firstElement = scrambler.Scramble(i)[0];
                generatedSequence.Add(firstElement);
            }
            generatedSequence.Reverse();

            BitArray functionResult = new BitArray(generatedSequence.ToArray());

            //functionResult = functionResult.Xor(leftHalf);

            return functionResult;
        }
    }
}
