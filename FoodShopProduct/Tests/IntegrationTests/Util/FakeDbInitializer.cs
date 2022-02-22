using DAL;

namespace Tests.IntegrationTests.Util
{
    internal class FakeDbInitializer
    {
        public static void Initialize(ProductContext context)
        {
            var fake = new FakeData();

            context.Products.AddRange(fake.Products);
            context.ProductInfos.AddRange(fake.ProductsInfo);
            context.SaveChanges();
        }
    }
}