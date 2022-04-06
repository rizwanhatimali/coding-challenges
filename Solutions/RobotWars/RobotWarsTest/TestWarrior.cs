using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotWars;
using System;
using System.Collections.Generic;
using System.IO;

namespace RobotWarsTest
{
    [TestClass]
    public class TestWarrior
    {
        Warrior warrior;
        public TestWarrior()
        {
            warrior = new Warrior(1);
            warrior.coordinates = new List<string> { "5", "5" };
            warrior.robotMovements = new List<Movement> { new Movement() { InitialPosition = new List<string> { "1", "2", "N" }, Moves = "LMLMLMLMM".ToCharArray() } };
        }



        [TestMethod]
        public void TestValidInputs()
        {
            try
            {
                warrior.ValidateInputs();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void TestInvalidCoordinatesCount()
        {
            warrior.coordinates = new List<string> { "5" };
            InvalidInputException ex = Assert.ThrowsException<InvalidInputException>(() => warrior.ValidateInputs());
            Assert.AreEqual("Invalid coordinates", ex.Message);
        }

        [TestMethod]
        public void TestInvalidCoordinatesValues()
        {
            warrior.coordinates = new List<string> { "5", "-1" };
            InvalidInputException ex = Assert.ThrowsException<InvalidInputException>(() => warrior.ValidateInputs());
            Assert.AreEqual("Invalid Upper right coordinates as lower left coordinates are assumed to be (0, 0).", ex.Message);
        }

        [TestMethod]
        public void TestInvalidMovementsFormat()
        {
            warrior.robotMovements = new List<Movement> { new Movement() { InitialPosition = new List<string> { "1" }, Moves = "LMLMLMLMM".ToCharArray() } };
            InvalidInputException ex = Assert.ThrowsException<InvalidInputException>(() => warrior.ValidateInputs());
            Assert.AreEqual("Robot 1 position should be in format 'xCoordinate yCoordinate currDir'", ex.Message);
        }

        [TestMethod]
        public void TestOutofRangeMovement()
        {
            warrior.robotMovements = new List<Movement> { new Movement() { InitialPosition = new List<string> { "1", "8", "N" }, Moves = "LMLMLMLMM".ToCharArray() } };
            InvalidInputException ex = Assert.ThrowsException<InvalidInputException>(() => warrior.ValidateInputs());
            Assert.AreEqual("Robot 1 coordinates are out of range", ex.Message);
        }

        [TestMethod]
        public void TestInvalidMoves()
        {
            warrior.robotMovements = new List<Movement> { new Movement() { InitialPosition = new List<string> { "1", "2", "N" }, Moves = "XYZ".ToCharArray() } };
            InvalidInputException ex = Assert.ThrowsException<InvalidInputException>(() => warrior.ValidateInputs());
            Assert.AreEqual("Robot 1 performs invalid moves", ex.Message);
        }

        [TestMethod]
        public void TestPrintValidResult()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            warrior.PrintResult();
            Assert.AreEqual("1 3 N\r\n", sw.ToString());
        }

        [TestMethod]
        public void TestPrintOutOfBoundResult()
        {
            warrior.coordinates = new List<string> { "1", "1" };
            var ex = Assert.ThrowsException<InvalidOperationException>(() => warrior.PrintResult());
            Assert.AreEqual("Requested moves will throw Robot 1 out of battlefield", ex.Message);
        }
    }
}