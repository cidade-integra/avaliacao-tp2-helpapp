namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; } // ID único do usuário
        public string Email { get; set; } // E-mail usado no login
        public string Password { get; set; } // Senha (armazenada em texto ou hash)

        // Você pode adicionar outros campos depois, como:
        // public string Name { get; set; }
        // public string Role { get; set; }
    }
}
