using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Implementation
{
    public class ProductInfoService : IProductInfoService
    {
        private readonly IMapper _mapper;
        private readonly IProductInfoRepository _productInfoRepository;

        public ProductInfoService(IProductInfoRepository productInfoRepository, IMapper mapper)
        {
            _productInfoRepository = productInfoRepository;
            _mapper = mapper;
        }

        public async Task<ProductInfo> Get(Guid id)
        {
            if (await _productInfoRepository.Get(id) == null)
                throw new ArgumentException();

            return await _productInfoRepository.Get(id);
        }

        public async Task<IEnumerable<ProductInfo>> GetAll()
        {
            return await _productInfoRepository.GetAll();
        }

        public async Task Create(ProductInfoDto item)
        {
            var productInfo = _mapper.Map<ProductInfo>(item,
                opt => opt.AfterMap((_,
                    p) => p.LastTimeModify = DateTime.Now));

            await _productInfoRepository.Create(productInfo);
            await _productInfoRepository.Save();
        }

        public async Task Update(Guid id, ProductInfoDto item)
        {
            if (await _productInfoRepository.Get(id) == null)
                throw new ArgumentException();

            var productInfo = _mapper.Map<ProductInfo>(item,
                opts => opts.AfterMap((_,
                    p) =>
                {
                    p.Id = id;
                    p.LastTimeModify = DateTime.Now;
                }));

            await _productInfoRepository.Update(productInfo);

            await _productInfoRepository.Save();
        }

        public async Task Delete(Guid id)
        {
            if (await _productInfoRepository.Get(id) == null)
                throw new ArgumentException();

            await _productInfoRepository.Delete(id);
            await _productInfoRepository.Save();
        }
    }
}