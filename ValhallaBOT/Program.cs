using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValhallaBOT
{
    class Program
    {
        static void Main(string[] args)
        {

            var bot = new bot();
            bot.RunAsync().GetAwaiter().GetResult();

        }
    }
}
