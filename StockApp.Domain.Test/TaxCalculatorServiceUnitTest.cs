using StockApp.Application.Services;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Test
{
    public class TaxCalculatorServiceUnitTest
    {

        #region Atributos

        private readonly TaxCalculatorService _calculator;

        #endregion

        #region Construtor

        public TaxCalculatorServiceUnitTest()
        {
            _calculator = new TaxCalculatorService();
        }

        #endregion

        #region Testes Positivos

        [Fact]
        public void ShouldReturnZero_WhenNoTaxes()
        {
            var baseAmount = 100m;
            var taxes = new List<Tax>();

            var result = _calculator.Calculate(baseAmount, taxes);

            Assert.Equal(0, result);
        }

        [Fact]
        public void ShouldCalculateSingleTaxCorrectly()
        {
            var baseAmount = 200m;
            var taxes = new List<Tax>
            {
                new Tax { Name = "VAT", Rate = 0.10m }
            };

            var result = _calculator.Calculate(baseAmount, taxes);

            Assert.Equal(20m, result);
        }

        [Fact]
        public void ShouldSumMultipleTaxesCorrectly()
        {
            var baseAmount = 100m;
            var taxes = new List<Tax>
            {
                new Tax { Name = "VAT", Rate = 0.10m },
                new Tax { Name = "Service", Rate = 0.05m }
            };

            var result = _calculator.Calculate(baseAmount, taxes);

            Assert.Equal(15m, result); // 10 + 5
        }

        [Fact]
        public void ShouldReturnZero_WhenBaseAmountIsZero()
        {
            var baseAmount = 0m;
            var taxes = new List<Tax>
            {
                new Tax { Name = "ISS", Rate = 0.05m }
            };

            var result = _calculator.Calculate(baseAmount, taxes);

            Assert.Equal(0, result);
        }

        #endregion

        #region Testes Negativos

        [Fact]
        public void ShouldThrowException_WhenTaxesIsNull()
        {
            decimal baseAmount = 100m;
            List<Tax> taxes = null;

            Assert.Throws<ArgumentNullException>(() =>
                _calculator.Calculate(baseAmount, taxes));
        }

        [Fact]
        public void ShouldThrowException_WhenTaxHasNegativeRate()
        {
            decimal baseAmount = 100m;
            var taxes = new List<Tax>
            {
                new Tax { Name = "InvalidTax", Rate = -0.05m }
            };

            Assert.Throws<ArgumentException>(() =>
                _calculator.Calculate(baseAmount, taxes));
        }

        [Fact]
        public void ShouldThrowException_WhenTaxNameIsEmpty()
        {
            decimal baseAmount = 100m;
            var taxes = new List<Tax>
            {
                new Tax { Name = "", Rate = 0.1m }
            };

            Assert.Throws<ArgumentException>(() =>
                _calculator.Calculate(baseAmount, taxes));
        }

        #endregion

    }
}
