using System.Collections.Generic;
using System.Linq;
using Xunit;
using ZPD_Lab_1_3.Algorithms;
using System.Collections;

namespace ZPD_Lab_1_3.Tests
{
    public class FeistelCipherTest
    {
        [Theory]
        [InlineData(
            "With a spring in my step, I smash the windows of every house I pass by Yeah, yeah, yeah, yeah, yeah  To the Nurse Cafe To imbibe the principles that produce rainbows I open up the sky ",
            new bool[] { false, false, false, true, true, false, true, false, true, false, true, false, false, false, false, false, true, false, true, false, true, true, true, true, true, true, false, false, false, false, true, true })]
        [InlineData(
            "Nurse Cafe      ",
            new bool[] { false, false, false, true, true, false, true, false, true, false, true, false, false, false, true, false, true, false, true, false, true, true, true, true, true, true, false, false, false, false, true, true })]
        public void EncodeAndDecode_AlgorithmsAMessageKey_ReturnsStartingMessage(string message, bool[] key)
        {
            FeistelCipher cipher = new FeistelCipher(16, new SubKeyGeneratorA(), new CipherFunctionA());
            char[] messageToEncode = message.ToCharArray();

            List<char> encodedMessage =  cipher.Encode(messageToEncode, new BitArray(key)).ToList();

            List<char> decodedMessage = cipher.Decode(encodedMessage.ToArray(), new BitArray(key)).ToList();

            Assert.Equal(messageToEncode, decodedMessage.ToArray());
        }

        [Theory]
        [InlineData(
            "With a spring in my step, I smash the windows of every house I pass by Yeah, yeah, yeah, yeah, yeah  To the Nurse Cafe To imbibe the principles that produce rainbows I open up the sky ",
            new bool[] { false, false, false, true, true, false, true, false, true, false, true, false, false, false, false, false, true, false, true, false, true, true, true, true, true, true, false, false, false, false, true, true })]
        [InlineData(
            "Nurse Cafe      ",
            new bool[] { false, false, false, true, true, false, true, false, true, false, true, false, false, false, true, false, true, false, true, false, true, true, true, true, true, true, false, false, false, false, true, true })]
        public void EncodeAndDecode_AlgorithmsBMessageKey_ReturnsStartingMessage(string message, bool[] key)
        {
            FeistelCipher cipher = new FeistelCipher(16, new SubKeyGeneratorB(), new CipherFunctionB());
            char[] messageToEncode = message.ToCharArray();

            List<char> encodedMessage = cipher.Encode(messageToEncode, new BitArray(key)).ToList();

            List<char> decodedMessage = cipher.Decode(encodedMessage.ToArray(), new BitArray(key)).ToList();

            Assert.Equal(messageToEncode, decodedMessage.ToArray());
        }



    }
}
