using API.DTOs;
using API.Models;

namespace API.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToCommenDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                CreatedBy=comment.AppUser.UserName,
                StockId = comment.StockId,
            };
        }

        public static Comment ToCommenFromCretaeDto(this CreateCommentDto comment,int StockId)
        {
            return new Comment
            {
                
                Title = comment.Title,
                Content = comment.Content,
                StockId = StockId
            };
        }
        public static Comment ToCommenFromUpdate(this UpdateCommentDto comment)
        {
            return new Comment
            {

                Title = comment.Title,
                Content = comment.Content
            };
        }
    }
}
