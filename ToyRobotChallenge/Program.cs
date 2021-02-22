using System;
using ToyRobotChallenge.Source.Objects.Commander;

namespace ToyRobotChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing Command Protocol");
            Console.WriteLine("Available Commands: 'PLACE x,y,facing', 'MOVE', 'LEFT', 'RIGHT', 'REPORT'");
            Console.WriteLine("Type 'exit' to close");
            var commander = new Commander();

            while (true)
            {
                var input = Console.ReadLine();
                if (input.Equals("exit"))
                {
                    break;
                }

                try
                {
                    commander.ProcessCommand(input);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
