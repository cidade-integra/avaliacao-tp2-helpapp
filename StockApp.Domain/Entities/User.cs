using StockApp.Domain.Validation;
using System;

namespace StockApp.Domain.Entities
{
    public class User
    {
        #region Atributos
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        #endregion

        #region Construtores
        public User(string userName, string email, string passwordHash)
        {
            ValidateDomain(userName, email, passwordHash);
        }
        public User(int id, string userName, string email, string passwordHash)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id Value");
            Id = id;
            ValidateDomain(userName, email, passwordHash);
        }
        #endregion

        #region Validações
        private void ValidateDomain(string username, string email, string passwordHash)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(username),
                "Username is required.");
            DomainExceptionValidation.When(username.Length < 3,
                "UserName must be at least 3 characters.");
            DomainExceptionValidation.When(username.Length > 80,
                "Username cannot exceed 80 characters");

            DomainExceptionValidation.When(string.IsNullOrEmpty(email),
                "Email is required.");
            DomainExceptionValidation.When(email.Length > 250,
                "Email cannot exceed 250 characters.");

            DomainExceptionValidation.When(string.IsNullOrEmpty(passwordHash),
                "Password hash is required.");
        }
        public void UpdateRole (string role)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(role),
                "Role is required");
            Role = role;
        }
        #endregion
    }
}
