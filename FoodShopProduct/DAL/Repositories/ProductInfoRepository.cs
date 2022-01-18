using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductInfoRepository : IProductInfoRepository
    {
        private readonly ProductContext _db;

        public ProductInfoRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<ProductInfo> Get(Guid id)
        {
            if (await _db.ProductInfos.CountAsync(p => p.Id == id) == 0)
                return null;

            return await _db.ProductInfos.FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductInfo>> GetAll()
        {
            return await _db.ProductInfos.ToListAsync();
        }

        public async Task Create(ProductInfo item)
        {
            await _db.ProductInfos.AddAsync(item);
        }

        public async Task Update(ProductInfo item)
        {
            if (await _db.ProductInfos.CountAsync(p => p.Id == item.Id) == 0)
                return;

            _db.ProductInfos.Remove(await _db.ProductInfos.FirstAsync(p => p.Id == item.Id));
            await _db.ProductInfos.AddAsync(item);
        }

        public async Task Delete(Guid id)
        {
            if (await _db.ProductInfos.CountAsync(p => p.Id == id) == 0)
                return;

            _db.ProductInfos.Remove(await _db.ProductInfos.FirstAsync(p => p.Id == id));
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}