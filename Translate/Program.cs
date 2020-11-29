using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translate
{
    class Program
    {
        static void Main(string[] args)
        {
            int x1 = 192;
            int x2 = 168;
            int x3 = 0;
            int x4 = 56;

            Console.WriteLine(Convert.ToString(x1, 2).PadLeft(8, '0'));
            Console.WriteLine(Convert.ToString(x2, 2).PadLeft(8, '0'));
            Console.WriteLine(Convert.ToString(x3, 2).PadLeft(8, '0'));
            Console.WriteLine(Convert.ToString(x4, 2).PadLeft(8, '0'));

            string s = Convert.ToString(x1, 2).PadLeft(8, '0') + Convert.ToString(x2, 2).PadLeft(8, '0') + Convert.ToString(x3, 2).PadLeft(8, '0') + Convert.ToString(x4, 2).PadLeft(8, '0');

            Console.WriteLine(s);

            UInt32 bit = Convert.ToUInt32(s, 2);

            Console.WriteLine(bit);

            Console.ReadKey();
        }
    }
}
