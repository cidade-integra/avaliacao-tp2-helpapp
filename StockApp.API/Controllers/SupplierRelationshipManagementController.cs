using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pela gestão do relacionamento com fornecedores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierRelationshipManagementController : ControllerBase
    {
        private readonly ISupplierRelationshipManagementService _srmService;

        public SupplierRelationshipManagementController(ISupplierRelationshipManagementService srmService)
        {
            _srmService = srmService;
        }

        /// <summary>
        /// Avalia um fornecedor pelo seu ID.
        /// </summary>
        /// <param name="supplierId">ID do fornecedor.</param>
        /// <returns>Dados da avaliação do fornecedor.</returns>
        /// <response code="200">Avaliação retornada com sucesso.</response>
        /// <response code="404">Fornecedor não encontrado.</response>
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

        /// <summary>
        /// Renova o contrato com um fornecedor específico.
        /// </summary>
        /// <param name="supplierId">ID do fornecedor.</param>
        /// <returns>Dados atualizados do fornecedor após renovação.</returns>
        /// <response code="200">Contrato renovado com sucesso.</response>
        /// <response code="404">Fornecedor não encontrado.</response>
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