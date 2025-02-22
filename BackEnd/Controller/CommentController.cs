using BackEnd.Dtos.Comment;
using BackEnd.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepository)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CommentDto? comment = await _commentRepo.GetById(id);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CommentDto>? commentsList = await _commentRepo.GetAll();
            if (commentsList == null)
                return NotFound();
            return Ok(commentsList);
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDto commentInfo)
        {
            if (await _stockRepo.StockExists(stockId))
            {
                CommentDto? commentCreated = await _commentRepo.Create(commentInfo);
                if (commentCreated == null)
                    return NotFound();
                return CreatedAtAction(nameof(GetById), new { id = commentCreated }, commentCreated);
            }
            return BadRequest("Stock wasn't found");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool response = await _commentRepo.DeleteById(id);
            if (!response)
                return NotFound();
            return Ok();
        }
        [HttpPut("{commentId}")]
        public async Task<IActionResult> Replace([FromRoute] int commentId, [FromBody] UpdateCommentDto commentNewInfo)
        {
            bool response = await _commentRepo.Replace(commentId, commentNewInfo);
            if(!response)
                return NotFound();
            return Ok();
        }
    }
}
