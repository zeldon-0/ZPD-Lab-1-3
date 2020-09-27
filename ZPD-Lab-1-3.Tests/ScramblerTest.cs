using System;
using Xunit;
using System.Collections;

namespace ZPD_Lab_1_3.Tests
{
    public class ScramblerTest
    {
        [Theory]
        [InlineData(new bool[] { true, true, true, true, false, false, true},
            new bool[] { false, false, true, false, false, false, true},
            1,
            new bool[] { true, true, true, false, false, true, false })]
        [InlineData(new bool[] { true, true, true, true, false, false, true },
            new bool[] { false, false, true, false, false, false, true },
            2,
            new bool[] { true, true, false, false, true, false, true })]
        [InlineData(new bool[] { true, true, true, true, false, false, true },
            new bool[] { false, false, true, false, false, false, true },
            3,
            new bool[] { true, false, false, true, false, true, true })]
        public void Scrambler_SequenceAndKeyAndRoundCount_ReturnsScrambledSequence(bool[] sequence, bool[] key, int rounds, bool[] expectedSequence)
        {
            Scrambler scrambler = new Scrambler(sequence, key);

            BitArray actualSequence = scrambler.Scramble(rounds);

            bool[] sequenceAsBoolArray = new bool[actualSequence.Length];

            actualSequence.CopyTo(sequenceAsBoolArray, 0);


            Assert.Equal(expectedSequence, sequenceAsBoolArray);
        }
    }
}
