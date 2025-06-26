using StockApp.Domain.Validation;
using System;


namespace StockApp.Domain.Entities
{
    public class Supplier
    {
        #region Atributos
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactEmail { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Purchase> Purchases { get; set; }
        #endregion

        #region Construtores
        public Supplier(string name, string contactEmail, string phoneNumber)
        {
            ValidadeDomain(name, contactEmail, phoneNumber);
        }
        public Supplier(int id, string name, string contactEmail, string phoneNumber)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id Value");
            Id = id;
            ValidadeDomain(name, contactEmail, phoneNumber);
        }
        public ICollection<Product> Products { get; set; }
        #endregion

        #region Validação
        private void ValidadeDomain(string name, string contactEmail, string phoneNumber)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name, name is required.");
            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characteres");

            DomainExceptionValidation.When(string.IsNullOrEmpty(contactEmail),
                "Invalid email, contact Email is required.");
            DomainExceptionValidation.When(!contactEmail.Contains("@"),
                "Invalid email, missing @ character.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(phoneNumber),
                "Invalid phone number, phone is required.");
            DomainExceptionValidation.When(phoneNumber.Length < 8,
                "Invalid phone number, too short, minimum 8 characters.");

            Name = name;
            ContactEmail = contactEmail;
            PhoneNumber = phoneNumber;
        }
        #endregion

    }
}
