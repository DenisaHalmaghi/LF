using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace First_App
{
    class Input
    {
        Queue<String> symbols = new Queue<String>();

        public Input(String input)
        {
            //  MatchCollection g = Regex.Matches(input, @"([a])|([+])|([*])");
            String regex = "";
            foreach (string terminal in Program.terminale)
            {
                regex += $"|([{terminal}])";

            }

            foreach (string neterminal in Program.neterminale)
            {
                regex += $"|([{neterminal}])";
            }

            regex = regex.Substring(1);

            foreach (Match symbol in Regex.Matches(input, regex))
            {
                symbols.Enqueue(symbol.Value);
            }
            symbols.Enqueue("$");
        }

        public String getNextSymbol()
        {
            return symbols.Peek();
        }

        public String popNextSymbol()
        {
            return symbols.Dequeue();
        }

        public bool isEmpty()
        {
            return symbols.Count == 0;
        }
    }
}
