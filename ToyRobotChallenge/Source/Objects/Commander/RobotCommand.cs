using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotChallenge.Source.Objects.Commander
{
    public class RobotCommand : IRobotCommand
    {
        public CommandType CommandType { get; set; }
    }
}
