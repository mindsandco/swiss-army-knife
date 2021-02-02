using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SCM.SwissArmyKnife.Extensions;
using Xunit;

namespace SCM.SwissArmyKnife.Test.Extensions
{
    public class FluentTaskExtensionsTests
    {
        [Fact]
        public async Task Select_ShouldWorkWith_Task_EmittingSingleValue()
        {
            var result = await GetTask(2).Select(i => i * 2);
            result.Should().Be(4);
        }

        [Fact]
        public async Task Select_ShouldWorkWith_Task_EmittingAnEnumerable()
        {
            var result = await GetTask(new int[] { 1, 2, 3 }.AsEnumerable())
                .Select(i => i + 1);

            result.Should().BeEquivalentTo(2, 3, 4);
        }

        [Fact]
        public async Task Select_ShouldWorkWith_Task_EmittingAnArray()
        {
            var result = await GetTask(new int[] { 1, 2, 3 })
                .Select(i => i + 1);

            result.Should().BeEquivalentTo(2, 3, 4);
        }

        [Fact]
        public async Task Select_ShouldWorkWith_Task_EmittingAList()
        {
            var result = await GetTask(new List<int> { 1, 2, 3 })
                .Select(i => i + 1);

            result.Should().BeEquivalentTo(2, 3, 4);
        }

        [Fact]
        public async Task First_ShouldWorkWith_Enumerable()
        {
            var result = await GetTask(new int[] { 1, 2 }.AsEnumerable())
                .First();

            result.Should().Be(1);
        }

        [Fact]
        public async Task First_ShouldWorkWith_List()
        {
            var result = await GetTask(new List<int> { 1, 2 })
                .First();

            result.Should().Be(1);
        }

        [Fact]
        public async Task First_ShouldWorkWith_Array()
        {
            var result = await GetTask(new int[] { 1, 2 })
                .First();

            result.Should().Be(1);
        }

        [Fact]
        public async Task First_ShouldThrowError_IfNoFirstFound()
        {
            // Same behaviour as regular "first"
            Func<Task> callThatWillThrow = async () => await GetTask(Array.Empty<int>())
                .First();

            await callThatWillThrow.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task SelectMany_ShouldCollapseEnumerablesFromTask()
        {
            var taskk = Task.FromResult(new List<int[]> { new int[] { 1, 2 }, new int[] { 3, 4 } }.AsEnumerable());
            await taskk.SelectMany(i => i);

            var result = await GetTask(new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } }.AsEnumerable())
                .SelectMany(i => i);

            result.Should().BeEquivalentTo(1, 2, 3, 4, 5, 6);
        }

        [Fact]
        public async Task SelectMany_ShouldCollapse_AndTransform_FromTaskReturning_Enumerable()
        {
            var result = await GetTask(new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } }.AsEnumerable())
                .SelectMany(i => i.Append(9));

            result.Should().BeEquivalentTo(1, 2, 3, 9, 4, 5, 6, 9);
        }

        [Fact]
        public async Task SelectMany_ShouldCollapse_AndTransform_FromTaskReturning_Array()
        {
            var result = await GetTask(new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } })
                .SelectMany(i => i.Append(9));

            result.Should().BeEquivalentTo(1, 2, 3, 9, 4, 5, 6, 9);
        }

        [Fact]
        public async Task SelectMany_ShouldCollapse_AndTransform_FromTaskReturning_List()
        {
            var result = await GetTask(new List<int[]> { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } })
                .SelectMany(i => i.Append(9));

            result.Should().BeEquivalentTo(1, 2, 3, 9, 4, 5, 6, 9);
        }

        [Fact]
        public async Task ToList_ShouldCollapseEnumerableToList()
        {
            var result = await GetTask(new string[] { "1", "2" }.AsEnumerable()).ToList();

            result.Should().BeOfType<List<string>>();
            result.Should().BeEquivalentTo("1", "2");
        }



        private static async Task<T> GetTask<T>(T returnValue)
        {
            // Just to ensure we're actually not returning a completed task
            await Task.Delay(10);
            return returnValue;
        }
    }
}
