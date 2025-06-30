using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SupplierRelationshipManagementController : ControllerBase
    {
        private readonly ISupplierRelationshipManagementService _srmService;
        public
        SupplierRelationshipManagementController(ISupplierRelationshipManagementService
        srmService)
        {
            _srmService = srmService;
        }
        [HttpGet("evaluate/{supplierId}")]
        public async Task<IActionResult> EvaluateSupplier(int supplierId)
        {
            var supplier = await _srmService.EvaluateSupplierAsync(supplierId);
            if (supplier == null)
            {
                return NotFound($"Fornecedor com ID {supplierId} não encontrado.");
            }
            return Ok(supplier);
        }
        [HttpPost("renew-contract/{supplierId}")]
        public async Task<IActionResult> RenewContract(int supplierId)
        {
            var supplier = await _srmService.RenewContractAsync(supplierId);
            if (supplier == null)
            {
                return NotFound($"Fornecedor com ID {supplierId} não encontrado.");
            }
            return Ok(supplier);
        }

    }
}
