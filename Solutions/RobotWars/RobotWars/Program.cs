using System;
using System.Linq;

namespace RobotWars
{
    public class Program
    {
		private const int _numRobots = 2;
		public static void Main()
		{
			var warrior = new Warrior(_numRobots);
			warrior.StartWar();
		}
	}
}
