using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZPD_Lab_1_3.Algorithms;
using System.Collections;

namespace ZPD_Lab_1_3.Tests
{
    public class FeistelCipherEncodeTest
    {
        [Theory]
        [InlineData(
            "On stree",
            new bool[] { false, false, false, true, true, false, true, false, true, false, true, false, false, false, false, false, true, false, true, false, true, true, true, true, true, true, false, false, false, false, true, true })]
        [InlineData(
            "Nurse Cafe      ",
            new bool[] { false, false, false, true, true, false, true, false, true, false, true, false, false, false, true, false, true, false, true, false, true, true, true, true, true, true, false, false, false, false, true, true })]
        public void Encode_AlgorithmsAMEssageKey_ReturnsMessageEncodedForKey(string message, bool[] key)
        {
            FeistelCipher cipher = new FeistelCipher(16, new SubKeyGeneratorA(), new CipherFunctionA());
            char[] messageToEncode = message.ToCharArray();

            List<char> encodedMessage =  cipher.Encode(messageToEncode, new BitArray(key)).ToList();

            List<char> decodedMessage = cipher.Decode(encodedMessage.ToArray(), new BitArray(key)).ToList();

            Assert.Equal(messageToEncode, decodedMessage.ToArray());
        }


    }
}
