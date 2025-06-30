namespace StockApp.Domain.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; }
    }
}
