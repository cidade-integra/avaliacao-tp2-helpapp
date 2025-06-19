using StockApp.Domain.Entities;

namespace StockApp.Application.Interfaces
{
    public interface ITaxCalculatorService
    {
        decimal Calculate(decimal baseAmount, IEnumerable<Tax> taxes);
    }
}
