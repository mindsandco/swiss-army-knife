using System;
using FluentAssertions;
using Xunit;

namespace ScadaMinds.SwissArmyKnife.Tests
{
    using SCM.SwissArmyKnife.Extensions;

    public class NullableExtensionsTests

    {
        [Fact]
        public void ShouldTransformExistingStructParameterToSameType()
        {
            int? number = 2;
            int? transformedNumber = number.TransformIfExists(i => i * 2);
            transformedNumber!.Value.Should().Be(4);
        }

        [Fact]
        public void ShouldTransformExistingStructParameterToOtherStructType()
        {
            double? number = 2.5;
            int? transformedNumber = number.TransformIfExists(i => Convert.ToInt32(i));
            transformedNumber!.Value.Should().Be(2);
        }

        [Fact]
        public void ShouldLeaveStructParameterAsNullIfSourceIsNull()
        {
            double? nullNumber = null;
            int? transformedNumber = nullNumber.TransformIfExists(i => Convert.ToInt32(i));
            transformedNumber.HasValue.Should().BeFalse();
        }

        [Fact]
        public void ShouldTransformExistingClassParameterToSameType()
        {
            string? number = "2";
            var numberToString = number.TransformIfExists(i => i + "2");
            numberToString.Should().Be("22");
        }

        [Fact]
        public void ShouldTransformExistingClassParameterToOtherType()
        {
            string? number = "2";
            Wrapper? stringWrapper = number.TransformIfExists(i => new Wrapper(i));
            stringWrapper!.MyString.Should().Be("2");
        }

        [Fact]
        public void ShouldLeaveClassParameterAsNullIfSourceIsNull()
        {
            string? nullString = null;
            string? transformedString = nullString.TransformIfExists(i => i + "2");
            transformedString.Should().BeNull();
        }

        [Fact]
        public void ShouldThrowErrorIfTransformationFunctionThrowsError()
        {
            double? number = 2.5;
            number.Invoking(n => n.TransformIfExists<double, double>(i => throw new ArgumentException("foo")))
                .Should().Throw<ArgumentException>();
        }

        private class Wrapper
        {
            public string MyString { get; set; }

            public Wrapper(string myString)
            {
                MyString = myString;
            }
        }
    }
}