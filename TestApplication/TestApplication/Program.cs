using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            #region firstList
            List<int> firstList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var result = firstList.Where(x => x % 2 == 0).ToList();

            firstList[1] = 1;

            result = firstList.Where(x => x % 2 == 0).ToList();

            foreach (var elem in result) {
                Console.WriteLine(elem);
            }
            #endregion

            Console.ReadKey();
        }
    }
}
