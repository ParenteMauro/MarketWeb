using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BackEnd.Mappers;
using BackEnd.Dtos.Stock;
using BackEnd.Services;

namespace BackEnd.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly MarketDBContext _context;
        private readonly ICreateStockService _createStockService;
        public StockController(MarketDBContext context, ICreateStockService createStockService)
        {
            _context = context;
            _createStockService = createStockService;
        }
        [HttpGet]
        public async Task<IActionResult> getAllStocks()
        {
            var stocks = await _context.stocks.ToListAsync();
            var stockDtos = stocks.Select(s => s.ToStockDto());
            if (stockDtos == null)
                return NotFound();
            return Ok(stockDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getStockById([FromRoute] int id)
        {
            var stockEntity = await _context.stocks.FirstOrDefaultAsync(stock => stock.Id == id);
            if (stockEntity == null)
                return NotFound();

            StockDto stock = stockEntity.ToStockDto();
            return Ok(stock);
        }
        [HttpPost]
        public async Task<IActionResult> createStock([FromBody] CreateStockDto newStock)
        =>Ok( await _createStockService.CreateStock(newStock));
        

        [HttpDelete("{id}")]
       
        public async Task<IActionResult> deleteStock([FromRoute] int id)
        {
            var stock = await _context.stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stock == null)
                return NotFound();
            _context.Remove(stock);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> updateStock([FromRoute] int id, [FromBody] StockDto stockDto)
        {

            stockDto.Id = id;

            _context.Update(stockDto.ToStockFromStockDto());
            await _context.SaveChangesAsync();
            return Ok(stockDto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> replaceStock([FromRoute] int id, [FromBody] StockDto stockDto)
        {
            stockDto.Id = id;

            _context.Update(stockDto.ToStockFromStockDto());
            await _context.SaveChangesAsync();
            return Ok(stockDto);
        }

    }
}
