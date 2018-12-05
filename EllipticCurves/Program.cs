using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EllipticCurves.Model;
using EllipticCurves.Services;

namespace EllipticCurves
{
    class Program
    {
        static void Main(string[] args)
        {
            var solutions = TaskService.Solve("input.txt");
            foreach (var solution in solutions)
            {
                Console.WriteLine(solution);
            }
        }
    }
}
