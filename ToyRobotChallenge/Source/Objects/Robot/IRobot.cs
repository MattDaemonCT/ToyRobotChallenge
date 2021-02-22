using System.Drawing;
using ToyRobotChallenge.Source.Objects.Environment;

namespace ToyRobotChallenge.Source.Objects.Robot
{
    /// <summary>
    /// Base class for a robot
    /// </summary>
    public interface IRobot
    {
        Tabletop Tabletop { get; }

        Facing CurrentFacing { get; }

        Point CurrentPosition { get; }

        bool HasBeenPlaced { get; set; }
        
        void Place(int x, int y, Facing facing);

        void Move();         

        void Rotate(Rotation rotation);

        string GetReportText();
    }
}
