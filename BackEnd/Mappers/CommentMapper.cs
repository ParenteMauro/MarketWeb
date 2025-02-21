using BackEnd.Model;
using BackEnd.Dtos.Comment;
namespace BackEnd.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            CommentDto dto = new CommentDto()
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
            return dto;
        }

        public static Comment ToCommentFromCreateCommentDto(this CreateCommentDto commentModel)
        {
            Comment commentEntity = new Comment()
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
            return commentEntity;
        }
    }
}
