namespace Application.DTOs.Auth
{
    public class TokenResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

        // Campos adicionais, se desejar:
        // public string UserEmail { get; set; } = string.Empty;
        // public string UserName { get; set; } = string.Empty;
    }
}