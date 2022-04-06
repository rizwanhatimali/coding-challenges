using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotWars
{
    public class Warrior
    {
        private readonly int _numRobots;
        private char[] _allowedMoves = { 'L', 'R', 'M' };
        public List<string> coordinates { get; set; }
        public List<Movement> robotMovements { get; set; } = default(List<Movement>);


        public Warrior(int numRobots)
        {
            _numRobots = numRobots;
        }

        public void StartWar()
        {
            try
            {
                GetInputs();
                ValidateInputs();
                PrintResult();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void PrintResult()
        {
            int robot = 1;
            robotMovements.ForEach(obj =>
            {
                var currentX = int.Parse(obj.InitialPosition[0]);
                var currentY = int.Parse(obj.InitialPosition[1]);
                var currentDirection = obj.InitialPosition[2];

                foreach (char move in obj.Moves)
                {
                    switch (move)
                    {
                        case 'L':
                            switch(currentDirection)
                            {
                                case "N":
                                    currentDirection = "W";
                                    break;
                                case "W":
                                    currentDirection = "S";
                                    break;
                                case "S":
                                    currentDirection = "E";
                                    break;
                                case "E":
                                    currentDirection = "N";
                                    break;
                            }
                            break;
                        case 'R':
                            switch (currentDirection)
                            {
                                case "N":
                                    currentDirection = "E";
                                    break;
                                case "E":
                                    currentDirection = "S";
                                    break;
                                case "S":
                                    currentDirection = "W";
                                    break;
                                case "W":
                                    currentDirection = "N";
                                    break;
                            }
                            break;
                        case 'M':
                            switch (currentDirection)
                            {
                                case "N":
                                    if(currentY == int.Parse(coordinates[1]))
                                    {
                                        throw new InvalidOperationException($"Requested moves will throw Robot {robot} out of battlefield");
                                    }
                                    currentY++;
                                    break;
                                case "S":
                                    if (currentY == 0)
                                    {
                                        throw new InvalidOperationException($"Requested moves will throw Robot {robot} out of battlefield");
                                    }
                                    currentY--;
                                    break;
                                case "E":
                                    if (currentX == int.Parse(coordinates[0]))
                                    {
                                        throw new InvalidOperationException($"Requested moves will throw Robot {robot} out of battlefield");
                                    }
                                    currentX++;
                                    break;
                                case "W":
                                    if (currentX == 0)
                                    {
                                        throw new InvalidOperationException($"Requested moves will throw Robot {robot} out of battlefield");
                                    }
                                    currentX--;
                                    break;
                            }
                            break;
                    }
                }
                Console.WriteLine($"{currentX} {currentY} {currentDirection}");
                robot++;
            });
        }

        public void ValidateInputs()
        {
            if (coordinates.Count != 2)
                throw new InvalidInputException("Invalid coordinates");
            if (int.Parse(coordinates[0]) < 0 || int.Parse(coordinates[1]) < 0)
                throw new InvalidInputException("Invalid Upper right coordinates as lower left coordinates are assumed to be (0, 0).");

            int robot = 1;
            robotMovements.ForEach(obj =>
            {
                if (obj.InitialPosition.Count != 3)
                    throw new InvalidInputException($"Robot {robot} position should be in format 'xCoordinate yCoordinate currDir'");
                if (int.Parse(obj.InitialPosition[0]) < 0 || int.Parse(obj.InitialPosition[1]) < 0 || int.Parse(obj.InitialPosition[0]) > int.Parse(coordinates[0]) || int.Parse(obj.InitialPosition[1]) > int.Parse(coordinates[1]))
                    throw new InvalidInputException($"Robot {robot} coordinates are out of range");
                
                if (obj.Moves.Any(c => !_allowedMoves.Contains(c)))
                {
                    throw new InvalidInputException($"Robot {robot} performs invalid moves");
                }
                robot++;
            });
        }

        private void GetInputs()
        {
            coordinates = Console.ReadLine().Split(new char[] { ' ' }).ToList();
            robotMovements = new List<Movement>();
            for (int i = 0; i < _numRobots; i++)
            {
                var movement = new Movement();
                movement.InitialPosition = Console.ReadLine().Split(new char[] { ' ' }).ToList();
                movement.Moves = Console.ReadLine().ToCharArray();
                robotMovements.Add(movement);
            }
        }
    }
}
