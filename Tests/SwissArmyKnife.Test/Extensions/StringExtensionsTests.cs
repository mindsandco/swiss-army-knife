using System;
using FluentAssertions;
using SCM.SwissArmyKnife.Extensions;
using Xunit;

namespace ScadaMinds.SwissArmyKnife.Tests
{

    public class StringExtensionsTests
    {
        [Fact]
        public void Repeat_ShouldRepeatString_ASpecifiedAmountOfTimes()
        {
            "foo".Repeat(1).Should().Be("foo");
            "foo".Repeat(2).Should().Be("foofoo");
            "foo".Repeat(3).Should().Be("foofoofoo");
        }

        [Fact]
        public void Repeat_ShouldThrowArgumentException_IfTimesToRepeatIs0()
        {
            Action action = () => "foo".Repeat(0);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Truncate_ShouldThrowOnMaxLengthThatIsNegativeOrZero()
        {
            Action action = () => "foo".Truncate(0);
            action.Should().Throw<ArgumentOutOfRangeException>().WithMessage("*maxLength*");
        }

        [Fact]
        public void Truncate_ShouldAddThreeDotsIfStringWasTruncated()
        {
            "12345".Truncate(3).Should().Be("123...");
        }

        [Fact]
        public void Truncate_ShouldLeaveStringAsIs_IfItIsSmaller_ThanMaxLength()
        {
            "12345".Truncate(10).Should().Be("12345");
        }

        [Fact]
        public void Truncate_ShouldLeaveStringAsIs_IfItIsEqualTo_MaxLength()
        {
            "123".Truncate(3).Should().Be("123");
        }
    }
}
