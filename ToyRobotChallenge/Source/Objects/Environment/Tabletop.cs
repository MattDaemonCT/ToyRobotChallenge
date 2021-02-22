using System.Linq;

namespace ToyRobotChallenge.Source.Objects.Environment
{
    /// <summary>
    /// Represents the tabletop environment
    /// Currently only supports default size
    /// </summary>
    public class Tabletop
    {
        public int Width { get; }
        public int Height { get; }
       
        public Tabletop()
        {        
            Width = 5;
            Height = 5;         
        }

        public Tabletop (int width, int height)
        {
            Width = width;
            Height = height;
        }
      
        public bool IsValidMove(int x, int y)
        {
            return x < Width && y < Height;
        }
    }
}
