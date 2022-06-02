using System;

namespace WebApplication1.Exceptions
{
	public class UnAuthorizedOperationException : ApplicationException
	{
		public UnAuthorizedOperationException(string message)
			: base(message)
		{
		}
	}
}
