using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace sExpressionCalc
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(SExpression.calc(string.Join(" ", args)));          
        }

    }
}
