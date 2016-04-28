using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> str = new List<string>() {"asd","asd","asd","asd"};
            Console.Write((str.Where(x => x == "asd").Count() / 5.0));
           
            Console.ReadKey();
        }
    }
}
