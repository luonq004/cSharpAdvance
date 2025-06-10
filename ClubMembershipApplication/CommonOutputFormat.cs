using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication
{
    public enum FontTheme
    {
        Default, 
        Danger,
        Success
    }

    public static class CommonOutputFormat
    {
        public static void ChangeFontColor(FontTheme fontTheme)
        {
            if (fontTheme == FontTheme.Default) {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
            } else {
                Console.ResetColor();
            }
        }
    }
}
