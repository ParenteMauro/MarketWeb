using BackEnd.Dtos.Comment;

namespace BackEnd.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<CommentDto>> GetAll();
        Task<CommentDto?> GetById(int id);
        Task<CommentDto> Create(CreateCommentDto newComment);
        Task<bool> Replace(int id, UpdateCommentDto commentDto);
        Task<bool> DeleteById(int id); 
    }
}
