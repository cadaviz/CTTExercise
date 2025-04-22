namespace CTTExercise.Tests.Shared.Extensions
{
    using FluentAssertions;
    using System.Collections.Generic;
    using Xunit;

    public class EnumerableExtensionsTests : TestBase
    {
        [Theory]
#pragma warning disable CA1861
        [InlineData(null, true)]
        [InlineData(new int[] { }, true)]
        [InlineData(new int[] { 1, 2, 3 }, false)]
        [InlineData(new int[] { 1 }, false)]
#pragma warning restore CA1861
        public void IsNullOrEmpty_ShouldReturnExpectedResult(IEnumerable<int>? list, bool expectedResult)
        {
            // Act
            var result = list!.IsNullOrEmpty();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
