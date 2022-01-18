using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _db;

        public ProductRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<Product> Get(Guid id)
        {
            if (await _db.Products.CountAsync(p => p.Id == id) == 0)
                return null;

            return await _db.Products.FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task Create(Product item)
        {
            await _db.Products.AddAsync(item);
        }

        public async Task Update(Product item)
        {
            if (await _db.Products.CountAsync(p => p.Id == item.Id) == 0)
                return;

            _db.Products.Remove(await _db.Products.FirstAsync(p => p.Id == item.Id));
            await _db.Products.AddAsync(item);
        }

        public async Task Delete(Guid id)
        {
            if (await _db.Products.CountAsync(p => p.Id == id) == 0)
                return;

            _db.Products.Remove(await _db.Products.FirstAsync(p => p.Id == id));
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}