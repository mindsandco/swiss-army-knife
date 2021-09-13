using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AutoFixture.Xunit2;
using FluentAssertions;
using SCM.SwissArmyKnife.TestUtils;
using Xunit;

namespace SCM.SwissArmyKnife.Test.TestUtils
{
    public class SameValueDictionaryTests
    {
        private const string DefaultStringValue = "defaultStringValue";
        private readonly SameValueDictionary<string, string> sut = new(DefaultStringValue);

        [Theory]
        [AutoData]
        public void Get_ShouldAlwaysReturnTheValueSetInConstructor(string keyName)
        {
            this.sut[keyName].Should().Be(DefaultStringValue);
        }

        [Theory]
        [AutoData]
        public void Get_ShouldAlwaysReturnTheValueSetInConstructor_EvenThoughAValueHasBeenSet(string keyName)
        {
            // Try adding a few different ways
            this.sut[keyName] = "someOtherValue";
            this.sut.TryAdd(keyName, "someOtherValue");
            this.sut[keyName].Should().Be(DefaultStringValue);
        }

        [Theory]
        [AutoData]
        public void Count_ShouldAlwaysHaveCount1(string keyName)
        {
            // Try adding a few different ways
            this.sut[keyName] = "someOtherValue";
            this.sut.TryAdd(keyName, "someOtherValue");
            this.sut.Count.Should().Be(1);
        }
    }
}
