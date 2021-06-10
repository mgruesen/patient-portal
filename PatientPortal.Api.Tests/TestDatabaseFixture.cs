using Xunit;

namespace PatientPortal.Api.Tests
{
    public class TestDatabaseFixture : IClassFixture<TestDatabaseContext>
    {
        public TestDatabaseFixture(TestDatabaseContext context) => Fixture = context;
        public TestDatabaseContext Fixture { get; }
    }
}
