using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using DAL.Models;

namespace Tests
{
    internal class FakeData
    {
        public FakeData()
        {
            Fixture = new Fixture();

            Products = Fixture.Build<Product>()
                .Without(p => p.ProductInfo)
                .CreateMany(10)
                .ToList();

            ProductsInfo = new List<ProductInfo>();


            foreach (var product in Products)
            {
                var productInfo = Fixture.Build<ProductInfo>()
                    .With(p => p.ProductId, product.Id)
                    .Without(p => p.Product)
                    .Create();
                ProductsInfo.Add(productInfo);
            }
        }

        public List<Product> Products { get; }

        public List<ProductInfo> ProductsInfo { get; }

        public Fixture Fixture { get; }
    }
}