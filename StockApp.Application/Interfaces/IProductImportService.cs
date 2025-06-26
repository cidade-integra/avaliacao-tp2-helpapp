namespace StockApp.Application.Interfaces
{
    public interface IProductImportService
    {
        Task<int> ImportFromCsvAsync(Stream csvStream);
    }
}
