using BackEnd.Dtos.Stock;
using BackEnd.Helpers;

namespace BackEnd.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<StockDto> Create(CreateStockDto newStock);
        Task<StockDto?> GetById(int id);
        Task<List<StockDto>> GetAll(QueryObject query);
        Task<bool> DeleteStock(int id);
        Task<bool> RepalceStock(int id, CreateStockDto stockNewInfo);
        Task<StockDto?> GetByIdWithComments(int id);
        Task<List<StockDto>> GetAllWithComments();
        Task<bool> StockExists(int id);
    }
}
