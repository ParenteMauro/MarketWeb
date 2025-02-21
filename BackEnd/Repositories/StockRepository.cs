using BackEnd.Data;
using BackEnd.Dtos.Stock;
using BackEnd.Mappers;
using BackEnd.Model;
using Microsoft.EntityFrameworkCore;
using BackEnd.Repositories.Interfaces;
namespace BackEnd.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly MarketDBContext _context;

        public StockRepository(MarketDBContext context)
        {
            _context = context;
        }
        public async Task<List<StockDto>> GetAll()
        {
            List<Stock> stocks = await _context.stocks.ToListAsync();
            if (stocks == null)
                return null;
            List<StockDto> returnStocks = stocks.Select(s => s.ToStockDto()).ToList();
            return returnStocks;
        }
        public async Task<StockDto> Create(CreateStockDto newStock)
        {
            Stock insertStock = new Stock()
            {
                Symbol = newStock.Symbol,
                CompanyName = newStock.CompanyName,
                Purchase = newStock.Purchase,
                LastDiv = newStock.LastDiv,
                Industry = newStock.Industry,
                MarketCap = newStock.MarketCap
            };

            var stockEntity = await _context.stocks.AddAsync(insertStock);
            await _context.SaveChangesAsync();

            StockDto stockAdded = stockEntity.Entity.ToStockDto();
            return stockAdded;

        }
        public async Task<StockDto?> GetById(int id)
        {
            Stock? stockEntity = await _context.stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockEntity == null)
                return null;

            StockDto stockReturn = stockEntity.ToStockDto();
            return stockReturn;

        }


        public async Task<bool> DeleteStock(int id)
        {
            Stock? stockEntity = await _context.stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockEntity == null)
                return false;
            _context.stocks.Remove(stockEntity);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> RepalceStock(int id, CreateStockDto stockNewInfo)
        {
            Stock? stockEntity = _context.stocks.FirstOrDefault(stock => stock.Id == id);
            if (stockEntity == null)
                return false;
            else
            {
                stockEntity.Symbol = stockNewInfo.Symbol;
                stockEntity.CompanyName = stockNewInfo.CompanyName;
                stockEntity.Purchase = stockNewInfo.Purchase;
                stockEntity.LastDiv = stockNewInfo.LastDiv;
                stockEntity.Industry = stockNewInfo.Industry;
                stockEntity.MarketCap = stockNewInfo.MarketCap;


                _context.stocks.Update(stockEntity);
                await _context.SaveChangesAsync();
                return true;
            }
        }

    }
}
