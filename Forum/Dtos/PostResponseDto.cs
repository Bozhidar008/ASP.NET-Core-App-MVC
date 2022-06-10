using System.Collections.Generic;
using WebApplication1.Models;

namespace WebApplication1.Dtos
{
    public class PostResponseDto
    {
		public PostResponseDto(Post postModel)
		{
            this.Title = postModel.Title;
            this.Content = postModel.Content;
            this.Author = postModel.User.UserName;//??
            this.CommentCount = Comments.Count;//??
            this.LikeCount = Likes.Count;//??
		}

        public string Title { get; set; }

        public string Content { get; set; }

        public int CommentCount { get; set; }//??

        public int LikeCount { get; set; }//??

        public string Author { get; set; }
        
        public List<string> Comments { get; set; } = new List<string>();

        public List<string> Likes { get; set; } = new List<string>();
        


      
    }
}
