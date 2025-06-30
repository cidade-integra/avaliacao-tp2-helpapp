namespace StockApp.Application.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EvaluationScore { get; set; }
        public DateTime LastEvaluationDate { get; set; }
        public DateTime ContractRenewalDate { get; set; }
        public string Status { get; set; }
    }
}