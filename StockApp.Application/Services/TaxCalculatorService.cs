using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;

namespace StockApp.Application.Services
{
    public class TaxCalculatorService : ITaxCalculatorService
    {
        public decimal Calculate(decimal baseAmount, IEnumerable<Tax> taxes)
        {
            if (taxes == null)
                throw new ArgumentNullException(nameof(taxes), "Tax list cannot be null.");

            decimal total = 0;

            foreach (var tax in taxes)
            {
                if (string.IsNullOrWhiteSpace(tax.Name))
                    throw new ArgumentException("Tax name cannot be null or empty.");

                if (tax.Rate < 0)
                    throw new ArgumentException("Tax rate cannot be negative.");

                total += baseAmount * tax.Rate;
            }

            return total;
        }
    }
}
