using WebApplication1.Models;

namespace WebApplication1.Dtos
{
    public class CommentResponseDto
    {
        //need PostId???
        //need to use user as parameter??
        public CommentResponseDto(Comment commentModel)
        {           
            this.Content = commentModel.Content;
            this.Author = commentModel.User.UserName;           
        }

        public string Content { get; set; }

        public string Author { get; set; }
    }
}
