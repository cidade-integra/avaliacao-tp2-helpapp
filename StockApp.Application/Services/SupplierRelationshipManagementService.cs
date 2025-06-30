using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.Application.Services
{
    public class SupplierRelationshipManagementService :
ISupplierRelationshipManagementService
    {
        public async Task<SupplierDto> EvaluateSupplierAsync(int supplierId)
        {
            return new SupplierDto
            {
                Id = supplierId,
                Name = $"Fornecedor {supplierId}",
                EvaluationScore = new Random().Next(60, 100),
            LastEvaluationDate = DateTime.Now,
                ContractRenewalDate = DateTime.Now.AddYears(1),
                Status = "Ativo"
            };
        }
        public async Task<SupplierDto> RenewContractAsync(int supplierId)
        {
            return new SupplierDto
            {
                Id = supplierId,
                Name = $"Fornecedor {supplierId}",
                EvaluationScore = 95,
                LastEvaluationDate = DateTime.Now.AddMonths(-6),
                ContractRenewalDate = DateTime.Now.AddYears(1),
                Status = "Contrato Renovado"
            };
        }
    }
}
