using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using ZPD_Lab_1_3.Contracts;
using ZPD_Lab_1_3.Algorithms;

namespace ZPD_Lab_1_3
{
    public  class FeistelCipher
    {
        int _roundCount;
        ISubKeyGenerator _subKeyGenerator;
        ICipherFunction _cipherFunction;


        public FeistelCipher(int roundCount, ISubKeyGenerator subKeyGenerator, ICipherFunction cipherFunction)
        {
            _roundCount = roundCount;
            _subKeyGenerator = subKeyGenerator;
            _cipherFunction = cipherFunction;
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
            StringBuilder stringBuilder = new StringBuilder();

            for (int block64 = 0; block64 < message.Length / 8; block64++)
            {
                (BitArray leftHalf, BitArray rightHalf)
                    = _splitIntoHalves(message.Substring(block64 * 8, block64 * 8 + 8));
                bool[] encodedBits = _encodeBlock(leftHalf, rightHalf, key);
                stringBuilder.Append(_parseBlock(encodedBits));
            }

            return stringBuilder.ToString();
        }

        public string Decode(string message, BitArray key)
        {
            if (message.Length % 8 != 0)
            {
                for (int i = message.Length % 8; i < 8; i++)
                {
                    message = message + " ";
                }
            }
            StringBuilder stringBuilder = new StringBuilder();

            for (int block64 = 0; block64 < message.Length / 8; block64++)
            {
                (BitArray leftHalf, BitArray rightHalf)
                    = _splitIntoHalves(message.Substring(block64 * 8, block64 * 8 + 8));
                bool[] encodedBits = _decodeBlock(leftHalf, rightHalf, key);
                stringBuilder.Append(_parseBlock(encodedBits));
            }

            return stringBuilder.ToString();
        }

        private bool[] _encodeBlock(BitArray leftHalf, BitArray rightHalf, BitArray key)
        {
            for (int round = 0; round < 16;  round++)
            {
                Scrambler scrambler = null;
                BitArray subKey = _subKeyGenerator.GenerateSubkey(key, scrambler, round);
                BitArray functionResult = _cipherFunction.CipherFunction(leftHalf, subKey, scrambler, round);

                rightHalf = rightHalf.Xor(functionResult);

                (leftHalf, rightHalf) = (rightHalf, leftHalf);
            }
            bool[] encodedBits = new bool[64];

            rightHalf.CopyTo(encodedBits, 0);
            leftHalf.CopyTo(encodedBits, 32);

            return encodedBits;
        }


        private bool[] _decodeBlock(BitArray leftHalf, BitArray rightHalf, BitArray key)
        {
            for (int round = 0; round < 16; round++)
            {
                Scrambler scrambler = null;
                BitArray subKey = _subKeyGenerator.GenerateSubkey(key, scrambler, 16 - round);
                BitArray functionResult = _cipherFunction.CipherFunction(leftHalf, subKey, scrambler, round);

                rightHalf = rightHalf.Xor(functionResult);

                (leftHalf, rightHalf) = (rightHalf, leftHalf);
            }
            bool[] encodedBits = new bool[64];

            rightHalf.CopyTo(encodedBits, 0);
            leftHalf.CopyTo(encodedBits, 32);

            return encodedBits;
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

        private string _parseBlock(bool[] encodedBits)
        {
            char[] decodedChars = new char[encodedBits.Length/8];
            for (int i = encodedBits.Length - 1; i >= 0; i -= 8)
            {
                int numericValue = 0;
                for (int j = 0; j < 8; j-- )
                {
                    if (encodedBits[i - j])
                    {
                        numericValue += (int) Math.Pow(2, 7 - (i - j));
                    }
                }
                decodedChars[7 - i / 8] = (char) numericValue;
            }

            return decodedChars.ToString();
        }
    }
}
