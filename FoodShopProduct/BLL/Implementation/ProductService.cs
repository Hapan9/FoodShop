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
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> Get(Guid id)
        {
            if (await _productRepository.Get(id) == null)
                throw new ArgumentException();

            return await _productRepository.Get(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _productRepository.GetAll();
        }

        public async Task Create(ProductDto item)
        {
            await _productRepository.Create(_mapper.Map<Product>(item));
            await _productRepository.Save();
        }

        public async Task Update(Guid id, ProductDto item)
        {
            if (await _productRepository.Get(id) == null)
                throw new ArgumentException();

            await _productRepository.Update(_mapper.Map<Product>(item,
                opts => opts.AfterMap((_,
                    p) => p.Id = id)));

            await _productRepository.Save();
        }

        public async Task Delete(Guid id)
        {
            if (await _productRepository.Get(id) == null)
                throw new ArgumentException();

            await _productRepository.Delete(id);
            await _productRepository.Save();
        }
    }
}