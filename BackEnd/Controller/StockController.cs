using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BackEnd.Mappers;
using BackEnd.Dtos.Stock;
using BackEnd.Repositories.Interfaces;

namespace BackEnd.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stockList = await _stockRepo.GetAll();
            if (stockList == null)
                return NotFound();

            return Ok(stockList);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            StockDto? stock = await _stockRepo.GetById(id);
            if (stock == null)
                return NotFound();
            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockDto newStock)
        => Ok(await _stockRepo.Create(newStock));


        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _stockRepo.DeleteStock(id);
            if (!response)
                return NotFound();
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Replace([FromRoute] int id, [FromBody] CreateStockDto stockInfo)
        {
            bool response = await _stockRepo.RepalceStock(id, stockInfo);
            if (!response)
                return BadRequest();
            return Ok(stockInfo);
        }

        [HttpGet("stockWithComments/{id}")]

        public async Task<IActionResult> GetStockWithComments(int id)
        {
            StockDto? stock = await _stockRepo.GetByIdWithComments(id);
            if (stock == null) 
                return NotFound();
            return Ok(stock);
        }

        [HttpGet("getAllWithComments")]
        public async Task<IActionResult> GetAllWithComments()
        {
            List<StockDto> stockList = await _stockRepo.GetAllWithComments();
            return Ok(stockList);
        }
    }
}
