namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; } // Chave primária
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Atenção: em produção, usar hash de senha
    }
}