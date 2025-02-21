using BackEnd.Dtos.Stock;

namespace BackEnd.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<StockDto> Create(CreateStockDto newStock);
        Task<StockDto?> GetById(int id);
        Task<List<StockDto>> GetAll();
        Task<bool> DeleteStock(int id);
        Task<bool> RepalceStock(int id, CreateStockDto stockNewInfo);
    }
}
