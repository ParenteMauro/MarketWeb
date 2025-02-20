using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BackEnd.Mappers;
using BackEnd.Dtos.Stock;
using BackEnd.Repositories;

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
        public async Task<IActionResult> getAllStocks()
        {
            var stockList = await _stockRepo.GetAll();
            if(stockList == null) 
                return NotFound();

            return Ok(stockList);
        }
            
        
        [HttpGet("{id}")]
        public async Task<IActionResult> getStockById([FromRoute] int id)
        {
            StockDto? stock = await _stockRepo.GetById(id);
            if(stock == null)
                return NotFound();
            return Ok(stock);
        }

        [HttpPost]
        public async Task<IActionResult> createStock([FromBody] CreateStockDto newStock)
        => Ok(await _stockRepo.Create(newStock)); 


        [HttpDelete("{id}")]

        public async Task<IActionResult> deleteStock([FromRoute] int id)
        {
          var response = await _stockRepo.DeleteStock(id);
            if (!response)
                return NotFound();
            return Ok(response);
        }

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> updateStock([FromRoute] int id, [FromBody] StockDto stockDto)
        //{
        //    var response = await _updateStockService.Update(id, stockDto);
        //    if (response)
        //        return Ok(stockDto);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> replaceStock([FromRoute] int id, [FromBody] CreateStockDto stockInfo)
        {
           bool response = await _stockRepo.RepalceStock(id, stockInfo);   
            if(!response)
                return BadRequest();
            return Ok(stockInfo);    
        }

    }
}
