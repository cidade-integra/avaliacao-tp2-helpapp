using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Text;

namespace StockApp.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly INotificationEmailService _notificationEmailService;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

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

        public async Task<IEnumerable<ProductDTO>> SearchAsync(ProductFilterDto filter)
        {
            var query = _productRepository.Query();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (!string.IsNullOrWhiteSpace(filter.Category))
                query = query.Where(p => p.Category.Name == filter.Category); // ajuste se categoria for diferente

            if (filter.MinQuantity.HasValue)
                query = query.Where(p => p.Quantity >= filter.MinQuantity.Value);

            if (filter.MaxQuantity.HasValue)
                query = query.Where(p => p.Quantity <= filter.MaxQuantity.Value);

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            var result = await query.ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(result);

        }
        
        public async Task UploadProductImageAsync(DTOs.ProductImageUploadDto dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product == null)
                throw new Exception("Produto não encontrado.");

            var fileName = $"{Guid.NewGuid()}_{dto.Image.FileName}";
            var filePath = Path.Combine("wwwroot", "uploads", fileName);
            
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Image.CopyToAsync(stream);
            }

            product.ImageUrl = $"/uploads/{fileName}";
            await _productRepository.UpdateAsync(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetLowStockAsync(int threshold)
        {
            var productsEntity = await _productRepository.GetLowStockAsync(threshold);
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }

        public async Task<IEnumerable<ProductDTO>> SearchProductsAsync(string query, string sortBy, bool descending)
        {
            var products = await _productRepository.SearchAsync(query, sortBy, descending);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<string> ExportProductsToCsvAsync()
        {
            var products = await _productRepository.GetProducts();
            var csv = new StringBuilder();
            csv.AppendLine("Id,Name,Description,Price,Stock");

            foreach (var p in products)
            {
                csv.AppendLine($"{p.Id},{Escape(p.Name)},{Escape(p.Description)},{p.Price},{p.Quantity}");
            }

            return csv.ToString();
        }

        #region Métodos privados
        private static string Escape(string? field)
        {
            if (string.IsNullOrWhiteSpace(field)) return "";
            return $"\"{field.Replace("\"", "\"\"")}\""; // escapando aspas
        }
        #endregion
    }
}
