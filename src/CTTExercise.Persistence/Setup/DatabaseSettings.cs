namespace CTTExercise.Persistence.Setup
{
    public sealed class DatabaseSettings
    {
        public DatabaseSettings(string connectionString, string databaseName, string? user, string? password)
        {
            ArgumentException.ThrowIfNullOrEmpty(connectionString);
            ArgumentException.ThrowIfNullOrEmpty(databaseName);

            ConnectionString = connectionString;
            DatabaseName = databaseName;
            User = user;
            Password = password;
        }

        public string ConnectionString { get; }
        public string DatabaseName { get; }
        public string? User { get; }
        public string? Password { get; }
    }
}
