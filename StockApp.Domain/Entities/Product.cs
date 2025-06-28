using StockApp.Domain.Validation;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Domain.Entities
{
    public class Product
    {
        #region Atributos

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public int Stock { get; set; }

        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public string? ImageUrl { get; set; }

        #endregion

        #region Construtores

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }

        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id < 0, "Update Invalid Id value");
            Id = id;
            ValidateDomain(name, description, price, stock, image);
        }

        #endregion

        #region Métodos

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name, name is required.");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(description),
                "Invalid description, name is required.");

            DomainExceptionValidation.When(description.Length < 5,
                "Invalid description, too short, minimum 5 characters.");

            DomainExceptionValidation.When(price < 0, "Invalid price negative value.");

            DomainExceptionValidation.When(stock < 0, "Invalid stock negative value.");

            DomainExceptionValidation.When(image.Length > 250, "Invalid image name, too long, maximum 250 characters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(image), "Invalid image address, image is required.");

            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;

        }

        #endregion
    }
}