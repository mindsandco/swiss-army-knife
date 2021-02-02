using FluentAssertions;
using SCM.SwissArmyKnife.Extensions;
using Xunit;

namespace SCM.SwissArmyKnife.Test.Extensions
{
    public class ObjectExtensionTests
    {
        [Fact]
        public void AsJson_ShouldConvertObject_ToString_And_SerializeEnums_AsString()
        {
            // Arrange
            var testModel = new TestModel() { MyValue = 3, MyEnum = TestEnum.SecondOption };

            // Act
            var jsonString = testModel.AsJson();

            // Assert.
            jsonString.Should().Be(@"{""MyValue"":3,""MyEnum"":""SecondOption""}");
        }

        [Fact]
        public void AsJsonIndented_ShouldConvertObject_ToString_And_SerializeEnums_AsString()
        {
            // Arrange
            var testModel = new TestModel() { MyValue = 3, MyEnum = TestEnum.SecondOption };

            // Act
            var jsonString = testModel.AsIndentedJson();

            // Assert.
            jsonString.Should().Be(@"{
  ""MyValue"": 3,
  ""MyEnum"": ""SecondOption""
}");
        }
    }

    public class TestModel
    {
        public int MyValue { get; set; }
        public TestEnum MyEnum { get; set; }
    }

    public enum TestEnum
    {
        FirstOption,
        SecondOption
    }
}
