using System;
using System.Collections.Generic;
using System.Text;

namespace RobotWars
{
	public class InvalidInputException : System.Exception
	{
		public InvalidInputException(string message)
			: base(message)
		{
		}
	}
}
