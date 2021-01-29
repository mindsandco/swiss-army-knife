using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using SCM.SwissArmyKnife.Extensions;

namespace ScadaMinds.SwissArmyKnife.Tests
{
    public class DictionaryExtensionsTests
    {

        [Fact]
        public void GetOrThrow_ShouldReturnValue_IfItExists()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                {"foo", "bar"}
            };

            // Act
            var returnedValue = dictionary.GetOrThrow("foo", () => new Exception());

            // Assert
            returnedValue.Should().Be("bar");
        }

        [Fact]
        public void GetOrThrow_ShouldThrowExceptionProducedByFactory_IfNoValueExists()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>();

            // Act
            var exceptionToThrow = new ArgumentException("foo");
            Action tryGet = () => dictionary.GetOrThrow("foo", () => exceptionToThrow);

            // Assert
            tryGet.Should().ThrowExactly<ArgumentException>().Where(e => e == exceptionToThrow);
        }


        [Fact]
        public void GetValueOr_ShouldReturnValueCreatedByValueFactory_IfNoCurrentValueExists()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>();

            // Act
            var returnedValue = dictionary.GetValueOr("foo", () => "bar");

            // Assert
            returnedValue.Should().Be("bar");
        }

        [Fact]
        public void GetValueOr_ShouldReturnValue_IfItExists()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                {"foo", "bar"}
            };

            // Act
            var returnedValue = dictionary.GetValueOr("foo", () => string.Empty);

            // Assert
            returnedValue.Should().Be("bar");
        }

    }
}
