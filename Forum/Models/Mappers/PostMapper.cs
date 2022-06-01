using WebApplication1.Dtos;

namespace WebApplication1.Models.Mappers
{
    public class PostMapper
    {

		public Post ConvertToModel(PostRequestDto dto)
		{
			Post modelPost = new Post();
			modelPost.Title = dto.Title;
			modelPost.Content = dto.Content;
						
			return modelPost;
		}
	}
}
