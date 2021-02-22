using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotChallenge.Source.Objects.Robot;
using ToyRobotChallenge.Source.Objects.Environment;

namespace UnitTests
{
    [TestClass]
    public class RobotTests
    {
        private ToyRobot GetRobot()
        {
            var tableTop = new Tabletop();
            return new ToyRobot(tableTop);
        }

        [TestMethod]
        public void OnlyPlacesWithinBounds()
        {
            var robot = GetRobot();
            robot.Place(5, 5, Facing.NORTH);
            Assert.IsFalse(robot.HasBeenPlaced);

            robot.Place(-1, -1, Facing.NORTH);
            Assert.IsFalse(robot.HasBeenPlaced);

            robot.Place(-1, 4, Facing.NORTH);
            Assert.IsFalse(robot.HasBeenPlaced);

            robot.Place(4, -1, Facing.NORTH);
            Assert.IsFalse(robot.HasBeenPlaced);

            robot.Place(0, 0, Facing.NORTH);
            Assert.IsTrue(robot.HasBeenPlaced);

            robot.Place(4, 4, Facing.NORTH);
            Assert.IsTrue(robot.HasBeenPlaced);
        }

        [TestMethod]
        public void OnlyMovesToValidPosition()
        {
            var robot = GetRobot();
            robot.Place(4, 4, Facing.NORTH);
            robot.Move();
            Assert.AreEqual("4,4,NORTH", robot.GetReportText());
            
            robot.Place(4, 4, Facing.EAST);
            robot.Move();
            Assert.AreEqual("4,4,EAST", robot.GetReportText());

            robot.Place(0, 0, Facing.WEST);
            robot.Move();
            Assert.AreEqual("0,0,WEST", robot.GetReportText());

            robot.Place(0, 0, Facing.SOUTH);
            robot.Move();
            Assert.AreEqual("0,0,SOUTH", robot.GetReportText());
        }

        [TestMethod]
        public void RotatesLeftCorrectly()
        {
            var robot = GetRobot();
            robot.Place(0, 0, Facing.NORTH);
            robot.Rotate(Rotation.Left);
            Assert.AreEqual("0,0,WEST", robot.GetReportText());
            robot.Rotate(Rotation.Left);
            Assert.AreEqual("0,0,SOUTH", robot.GetReportText());
            robot.Rotate(Rotation.Left);
            Assert.AreEqual("0,0,EAST", robot.GetReportText());
            robot.Rotate(Rotation.Left);
            Assert.AreEqual("0,0,NORTH", robot.GetReportText());
        }

        [TestMethod]
        public void RotatesRightCorrectly()
        {
            var robot = GetRobot();
            robot.Place(0, 0, Facing.NORTH);
            robot.Rotate(Rotation.Right);
            Assert.AreEqual("0,0,EAST", robot.GetReportText());
            robot.Rotate(Rotation.Right);
            Assert.AreEqual("0,0,SOUTH", robot.GetReportText());
            robot.Rotate(Rotation.Right);
            Assert.AreEqual("0,0,WEST", robot.GetReportText());
            robot.Rotate(Rotation.Right);
            Assert.AreEqual("0,0,NORTH", robot.GetReportText());
        }
    }
}
