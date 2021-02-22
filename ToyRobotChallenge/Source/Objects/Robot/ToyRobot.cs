using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotChallenge.Source.Objects.Environment;

namespace ToyRobotChallenge.Source.Objects.Robot
{
    /// <summary>
    /// Implementation of a Robot with control methods
    /// </summary>
    public class ToyRobot : IRobot
    {
        public Tabletop Tabletop { get; private set; }
        public Facing CurrentFacing { get; private set; }
        public Point CurrentPosition { get; private set; }

        public bool HasBeenPlaced { get; set; }

        public void Place(int x, int y, Facing facing)
        {
            if (x >= Tabletop.Width || y >= Tabletop.Height || x < 0 || y < 0) return;
            
            CurrentFacing = facing;
            CurrentPosition = new Point(x, y);
            HasBeenPlaced = true;
        }

        public void Move()
        {
            var newX = CurrentPosition.X;
            var newY = CurrentPosition.Y;

            switch (CurrentFacing)
            {
                case Facing.NORTH:
                    newY++;
                    break;
                case Facing.SOUTH:
                    newY--;
                    break;
                case Facing.EAST:
                    newX++;
                    break;
                case Facing.WEST:
                    newX--;
                    break;
            }

            if (newX >= Tabletop.Width || newX < 0 || newY >= Tabletop.Height || newY < 0) return;

            CurrentPosition = new Point(newX, newY);
        }

        public void Rotate(Rotation rotation)
        {
            var newFacing = 0;
            switch (rotation)
            {
                case Rotation.Left:
                    newFacing = (int)CurrentFacing - 1;
                    break;
                case Rotation.Right:
                    newFacing = (int)CurrentFacing + 1;
                    break;
            }

            if (newFacing > 3) newFacing = 0;
            if (newFacing < 0) newFacing = 3;

            CurrentFacing = (Facing)newFacing;
        }

        public ToyRobot(Tabletop tabletop)
        {
            Tabletop = tabletop;
            CurrentFacing = Facing.NORTH;
            CurrentPosition = new Point(0, 0);
        }

        public ToyRobot(Tabletop tabletop, Facing startingFacing, Point startingPosition)
        {
            Tabletop = tabletop;
            CurrentFacing = startingFacing;
            CurrentPosition = startingPosition;
        }

        public string GetReportText()
        {
            return $"{CurrentPosition.X},{CurrentPosition.Y},{ Enum.GetName(typeof(Facing), CurrentFacing)}";
        }
    }
}
