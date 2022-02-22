using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Implementation
{
    public class ProductScoreService: IProductScoreService
    {
        private readonly IMapper _mapper;
        private readonly IProductScoreRepository _productScoreRepository;

        public ProductScoreService(IProductScoreRepository scoreRepository, IMapper mapper)
        {
            _productScoreRepository = scoreRepository;
            _mapper = mapper;
        }

        public async Task<ProductScore> Get(Guid id)
        {
            if (await _productScoreRepository.Get(id) == null)
                throw new ArgumentNullException();

            return await _productScoreRepository.Get(id);
        }

        public async Task<IEnumerable<ProductScore>> GetAll()
        {
            return await _productScoreRepository.GetAll();
        }

        public async Task<IEnumerable<ProductScore>> GetAll(Guid id)
        {
            return (await _productScoreRepository.GetAll()).Where(ps => ps.ProductId == id).ToList();
        }

        public async Task Create(ProductScoreDto item)
        {
            await _productScoreRepository.Create(_mapper.Map<ProductScore>(item));
            await _productScoreRepository.Save();
        }

        public async Task Update(Guid id, ProductScoreDto item)
        {
            if (_productScoreRepository.Get(id) == null)
                throw new ArgumentNullException();

            var productScore = _mapper.Map<ProductScore>(item,
                opt => opt.AfterMap((_,
                    ps) => ps.Id = id));

            await _productScoreRepository.Update(productScore);
            await _productScoreRepository.Save();
        }

        public async Task Delete(Guid id)
        {
            if (_productScoreRepository.Get(id) == null)
                throw new ArgumentNullException();

            await _productScoreRepository.Delete(id);
        }
    }
}
