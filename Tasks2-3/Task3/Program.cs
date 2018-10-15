using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
   // 3. * Дан фрагмент программы:

    //а) Свернуть обращение к OrderBy с использованием лямбда-выражения $.
    //б) * Развернуть обращение к OrderBy с использованием делегата Predicate<T>.
    class Program
    {
        /// <summary>
        /// Дан фрагмент программы:
        /// </summary>
        static void Source()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
                  {
                    {"four",4 },
                    {"two",2 },
                    { "one",1 },
                    {"three",3 },
                  };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// а) Свернуть обращение к OrderBy с использованием лямбда-выражения $.
        /// </summary>
        static void Lambda()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
                  {
                    {"four",4 },
                    {"two",2 },
                    { "one",1 },
                    {"three",3 },
                  };
            var d = dict.OrderBy(pair => pair.Value);
            foreach (var pair in d)
            {
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// б) * Развернуть обращение к OrderBy с использованием делегата Predicate<T>.
        /// </summary>
        static void Predicate()
        {
            // Вот здесь нет мыслей. Прошу рассказать на вебинаре.
        }


        static void Main(string[] args)
        {
            Source();
            Console.WriteLine();
            Lambda();
            Console.ReadKey();
        }
    }
}
