using System;
using System.Collections.Generic;
using System.Globalization;
using FluentAssertions;
using SCM.SwissArmyKnife.Extensions;
using Xunit;

namespace SCM.SwissArmyKnife.Test.Extensions
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

        [Fact]
        public void ConvertValuesToNewType_IfValueExists()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                {"foo", "2"}
            };

            // Act
            var convertedValueDictionary = dictionary.ConvertValuesToNewType<string, string, int>(oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));

            // Assert
            convertedValueDictionary.TryGetValue("foo", out var returnValue);

            returnValue.Should().BeOfType(typeof(int));
        }

        [Fact]
        public void ConvertValyesToNewType_IfNoValueExist()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>();

            // Act
            var convertedEmptyDictionary = dictionary.ConvertValuesToNewType<string, string, int>(oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));

            // Assert
            var arguments = convertedEmptyDictionary.GetType().GetGenericArguments();

            convertedEmptyDictionary.Should().BeEmpty();
            arguments[0].Should().Be(typeof(string));
            arguments[1].Should().Be(typeof(int));
        }

        [Fact]
        public void ConvertValuesToNewType_ThrowsOnInvalidConvert()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                {"foo", "bar"}
            };

            // Act
            Action convertAction = () => dictionary.ConvertValuesToNewType<string, string, int>(oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));

            // Assert
            convertAction
                .Should()
                .Throw<InvalidOperationException>()
                .WithMessage("Tried converting the value but encountered an error");
        }
    }
}
