namespace CTTExercise.Tests.Shared.Extensions
{
    using CTTExercise.Shared.Extensions;
    using FluentAssertions;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System;
    using System.Text.Json;

    public class LogExtensionsTests : TestBase
    {
        private readonly Mock<ILogger> _loggerMock;
        private static readonly JsonSerializerOptions jsonSerializerOptions = new() { WriteIndented = false };

        public LogExtensionsTests()
        {
            _loggerMock = new Mock<ILogger>();
        }

        [Fact]
        public void SerializeForLog_ShouldThrowArgumentNullException_WhenSourceIsNull()
        {
            // Arrange & Act
            var action = () => ((object?)null).SerializeForLog();

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SerializeForLog_ShouldSerializeObjectToJson()
        {
            // Arrange
            var obj = new { Name = "Test", Age = 30 };

            // Act
            var json = obj.SerializeForLog();

            // Assert
            json.Should().Be(JsonSerializer.Serialize(obj, jsonSerializerOptions));
        }

        [Fact]
        public void LogDebugIfEnabled_ShouldLog_WhenDebugLevelIsEnabled()
        {
            // Arrange
            _loggerMock.Setup(l => l.IsEnabled(LogLevel.Debug))
                .Returns(true);
            string message = "Debug message";

            // Act
            _loggerMock.Object.LogDebugIfEnabled(message);

            // Assert
            _loggerMock.Verify(
                l => l.Log(
                    It.Is<LogLevel>(level => level == LogLevel.Debug),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, type) => @object.ToString()!.Contains(message)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public void LogDebugIfEnabled_ShouldNotLog_WhenDebugLevelIsDisabled()
        {
            // Arrange
            _loggerMock.Setup(l => l.IsEnabled(LogLevel.Debug)).Returns(false);
            string message = "Debug message";

            // Act
            _loggerMock.Object.LogDebugIfEnabled(message);

            // Assert
            _loggerMock.Verify(
                l => l.Log(
                    It.Is<LogLevel>(level => level == LogLevel.Debug),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, type) => @object.ToString()!.Contains(message)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Never);
        }

        [Fact]
        public void LogInformationIfEnabled_ShouldLog_WhenInformationLevelIsEnabled()
        {
            // Arrange
            _loggerMock.Setup(l => l.IsEnabled(LogLevel.Information)).Returns(true);
            string message = "Info message";

            // Act
            _loggerMock.Object.LogInformationIfEnabled(message);

            // Assert
            _loggerMock.Verify(
                l => l.Log(
                    It.Is<LogLevel>(level => level == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, type) => @object.ToString()!.Contains(message)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public void LogInformationIfEnabled_ShouldNotLog_WhenInformationLevelIsDisabled()
        {
            // Arrange
            _loggerMock.Setup(l => l.IsEnabled(LogLevel.Information)).Returns(false);
            string message = "Info message";

            // Act
            _loggerMock.Object.LogInformationIfEnabled(message);

            // Assert
            _loggerMock.Verify(
                l => l.Log(
                    It.Is<LogLevel>(level => level == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, type) => @object.ToString()!.Contains(message)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Never);
        }

        [Fact]
        public void LogDebugIfEnabled_ShouldSerializeComplexArguments()
        {
            // Arrange
            _loggerMock.Setup(l => l.IsEnabled(LogLevel.Debug)).Returns(true);
            var complexObj = new { Name = "Test", Value = 42 };
            string message = "Complex object log {ComplexObject}";

            // Act
            _loggerMock.Object.LogDebugIfEnabled(message, complexObj);

            // Assert
            _loggerMock.Verify(
                l => l.Log(
                    It.Is<LogLevel>(level => level == LogLevel.Debug),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, type) => @object.ToString()!.Contains(complexObj.SerializeForLog())),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()
                ),
                Times.Once);
        }

        [Theory]
        [InlineData("test")]
        [InlineData(1)]
        [InlineData(null)]
        public void LogDebugIfEnabled_ShouldNotSerializePrimitiveArguments(object? primitiveArguments)
        {
            // Arrange
            _loggerMock.Setup(l => l.IsEnabled(LogLevel.Debug)).Returns(true);
            string message = "Simple object log with {PrimitiveArguments}";
            var expectedValue = primitiveArguments is null ? "null" : primitiveArguments.ToString();

            // Act
            _loggerMock.Object.LogDebugIfEnabled(message, primitiveArguments!);

            // Assert
            _loggerMock.Verify(
                l => l.Log(
                    It.Is<LogLevel>(level => level == LogLevel.Debug),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((@object, type) => @object.ToString()!.Contains(expectedValue!)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()
                ),
                Times.Once);
        }
    }
}
