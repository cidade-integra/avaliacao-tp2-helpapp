using Microsoft.AspNetCore.Http;

namespace StockApp.Application.DTOs
{
    public class ProductImageUploadDto
    {
        public int ProductId { get; set; }
        public IFormFile Image { get; set; } = null!;
    }
}
