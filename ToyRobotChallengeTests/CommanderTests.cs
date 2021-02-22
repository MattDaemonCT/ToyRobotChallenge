using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ToyRobotChallenge.Source.Objects.Commander;

namespace UnitTests
{
    [TestClass]
    public class CommanderTests
    {     
        [TestMethod]
        public void InvalidInputReturnsNullCommand()
        {
            var msg = "Invalid command";
            var commander = new Commander();

            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput(""), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput(" "), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput("BLAH"), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput("MOVE 123"), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput("LEFT 123"), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput("RIGHT 123"), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput("PLACE"), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput("PLACE 1,2"), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput("PLACE 1,NORTH"), msg);
            Assert.ThrowsException<Exception>(() => commander.ParseCommandInput("PLACE A,B,NORTH"), msg);            
        }

        [TestMethod]
        public void ValidMoveInputReturnsCommand()
        {
            var commander = new Commander();

            var command = commander.ParseCommandInput("MOVE");
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CommandType == CommandType.MOVE);

            command = commander.ParseCommandInput("move");
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CommandType == CommandType.MOVE);
        }

        [TestMethod]
        public void ValidLeftInputReturnsCommand()
        {
            var commander = new Commander();

            var command = commander.ParseCommandInput("LEFT");
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CommandType == CommandType.LEFT);

            command = commander.ParseCommandInput("left");
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CommandType == CommandType.LEFT);
        }

        [TestMethod]
        public void ValidRightInputReturnsCommand()
        {
            var commander = new Commander();

            var command = commander.ParseCommandInput("Right");
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CommandType == CommandType.RIGHT);

            command = commander.ParseCommandInput("right");
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CommandType == CommandType.RIGHT);
        }

        [TestMethod]
        public void ValidReportInputReturnsCommand()
        {
            var commander = new Commander();

            var command = commander.ParseCommandInput("REPORT");
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CommandType == CommandType.REPORT);

            command = commander.ParseCommandInput("Report");
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CommandType == CommandType.REPORT);
        }

        [TestMethod]
        public void RobotRequiresPlace()
        {
            var commander = new Commander();

            Assert.ThrowsException<Exception>(() => commander.ProcessCommand("MOVE"), "Robot has not been placed");
            Assert.ThrowsException<Exception>(() => commander.ProcessCommand("LEFT"), "Robot has not been placed");
            Assert.ThrowsException<Exception>(() => commander.ProcessCommand("RIGHT"), "Robot has not been placed");
            Assert.ThrowsException<Exception>(() => commander.ProcessCommand("REPORT"), "Robot has not been placed");
        }

        [TestMethod]
        public void RobotReportsAfterPlace()
        {
            var commander = new Commander();            
            commander.ProcessCommand("PLACE 0,0,NORTH");
            Assert.AreEqual("0,0,NORTH", commander.Robot.GetReportText());
        }

        [TestMethod]
        public void OnlyPlacesWithinBounds()
        {
            var msg = "Invalid command";
            var commander = new Commander();
           
            Assert.ThrowsException<Exception>(() => commander.ProcessCommand("PLACE 5,5,NORTH"), msg);
            Assert.ThrowsException<Exception>(() => commander.ProcessCommand("PLACE -1,4,NORTH"), msg);
            Assert.ThrowsException<Exception>(() => commander.ProcessCommand("PLACE 4,-1,NORTH"), msg);
            
            commander.ProcessCommand("PLACE 4,4,NORTH");
            Assert.IsTrue(commander.Robot.HasBeenPlaced);
          
            commander.ProcessCommand("PLACE 0,0,NORTH");
            Assert.IsTrue(commander.Robot.HasBeenPlaced);
        }
    }
}
