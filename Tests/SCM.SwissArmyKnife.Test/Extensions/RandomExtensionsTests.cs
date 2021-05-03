using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using SCM.SwissArmyKnife.Extensions;
using Xunit;

namespace SCM.SwissArmyKnife.Test.Extensions
{
    public class RandomExtensionsTests
    {
        private readonly Random random = new ();

        [Fact]
        public void NextDouble_ReturnsADouble_WithinSpecifiedRange()
        {
            // Run it a couple of times just to make sure it works always
            for (int i = 0; i < 100; i++)
            {
                var randomDouble = this.random.NextDouble(0, 2);
                randomDouble.Should().BeGreaterThan(0);
                randomDouble.Should().BeLessThan(2);
            }
        }

        [Fact]
        public void NextByte_ReturnsAByte_WithinSpecifiedRange()
        {
            // Run it a couple of times just to make sure it works always
            for (int i = 0; i < 100; i++)
            {
                var randomDouble = this.random.NextByte();
                randomDouble.Should().BeGreaterOrEqualTo(byte.MinValue);
                randomDouble.Should().BeLessOrEqualTo(byte.MaxValue);
            }
        }

        [Fact]
        public void NextBoolean_ReturnsABoolean()
        {
            // Run it a couple of times just to make sure it works always
            var randomlyGeneratedBooleans = Enumerable.Repeat(0, 100)
                .Select(i => this.random.NextBoolean())
                .ToList();

            // Contains both trues and falses
            randomlyGeneratedBooleans.Should().Contain(true);
            randomlyGeneratedBooleans.Should().Contain(false);
        }

        [Fact]
        public void Choice_ReturnsARandomItem_FromEnumerable()
        {
            var list = new List<int> { 1, 2, 3 };

            for (int i = 0; i < 100; i++)
            {
                var randomChoice = this.random.Choice(list);
                list.Should().Contain(randomChoice);
            }
        }
    }
}
