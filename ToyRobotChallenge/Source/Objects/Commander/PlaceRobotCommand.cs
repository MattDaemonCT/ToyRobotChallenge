using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotChallenge.Source.Objects.Robot;

namespace ToyRobotChallenge.Source.Objects.Commander
{
    public class PlaceRobotCommand : IRobotCommand
    {
        public CommandType CommandType { get; private set; }

        public int X { get; set; }

        public int Y { get; set; }

        public Facing Facing { get; set; }

        public PlaceRobotCommand(int x, int y, Facing facing)
        {
            CommandType = CommandType.PLACE;
            X = x;
            Y = y;
            Facing = facing;
        }
    }
}
