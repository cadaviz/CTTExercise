namespace CTTExercise.Tests.Persistence.Setup
{
    using CTTExercise.Persistence.Setup;
    using CTTExercise.Tests;
    using FluentAssertions;

    public class DatabaseSettingsTests : TestBase
    {
        [Fact]
        public void DatabaseSettings_ShouldBeCreated_WhenValidParametersAreProvided()
        {
            // Arrange
            var connectionString = "Server=myServerAddress;Database=myDataBase;";
            var databaseName = "MyDatabase";
            var user = "admin";
            var password = "password";

            // Act
            var dbSettings = new DatabaseSettings(connectionString, databaseName, user, password);

            // Assert
            dbSettings.ConnectionString.Should().Be(connectionString);
            dbSettings.DatabaseName.Should().Be(databaseName);
            dbSettings.User.Should().Be(user);
            dbSettings.Password.Should().Be(password);
        }

        [Fact]
        public void DatabaseSettings_ShouldAllowNullUserAndPassword_WhenTheyAreNotProvided()
        {
            // Arrange
            var connectionString = "Server=myServerAddress;Database=myDataBase;";
            var databaseName = "MyDatabase";

            // Act
            var dbSettings = new DatabaseSettings(connectionString, databaseName, null, null);

            // Assert
            dbSettings.User.Should().BeNull();
            dbSettings.Password.Should().BeNull();
        }

        [Theory]
        [InlineData("", "databaseName")]
        [InlineData("connectionString", "")]
        public void DatabaseSettings_ShouldThrowArgumentException_WhenMandatoryDataWasEmpty(string connectionString, string databaseName)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new DatabaseSettings(connectionString, databaseName, "user", "password"));
        }

        [Theory]
        [InlineData(null, "databaseName")]
        [InlineData("connectionString", null)]
        public void DatabaseSettings_ShouldThrowArgumentException_WhenMandatoryDataWasNull(string? connectionString, string? databaseName)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new DatabaseSettings(connectionString!, databaseName!, "user", "password"));
        }
    }
}
