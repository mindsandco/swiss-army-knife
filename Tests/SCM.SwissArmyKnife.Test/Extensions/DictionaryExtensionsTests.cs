using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
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
        public void SelectValues_IfValueExists()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                {"foo", "2"}
            };

            // Act
            var convertedValueDictionary = dictionary.SelectValues(oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));

            // Assert
            convertedValueDictionary.Should().ContainKey("foo").WhichValue.Should().Be(2);
        }

        [Fact]
        public void SelectValues_IfNoValueExist()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>();

            // Act
            Dictionary<string, int> convertedEmptyDictionary = dictionary.SelectValues(oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));

            // Assert
            convertedEmptyDictionary.Should().BeEmpty();
        }

        [Fact]
        public void SelectValues_ThrowsOnInvalidConvert()
        {
            // Arrange
            var dictionary = new Dictionary<string, string>
            {
                {"foo", "bar"}
            };

            // Act
            Action convertAction = () => dictionary.SelectValues(oldValue => int.Parse(oldValue, CultureInfo.InvariantCulture));

            // Assert
            convertAction
                .Should()
                .Throw<Exception>();
        }

        [Fact]
        public async Task AwaitTasksInValuesAsync_ShouldAwaitAllTasksInDictionary()
        {
            // Arrange
            var task1 = Task.FromResult(1);
            var task2 = Task.Run(async () =>
            {
                await Task.Delay(200);
                return 2;
            });
            var task3 = Task.Run(async () =>
            {
                await Task.Delay(500);
                return 3;
            });

            var dictionary = new Dictionary<string, Task<int>>() {{"1", task1}, {"2", task2}, {"3", task3}};

            // Act
            Dictionary<string, int> awaitedDictionary = await dictionary.AwaitTasksInValuesAsync();

            // Assert
            awaitedDictionary.Should().BeEquivalentTo(new Dictionary<string, int>()
            {
                {"1", 1}, {"2", 2}, {"3", 3}
            });
        }
    }
}
