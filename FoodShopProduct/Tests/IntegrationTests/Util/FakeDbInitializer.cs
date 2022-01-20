using DAL;

namespace Tests.IntegrationTests.Util
{
    internal class FakeDbInitializer
    {
        public static void Initialize(ProductContext productContext)
        {
            var fake = new FakeData();

            productContext.Products.AddRange(fake.Products);
            productContext.ProductInfos.AddRange(fake.ProductsInfo);
            productContext.SaveChanges();
        }
    }
}