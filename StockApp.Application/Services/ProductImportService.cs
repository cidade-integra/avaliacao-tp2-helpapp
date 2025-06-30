using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Domain.Validation;
using System.Text;

namespace StockApp.Application.Services
{
    public class ProductImportService : IProductImportService
    {
        #region Atributos

        private readonly IProductRepository _productRepository;

        #endregion

        #region Construtor

        public ProductImportService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        #region Métodos

        public async Task<int> ImportFromCsvAsync(Stream csvStream)
        {
            var reader = new StreamReader(csvStream, Encoding.UTF8);
            var importedProducts = new List<Product>();
            int lineNumber = 0;
            int skipped = 0;

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line)) continue;

                var values = line.Split(',');

                if (values.Length < 5)
                {
                    Console.WriteLine($"[Linha {lineNumber}] Ignorada: número de colunas inválido.");
                    skipped++;
                    continue;
                }

                if (!decimal.TryParse(values[2], out decimal price) ||
                    !int.TryParse(values[3], out int stock))
                {
                    Console.WriteLine($"[Linha {lineNumber}] Ignorada: erro de conversão numérica. Preço: '{values[2]}', Estoque: '{values[3]}'");
                    skipped++;
                    continue;
                }

                try
                {
                    var categoryId = 4;
                    var product = new Product(values[0], values[1], price, stock, values[4])
                    {
                        CategoryId = categoryId
                    };

                    importedProducts.Add(product);
                }
                catch (DomainExceptionValidation ex)
                {
                    Console.WriteLine($"[Linha {lineNumber}] Ignorada: {ex.Message}");
                    skipped++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Linha {lineNumber}] Erro inesperado: {ex.Message}");
                    skipped++;
                }
            }

            foreach (var product in importedProducts)
            {
                await _productRepository.Create(product);
            }

            Console.WriteLine($"Importação concluída: {importedProducts.Count} produtos importados, {skipped} ignorados.");
            return importedProducts.Count;
        }

        #endregion
    }
}
