using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using SCM.SwissArmyKnife.Extensions;
using Xunit;

namespace SCM.SwissArmyKnife.Test.Extensions
{
    public class HttpResponseMessageExtensionsTests
    {
        public static IEnumerable<object[]> SuccessfulStatusCodes = new[]
        {
            new object[] {200}, new object[] {201}, new object[] {202}, new object[] {203}, new object[] {204},
            new object[] {205}, new object[] {206}, new object[] {207}, new object[] {208}, new object[] {226}
        };

        [Theory]
        [MemberData(nameof(SuccessfulStatusCodes))]
        public async Task EnsureSuccessStatusCodeOrLogAsync_ShouldDoNothing_WhenStatusIsSuccessful(int statusCode)
        {
            var httpResponse = new HttpResponseMessage((HttpStatusCode)statusCode);

            await httpResponse.EnsureSuccessStatusCodeOrLogAsync((err, body) => { });
        }

        public static IEnumerable<object[]> ErrorStatusCodes = new[]
        {
            new object[] {400}, new object[] {401}, new object[] {403}, new object[] {404}, new object[] {500}
        };

        [Theory]
        [MemberData(nameof(ErrorStatusCodes))]
        public async Task EnsureSuccessStatusCodeOrLogAsync_ShouldCallProvidedFunction_IfAnErrorOccurs(int statusCode)
        {
            var httpResponse = new HttpResponseMessage((HttpStatusCode)statusCode)
            {
                Content = new StringContent("body!")
            };

            var loggingFunctionCalled = false;
            Func<Task> ensureSuccessFunction = async () => await httpResponse.EnsureSuccessStatusCodeOrLogAsync(
                (err, body) =>
                {
                    body.Should().Be("body!");
                    err.Message.Should().Contain($"{statusCode}");
                    loggingFunctionCalled = true;
                });

            // Assert that the logging function was called and we rethrow the exception
            await ensureSuccessFunction.Should().ThrowAsync<HttpRequestException>();
            loggingFunctionCalled.Should().BeTrue();
        }
    }
}
