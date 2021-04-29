// using System.Linq;
// using AIP.Shared.Events.Models;
// using AIP.Shared.Test.TestUtils;
// using AutoFixture;
// using FluentAssertions;
// using Newtonsoft.Json;
// using SCM.SwissArmyKnife.Compression;
// using Xunit;
//
// namespace AIP.Shared.Test.Tests.Utils
// {
//     // TOOD test GZip
//
//     // public class TestGzip : BaseTest
//     // {
//     //     [Fact]
//     //     public void Gzip_ZipsAndUnzips_ToTheSameObject()
//     //     {
//     //         // arrange
//     //         var samples = Fixture.CreateMany<IngestEventSample>(2)
//     //             .ToList();
//     //         var ingestEventBatch = Fixture.Build<IngestEventBatch>()
//     //             .With(ingest => ingest.Samples, samples)
//     //             .Create();
//     //         var serialized = JsonConvert.SerializeObject(ingestEventBatch);
//     //         var dataByteArray = GZip.Compress(serialized);
//     //
//     //         var decompressed = GZip.Decompress(dataByteArray);
//     //         var deserializedIngestEventBatch = JsonConvert.DeserializeObject<IngestEventBatch>(decompressed);
//     //
//     //         deserializedIngestEventBatch.Should().BeEquivalentTo(ingestEventBatch);
//     //     }
//
//
//     }
//
// }
