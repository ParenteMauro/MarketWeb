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
        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
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

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto commentInfo)
        {
            CommentDto? commentCreated = await _commentRepo.Create(commentInfo);
            if (commentCreated == null)
                return NotFound();
            return Ok(commentCreated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool response = await _commentRepo.DeleteById(id);
            if (!response)
                return NotFound();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Replace([FromBody] CommentDto comment)
        {
            bool response = await _commentRepo.Replace(comment);
            if(!response)
                return NotFound();
            return Ok(comment);
        }
    }
}
