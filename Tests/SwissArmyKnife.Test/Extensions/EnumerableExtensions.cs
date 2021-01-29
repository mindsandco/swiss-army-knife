using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using SCM.SwissArmyKnife.Extensions;

namespace ScadaMinds.SwissArmyKnife.Tests
{

    public class EnumerableExtensions
    {
        [Fact]
        public void Yield_ShouldProduceSameObject_AsEnumerable()
        {
            // Arrange
            var itemToYield = "foo";

            // Act
            var yieldedItem = itemToYield.Yield();

            // Assert
            yieldedItem.Single().Should().Be(itemToYield);
        }


        [Fact]
        public async Task YieldAsync_ShouldProduceSameObject_AsAsyncEnumerable()
        {
            // Arrange
            var itemToYield = "foo";

            // Act
            var yieldedItem = itemToYield.YieldAsync();

            // Assert - iterate through and add to list
            var items = new List<string>();
            await foreach (var result in yieldedItem)
            {
                items.Add(result);
            }

            // One yielded item
            items.Single().Should().Be(itemToYield);
        }
    }
}
