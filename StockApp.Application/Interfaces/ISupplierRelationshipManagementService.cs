using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface ISupplierRelationshipManagementService
    {
        Task<SupplierDto> EvaluateSupplierAsync(int supplierId);
        Task<SupplierDto> RenewContractAsync(int supplierId);
    }
}

