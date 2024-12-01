using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC2024
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!int.TryParse(args[0], out var day) || day < 0 || day > 25)
            {
                Console.Error.Write($"'{args[0]}' is not a valid day. Enter a number 1-25");
                return;
            }
            
            var solutionType = Type.GetType($"AOC2024.Solutions.Solution{day}");
            if(solutionType == null)
            {
                Console.Error.Write($"Solution for day {day} is unavailable. Come back later!");
                return;
            }

            var solution = (Solution)Activator.CreateInstance(solutionType);
            solution.Run();
        }
    }
}
