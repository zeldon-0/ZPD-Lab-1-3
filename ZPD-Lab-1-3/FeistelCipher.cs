using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace ZPD_Lab_1_3
{
    public  class FeistelCipher
    {
        int _roundCount;
        public FeistelCipher()
        {
            _roundCount = 16;
        }

        public FeistelCipher(int roundCount)
        {
            _roundCount = roundCount;
        }

        public string Encode(string message, BitArray key)
        {
            if (message.Length % 8 != 0)
            {
                for (int i = message.Length % 8; i < 8; i++)
                {
                    message = message + " ";
                }
            }
            
            for (int block64 = 0; block64 < message.Length / 8; block64++)
            {

            }


            for (int round = 0; round < _roundCount; round++)
            {

            }


            return message;
        }
        private (BitArray, BitArray) _splitIntoHalves(string charBlock)
        {
            string leftHalfChar = charBlock.Substring(0, charBlock.Length / 2);
            string rightHalfChar = charBlock.Substring(charBlock.Length / 2);


            BitArray leftHalf = new BitArray
            (
                leftHalfChar.ToCharArray().Reverse() //Reverse the order under the assumption that the start of the bitArray is little endian
                    .Select(c => (byte)c).ToArray() //Convert to byte (i.e. 8-bit int) to write hte 8-bit binary values
            );

            BitArray rightHalf = new BitArray
            (
                rightHalfChar.ToCharArray().Reverse() 
                    .Select(c => (byte)c).ToArray()
            );

            return (leftHalf, rightHalf);
        }
    }
}
