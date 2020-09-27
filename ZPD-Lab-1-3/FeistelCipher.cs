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

        public IEnumerable<char> Encode(char[] messageToEncode, BitArray key)
        {

            List<char> message = new List<char>(messageToEncode);
            if (messageToEncode.Length % 8 != 0)
            {
                for (int i = messageToEncode.Length % 8; i < 8; i++)
                {
                    message.Add(' ');
                }
            }
            List<char> encodedMessage = new List<char>() ;

            for (int block64 = 0; block64 < message.Count; block64 += 8)
            {
                (BitArray leftHalf, BitArray rightHalf)
                    = _splitIntoHalves(SubArray(message.ToArray(), block64, 8));
                bool[] encodedBits = _encodeBlock(leftHalf, rightHalf, key);
                encodedMessage.AddRange(_parseBlock(encodedBits));
            }

            return encodedMessage;
        }

        public IEnumerable<char> Decode(char[] messageToDecode, BitArray key)
        {

            List<char> message = new List<char>(messageToDecode);

            List<char> decodedMessage = new List<char>();

            for (int block64 = 0; block64 < message.Count; block64 += 8)
            {
                (BitArray leftHalf, BitArray rightHalf)
                    = _splitIntoHalves(SubArray(message.ToArray(), block64, 8));
                bool[] encodedBits = _decodeBlock(leftHalf, rightHalf, key);
                decodedMessage.AddRange(_parseBlock(encodedBits));
            }

            return decodedMessage;
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
                BitArray subKey = _subKeyGenerator.GenerateSubkey(key, scrambler, 15 - round);
                BitArray functionResult = _cipherFunction.CipherFunction(leftHalf, subKey, scrambler, round);

                rightHalf = rightHalf.Xor(functionResult);

                (leftHalf, rightHalf) = (rightHalf, leftHalf);
            }
            bool[] encodedBits = new bool[64];

            rightHalf.CopyTo(encodedBits, 0);
            leftHalf.CopyTo(encodedBits, 32);

            return encodedBits;
        }

        private (BitArray, BitArray) _splitIntoHalves(char[] charBlock)
        {
            char[] leftHalfChar = SubArray(charBlock, 0, charBlock.Length / 2);
            char[] rightHalfChar = SubArray(charBlock, charBlock.Length / 2, charBlock.Length / 2);


            BitArray leftHalf = new BitArray
            (
                leftHalfChar.Reverse() //Reverse the order under the assumption that the start of the bitArray is little endian
                    .Select(c => (byte)c).ToArray() //Convert to byte (i.e. 8-bit int) to write hte 8-bit binary values
            );

            BitArray rightHalf = new BitArray
            (
                rightHalfChar.Reverse() 
                    .Select(c => (byte)c).ToArray()
            );

            return (leftHalf, rightHalf);
        }

        private char[] _parseBlock(bool[] encodedBits)
        {
            char[] decodedChars = new char[encodedBits.Length/8];
            for (int i = encodedBits.Length - 1; i >= 0; i -= 8)
            {
                int numericValue = 0;
                for (int j = 0; j < 8; j++ )
                {
                    if (encodedBits[i - j])
                    {
                        numericValue += (int) Math.Pow(2, 7 - j);
                    }
                }
                decodedChars[7 - i / 8] = (char) numericValue;
            }

            return decodedChars;
        }

        private char[] SubArray(char[] data, int index, int length)
        {
            char[] result = new char[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}
