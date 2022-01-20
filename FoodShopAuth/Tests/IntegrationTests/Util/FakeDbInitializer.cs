using DAL;

namespace Tests.IntegrationTests.Util
{
    internal class FakeDbInitializer
    {
        public static void Initialize(UserContext userContext)
        {
            var fake = new FakeData();

            userContext.Users.AddRange(fake.Users);
            userContext.SaveChanges();
        }
    }
}