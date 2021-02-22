using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotChallenge.Source.Objects.Environment;
using ToyRobotChallenge.Source.Objects.Robot;

namespace ToyRobotChallenge.Source.Objects.Commander
{
    /// <summary>
    /// Class responsible for parsing user input and executing appropriate commands    
    /// </summary>
    public class Commander
    {                
        public IRobot Robot { get; set; }

        public Commander ()
        {                     
            var tableTop = new Tabletop();
            Robot = new ToyRobot(tableTop);
        }

        /// <summary>
        /// Parses console input into a RobotCommand
        /// Returns null if command is invalid
        /// </summary>
        public IRobotCommand ParseCommandInput(string input)
        {
            var exceptionText = "Invalid command";
            if (string.IsNullOrWhiteSpace(input)) throw new Exception(exceptionText);

            input = input.ToUpperInvariant();

            var splitInput = input.Split(' ');

            if (!splitInput.Any()) throw new Exception(exceptionText);

            if (!Enum.TryParse(splitInput.First(), out CommandType commandType)) throw new Exception(exceptionText);

            if (commandType == CommandType.PLACE)
            {
                if (splitInput.Length != 2) throw new Exception(exceptionText);

                var placeArguments = splitInput[1].Split(',');
                if (placeArguments.Length != 3) throw new Exception(exceptionText);

                var hasXValue = int.TryParse(placeArguments[0], out int x);
                var hasYValue = int.TryParse(placeArguments[1], out int y);
                var hasFacing = Enum.TryParse(placeArguments[2], out Facing facing);
                if (!(hasXValue && hasYValue && hasFacing && x >= 0 && y >= 0 && x < Robot.Tabletop.Width && y < Robot.Tabletop.Height)) throw new Exception(exceptionText);

                return new PlaceRobotCommand(x, y, facing);
            }
            else if (splitInput.Length > 1) throw new Exception(exceptionText);

            return new RobotCommand { CommandType = commandType };
        }
        
        /// <summary>
        /// Processes a parsed command to instruct the robot        
        /// </summary>
        public void ProcessCommand(string input)
        {           
            var command = ParseCommandInput(input);

            if (command == null) return;

            if (command.CommandType != CommandType.PLACE && !Robot.HasBeenPlaced) throw new Exception("Robot has not been placed");

            switch (command.CommandType)
            {
                case CommandType.PLACE:
                    var placeCommand = (PlaceRobotCommand)command;
                    Robot.Place(placeCommand.X, placeCommand.Y, placeCommand.Facing);
                    break;
                case CommandType.MOVE:
                    Robot.Move();
                    break;
                case CommandType.LEFT:
                    Robot.Rotate(Rotation.Left);
                    break;
                case CommandType.RIGHT:
                    Robot.Rotate(Rotation.Right);
                    break;
                case CommandType.REPORT:                   
                    Console.WriteLine(Robot.GetReportText());
                    break;
            }
        }
    }
}
