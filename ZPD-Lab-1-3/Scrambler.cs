using System;
using System.Collections;
using System.Text;

namespace ZPD_Lab_1_3
{
    public class Scrambler
    {
        BitArray _sequence;
        BitArray _key;

        public Scrambler(bool[] sequence, bool[] key)
        {
            if (sequence.Length != key.Length)
                throw new ArgumentException("Sequence and key lengths should be the same.");

            _sequence = new BitArray(sequence);
            _key = new BitArray(key);
        }

        public BitArray Scramble(int roundCount)
        {
            BitArray sequenceCopy = (BitArray) _sequence.Clone();

            for (int round = 0; round < roundCount; round++)
            {
                bool lastElement = false;
                for (int i = 0; i < sequenceCopy.Length - 1; i++)
                {
                    lastElement = lastElement ^ (sequenceCopy[i] & _key[i]);
                    sequenceCopy[i] = sequenceCopy[i + 1];
                }
                lastElement = lastElement ^ 
                    (sequenceCopy[sequenceCopy.Length - 1] & _key[sequenceCopy.Length - 1]);

                sequenceCopy[sequenceCopy.Length - 1] = lastElement;
            }
            return sequenceCopy;
        }

        public bool[] Sequence 
        {
            get 
            {
                bool[] array = new bool[_sequence.Length];
                _sequence.CopyTo(array, 0);
                return array;
            }
        }


    }
}
