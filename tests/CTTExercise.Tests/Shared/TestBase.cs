namespace CTTExercise.Tests.Shared
{
    using AutoFixture;

    public abstract class TestBase
    {
        protected Fixture Fixture { get; } = new Fixture();
    }
}
