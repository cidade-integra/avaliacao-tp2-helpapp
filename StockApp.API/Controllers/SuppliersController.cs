using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.Common.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;

        public SuppliersController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetAll()
        {
            var suppliers = await _supplierRepository.GetSuppliers();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetById(int id)
        {
            var supplier = await _supplierRepository.GetById(id);
            if(supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }
        [HttpPost]
        public async Task<ActionResult<Supplier>> Create(Supplier supplier)
        {
            await _supplierRepository.Create(supplier);
            return CreatedAtAction(nameof(GetById), new {id = supplier.Id}, supplier);
        }
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierRepository.Remove(id);
            return NoContent();
        }

        [HttpGet("search", Name = "SearchSuppliers")]
        public async Task<ActionResult<IEnumerable<Supplier>>> Search([FromQuery] string name, [FromQuery] string contactEmail)
        {
            var suppliers = await _supplierRepository.Search(name, contactEmail);
            if(suppliers == null || !suppliers.Any())
            {
                return NotFound("No suppliers found with the given criteria");
            }
            return Ok(suppliers);
        }
    }
}
