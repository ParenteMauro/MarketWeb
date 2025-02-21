using BackEnd.Data;
using BackEnd.Model;
using BackEnd.Mappers;
using BackEnd.Dtos.Comment;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MarketDBContext _context;
        public CommentRepository(MarketDBContext context)
        {
            _context = context;
        }

        public async Task<CommentDto> Create(CreateCommentDto newComment)
        {
            Comment commentEntity = newComment.ToCommentFromCreateCommentDto();
            EntityEntry<Comment> response = await _context.AddAsync(commentEntity);
            if (response == null)
                return null;
            await _context.SaveChangesAsync();
            commentEntity.Id = response.Entity.Id;
            return commentEntity.ToCommentDto();
        }

        public async Task<bool> DeleteById(int id)
        {
            Comment? checkComment = await _context.comments.FirstOrDefaultAsync(comment => comment.Id == id);
            if (checkComment == null)
                return false;
            _context.comments.Remove(checkComment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CommentDto>> GetAll()
        {
            List<Comment>? commentList = await _context.comments.ToListAsync();
            if (commentList == null)
                return null;
            List<CommentDto> commentListDto = commentList.Select(comment=> comment.ToCommentDto()).ToList();    
            return commentListDto;
        }

        public async Task<CommentDto?> GetById(int id)
        {
            Comment? commentEntity = await _context.comments.FirstOrDefaultAsync(comment => comment.Id == id);
            if (commentEntity == null)
                return null;
            return commentEntity.ToCommentDto();
        }

        public async Task<bool> Replace(CommentDto commentDto)
        {
            Comment? commentEntity = await _context.comments.FirstOrDefaultAsync(comment => comment.Id == commentDto.Id);
            if (commentEntity == null)
                return false;
            commentEntity.Title = commentDto.Title;
            commentEntity.Content = commentDto.Content;
            commentEntity.CreatedOn = commentDto.CreatedOn;
            commentEntity.StockId = commentDto.StockId;

            _context.Update(commentEntity);
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}
