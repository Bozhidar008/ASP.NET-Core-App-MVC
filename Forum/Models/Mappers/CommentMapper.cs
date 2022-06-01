using WebApplication1.Dtos;


namespace WebApplication1.Models.Mappers
{
    public class CommentMapper
    {
        public Comment ConvertToModel(CommentRequestDto dto) 
        {
            Comment comment = new Comment
            {
                Content = dto.Content,
                PostId = dto.PostId,
                UserId = dto.UserId,
            };

            return comment;
        }
    }
}
