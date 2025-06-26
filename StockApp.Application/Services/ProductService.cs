using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Security.AccessControl;

namespace StockApp.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IMapper _mapper;
        private readonly INotificationEmailService _notificationEmailService;

        public ProductService(IProductRepository productRepository, 
            IMapper mapper, 
            INotificationEmailService notificationEmailService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _notificationEmailService = notificationEmailService;
        }

        public async Task<ProductDTO> Add(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            var createProduct = await _productRepository.Create(productEntity);
            return _mapper.Map<ProductDTO>(createProduct);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productsEntity = await _productRepository.GetProducts();
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<ProductDTO> GetProductById(int? id)
        {
            var productEntity = _productRepository.GetById(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task Remove(int? id)
        {
            var productEntity = _productRepository.GetById(id).Result;
            await _productRepository.Remove(productEntity);
        }

        public async Task Update(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.Update(productEntity);
        }

    }
}