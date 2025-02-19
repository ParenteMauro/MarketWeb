using BackEnd.Model;
using BackEnd.Dtos.Stock;
using BackEnd.Data;
using BackEnd.Mappers;
namespace BackEnd.Services
{
    public interface ICreateStockService
    {
        Task<StockDto> CreateStock(CreateStockDto newStock);
    }
    public class CreateStockService : ICreateStockService
    {
        public readonly MarketDBContext _context;
        public CreateStockService( MarketDBContext context)
        {
            _context = context;
        }
        public async Task<StockDto> CreateStock(CreateStockDto newStock)
        {
            try
            {
                Stock newStockEntity = newStock.ToStockFromCreateDto();
                await _context.AddAsync(newStockEntity);
                await _context.SaveChangesAsync();
                StockDto newStockCreated = newStockEntity.ToStockDto();
                return newStockCreated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
                
        }

        
    }
}
