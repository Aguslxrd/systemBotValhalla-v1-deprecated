using DSharpPlus.Entities;
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
            string valhallaLogo = @"
       __      __   _ _           _ _       
       \ \    / /  | | |         | | |      
        \ \  / /_ _| | |__   __ _| | | __ _ 
         \ \/ / _` | | '_ \ / _` | | |/ _` |
          \  / (_| | | | | | (_| | | | (_| |
           \/ \__,_|_|_| |_|\__,_|_|_|\__,_|";
            string valhallaDev = "Developed by: #Aguslxrd#0410";
            string devClublog = "DevClub Industries.";

            
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(valhallaDev);
            Console.WriteLine(devClublog);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Initializing bot.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(valhallaLogo);
            Console.ResetColor();
            var bot = new bot();
            bot.RunAsync().GetAwaiter().GetResult();
            //

        }

    }
}
