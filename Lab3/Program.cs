using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    static class LexicalAnalyzer
    {
        private static string[] terminals = new string[]
        {"==", "++", "--", "=", "?", ":", "*", "[", "]", ";", "{", "}", "+", "-", "/", "(", ")", "<", ">"};
        public static void LexicalAnalyze(this string analyzedString)
        {
            string[] variables = analyzedString.Split(terminals, StringSplitOptions.None);
            foreach (string str in variables)
            {
                if (!str.Equals(""))
                {
                    try
                    {
                        int value = Int32.Parse(str);
                        Console.WriteLine(value + " is a number");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(str + " is a variable");
                    }
                }
            }
            foreach (string token in terminals)
            {
                if (analyzedString.Contains(token))
                {
                    analyzedString = analyzedString.Replace(token, "");
                    Console.WriteLine(token + " is a token");
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string str = "b=c?d:2*a*[m];";
            Console.WriteLine("Input string:  " + str);
            str.LexicalAnalyze();

            Console.ReadKey();
        }
    }
}
