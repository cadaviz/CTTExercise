namespace CTTExercise.Tests
{
    using AutoFixture;

    public abstract class TestBase
    {
        protected Fixture Fixture { get; } = new Fixture();
    }
}
