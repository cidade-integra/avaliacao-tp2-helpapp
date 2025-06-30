using Microsoft.AspNetCore.Mvc;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador para gerenciar fornecedores.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        /// <summary>
        /// Retorna todos os fornecedores cadastrados.
        /// </summary>
        /// <returns>Lista de fornecedores.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
        {
            var suppliers = await _supplierRepository.GetSuppliers();
            return Ok(suppliers);
        }

        /// <summary>
        /// Retorna um fornecedor pelo ID.
        /// </summary>
        /// <param name="id">ID do fornecedor.</param>
        /// <returns>Fornecedor encontrado ou status 404 se não existir.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetById(int id)
        {
            var supplier = await _supplierRepository.GetById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        /// <summary>
        /// Cadastra um novo fornecedor.
        /// </summary>
        /// <param name="supplier">Dados do fornecedor.</param>
        /// <returns>Fornecedor criado com status 201.</returns>
        [HttpPost]
        public async Task<ActionResult<Supplier>> Create(Supplier supplier)
        {
            await _supplierRepository.Create(supplier);
            return CreatedAtAction(nameof(GetById), new { id = supplier.Id }, supplier);
        }

        /// <summary>
        /// Atualiza os dados de um fornecedor existente.
        /// </summary>
        /// <param name="id">ID do fornecedor a ser atualizado.</param>
        /// <param name="supplier">Novos dados do fornecedor.</param>
        /// <returns>Status da operação.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest();
            }
            await _supplierRepository.Update(supplier);
            return NoContent();
        }

        /// <summary>
        /// Remove um fornecedor pelo ID.
        /// </summary>
        /// <param name="id">ID do fornecedor a ser removido.</param>
        /// <returns>Status da operação.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierRepository.Remove(id);
            return NoContent();
        }

        /// <summary>
        /// Pesquisa fornecedores por nome e/ou email de contato.
        /// </summary>
        /// <param name="name">Nome parcial ou completo do fornecedor.</param>
        /// <param name="contactEmail">Email de contato.</param>
        /// <returns>Lista de fornecedores que correspondem aos critérios.</returns>
        [HttpGet("search", Name = "SearchSuppliers")]
        public async Task<ActionResult<IEnumerable<Supplier>>> Search([FromQuery] string name, [FromQuery] string contactEmail)
        {
            var suppliers = await _supplierRepository.Search(name, contactEmail);
            if (suppliers == null || !suppliers.Any())
            {
                return NotFound("No suppliers found with the given criteria");
            }
            return Ok(suppliers);
        }
    }
}