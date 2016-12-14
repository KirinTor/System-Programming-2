using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab5
{
    static class ParseClass
    {
        private static int step = 0;
        public static void Analyze(this string s)
        {
            Parser.SEMICOLON.Parse(s);
        }
        private static void Parse(this Parser parser, string s)
        {
            switch (parser)
            {
                case Parser.SEMICOLON:
                    s = s.Trim();
                    if (s.EndsWith(";"))
                    {
                        Console.WriteLine("Step " + step + " ;");
                        step++;
                        Parser.STATEMENT_LIST.Parse(s.Substring(0, s.Length - 1));

                    }
                    else
                    {
                        Console.WriteLine("Step " + step + " Betrayal! Semicolon missing");
                        step++;
                    }
                    break;

                case Parser.STATEMENT_LIST:
                    string[] separators = new string[] { " then", " else" };
                    string[] parts = s.Trim().Split(separators, StringSplitOptions.None);
                    int p = 0;
                    foreach (string part in parts)
                    {
                        string str = "";
                        if (p == 1) str = " then" + part;
                        else if (p == 2) str = " else" + part;
                        else str = part;
                        Parser.STATEMENT.Parse(str);
                        p++;
                    }
                    break;

                case Parser.STATEMENT:
                    if (s.StartsWith("if"))
                    {
                        Console.WriteLine("Step " + step + " if");
                        step++;
                        Parser.IF.Parse(s.Substring(2));

                    }
                    else if (s.StartsWith(" then "))
                    {
                        Console.WriteLine("Step " + step + " then");
                        step++;
                        Parser.UNIT.Parse(s.Substring(4).Trim());

                    }
                    else if (s.StartsWith(" else "))
                    {
                        Console.WriteLine("Step " + step + " else");
                        step++;
                        Parser.UNIT.Parse(s.Substring(4).Trim());

                    }
                    else
                    {
                        Console.WriteLine("Step " + step + " Betrayal! Illegal conditional operator");
                        step++;
                    }
                    break;

                case Parser.IF:
                    int i = s.IndexOf("(");
                    int j = s.IndexOf(")");
                    if (i != -1 && j != -1)
                    {
                        Console.WriteLine("Step " + step + "( )");
                        step++;
                        Parser.LOGICAL.Parse(s.Substring(i + 1, j - 2).Trim());

                        //Parser.STATEMENT.Parse(s.Substring(j + 1).Trim());
                    }
                    else
                    {
                        Console.WriteLine("Step " + step + " Betrayal! Bracket missing");
                        step++;
                    }
                    break;

                case Parser.EXPRESSION:
                    //string[] terminals = new string[] { "==", "++", "--", "=", "?", ":", "*", "[", "]", ";", "{", "}", "+", "-", "/", "(", ")", "<", ">" };
                    string[] operators = new string[] { "/", "+", "-", "*" };
                    string[] exParts = s.Split(operators, StringSplitOptions.None);
                    foreach (string part in exParts)
                    {
                        if (part != "")
                            Parser.VARIABLE.Parse(part.Trim());
                        else
                        {
                            Console.WriteLine("Step " + step + " Betrayal! Illegal variable name");
                            step++;
                        }
                    }
                    break;


                case Parser.LOGICAL:
                    if (s.Contains("<"))
                    {
                        i = s.IndexOf("<");
                        Console.WriteLine("Step " + step + " " + s.Substring(0, i));
                        step++;
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());

                        Console.WriteLine("Step " + step + " " + s.Substring(i + 1));
                        step++;
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());

                    }
                    else if (s.Contains(">"))
                    {
                        i = s.IndexOf(">");
                        Console.WriteLine("Step " + step + " " + s.Substring(0, i));
                        step++;
                        Parser.EXPRESSION.Parse(s.Substring(0, i).Trim());

                        Console.WriteLine("Step " + step + " " + s.Substring(i + 1));
                        step++;
                        Parser.EXPRESSION.Parse(s.Substring(i + 1).Trim());

                    }
                    else
                    {
                        Console.WriteLine("Step " + step + " Betrayal! Illegal logical");
                        step++;
                    }
                    break;

                case Parser.VARIABLE:
                    string[] regulars = new string[] { "begin", "end", "if", "then", "else" };

                    foreach (string regular in regulars)
                    {
                        {
                            if (s == regular)
                            {
                                Console.WriteLine("Step " + step + " Betrayal! Illegal variable name (variable can not be a regular expression)");
                                step++;
                                break;
                            }
                        }
                    }
                    break;

                case Parser.UNIT:
                    if (s.StartsWith("begin "))
                    {
                        Console.WriteLine("Step " + step + " begin");
                        step++;
                    }
                    else
                    {
                        Console.WriteLine("Step " + step + " Betrayal! begin missing");
                        step++;
                    }
                    if (s.EndsWith(" end"))
                    {
                        Console.WriteLine("Step " + step + " end");
                        step++;
                    }
                    else
                    {
                        Console.WriteLine("Step " + step + " Betrayal! end missing");
                        step++;
                    }
                    break;
            }

        }
    }
    public enum Parser
    {
        SEMICOLON,
        IF,
        UNIT,
        STATEMENT_LIST,
        STATEMENT,
        LOGICAL,
        EXPRESSION,
        VARIABLE
    }
    class Program
    {
        public static void Main(String[] args)
        {
            string program = "if (a > b) then begin end; else begin end;";
            //string program = "if (an* > begin) tthen begin end; else beginend;";
            Console.WriteLine("Input string: " + program);
            program.Analyze();
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}




