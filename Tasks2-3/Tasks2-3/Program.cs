using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks2_3
{
    class Program
    {
        //2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
        //а) для целых чисел;
        //б) *для обобщенной коллекции;
        //в) *используя Linq.

        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            numbers.AddRange(new int[] { 12, 45, 65, 11, 21, 54, 21, 12, 12, 54, 87, 98 });
            var numNew = from n in numbers
                         where n == 12
                         select n;
            Console.WriteLine(numNew.Count());
            Console.ReadLine();
        }
    }
}
