using WebApplication1.Dtos;

namespace WebApplication1.Models.Mappers
{
	public class UserMapper
	{ 
		public User ConvertToModel(UserRequestDto dto)
		{
			User modelUser = new User();
			modelUser.FirstName = dto.FirstName;
			modelUser.LastName= dto.LastName;
			modelUser.UserName = dto.UserName;
			modelUser.Email = dto.Email;
			return modelUser;
		}

	
	}
}
