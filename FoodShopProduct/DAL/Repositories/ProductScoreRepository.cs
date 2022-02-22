using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductScoreRepository: IProductScoreRepository
    {
        private readonly ProductContext _db;

        public ProductScoreRepository(ProductContext db)
        {
            _db = db;
        }

        public async Task<ProductScore> Get(Guid id)
        {
            if (await _db.ProductScores.CountAsync(p => p.Id == id) == 0)
                return null;

            return await _db.ProductScores.FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductScore>> GetAll()
        {
            return await _db.ProductScores.ToListAsync();
        }

        public async Task Create(ProductScore item)
        {
            await _db.ProductScores.AddAsync(item);
        }

        public async Task Update(ProductScore item)
        {
            if (await _db.ProductScores.CountAsync(p => p.Id == item.Id) == 0)
                return;

            _db.ProductScores.Remove(await _db.ProductScores.FirstAsync(p => p.Id == item.Id));
            await _db.ProductScores.AddAsync(item);
        }

        public async Task Delete(Guid id)
        {
            if (await _db.ProductScores.CountAsync(p => p.Id == id) == 0)
                return;

            _db.ProductScores.Remove(await _db.ProductScores.FirstAsync(p => p.Id == id));
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
