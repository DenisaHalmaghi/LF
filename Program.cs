using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace First_App
{
    class Program
    {
        static Random rand = new Random();
        public static List<string> neterminale = new List<string>();
        public static List<string> terminale = new List<string>();
        public static List<string> simboluri = new List<string>();
        public static Productii productii = new Productii();
        public static Table tabelaActiuni = new Table();
        public static Table tabelaSalt = new Table();
        public static Input intrare;
        public const String ACCEPT = "acc";
        static Action action;
        public const String DEPASESTE = "depasaste";
        public static Stiva stiva = new Stiva();
        static void Main(string[] args)
        {
            terminale.Add("(");
            terminale.Add(")");
            setup();
            foreach (string neterminal in Program.neterminale)
            {
                simboluri.Add(neterminal);
            }

            foreach (string terminal in Program.terminale)
            {
                simboluri.Add(terminal);

            }
            String state, symbol, actionString = null;
            Helper h = new Helper();
            h.colectie();



            /* while (!intrare.isEmpty())
             {
                 //ia ultima stare din stiva
                 state = stiva.LatestState();
                 //ia primul simbol din sirul de intrare
                 symbol = intrare.getNextSymbol();
                 //ia actiunea rezultata
                 actionString = tabelaActiuni.getValue(Int32.Parse(state), symbol); //could throw error
                 action = ActionFactory.build(actionString);
                 if (action == null)
                 {
                     break;
                 }
                 //ruleaza actiunea
                 action.run();
             }

             Console.WriteLine("acceptare");*/

        }

        static void setupActionTable()
        {
            Dictionary<string, string> row = tabelaActiuni.getRow(0);
            row.Add("a", "d5");
            row.Add("(", "d4");
            row = tabelaActiuni.getRow(1);
            row.Add("+", "d6");
            row.Add("$", ACCEPT);
            row = tabelaActiuni.getRow(2);
            row.Add("+", "r2");
            row.Add("*", "d7");
            row.Add(")", "r2");
            row.Add("$", "r4");
            row = tabelaActiuni.getRow(3);
            row.Add("+", "r4");
            row.Add("*", "r4");
            row.Add(")", "r4");
            row.Add("$", "r4");
            row = tabelaActiuni.getRow(4);
            row.Add("a", "d5");
            row.Add("(", "d4");
            row = tabelaActiuni.getRow(5);
            row.Add("+", "r6");
            row.Add("*", "r6");
            row.Add(")", "r6");
            row.Add("$", "r6");
            row = tabelaActiuni.getRow(6);
            row.Add("a", "d5");
            row.Add("(", "d4");
            row = tabelaActiuni.getRow(7);
            row.Add("a", "d5");
            row.Add("(", "d4");
            row = tabelaActiuni.getRow(8);
            row.Add("+", "d6");
            row.Add(")", "d11");
            row = tabelaActiuni.getRow(9);
            row.Add("+", "r1");
            row.Add("*", "d7");
            row.Add(")", "r1");
            row.Add("$", "r1");
            row = tabelaActiuni.getRow(10);
            row.Add("+", "r3");
            row.Add("*", "r3");
            row.Add(")", "r3");
            row.Add("$", "r3");
            row = tabelaActiuni.getRow(11);
            row.Add("+", "r5");
            row.Add("*", "r5");
            row.Add(")", "r5");
            row.Add("$", "r5");
        }

        static void setupJumpTable()
        {
            Dictionary<string, string> row = tabelaSalt.getRow(0);
            row.Add("E", "1");
            row.Add("T", "2");
            row.Add("F", "3");
            row = tabelaSalt.getRow(4);
            row.Add("E", "8");
            row.Add("T", "2");
            row.Add("F", "3");
            row = tabelaSalt.getRow(6);
            row.Add("T", "9");
            row.Add("F", "3");
            row = tabelaSalt.getRow(7);
            row.Add("F", "10");
        }
        static void setup()
        {
            StreamReader sr = new StreamReader(Path.GetFullPath(@"..\..\..\") + "setup.txt");
            string contents = Regex.Replace(sr.ReadToEnd(), " ", "");

            MatchCollection matches = getParts(contents, "T");
            foreach (Match match in matches)
            {
                terminale.Add(match.ToString());
            }

            matches = getParts(contents, "N");
            foreach (Match match in matches)
            {
                neterminale.Add(match.ToString());
            }

            string[] productions = getProductions(contents, "P");
            foreach (string production in productions)
            {
                mapToProductions(production);
            }

            /*setupActionTable();
            setupJumpTable();*/
            intrare = new Input(Regex.Match(contents, @"I=(?<word>.+)").Groups["word"].Value);
            //  string inp = Regex.Match(contents, @"I=(?<word>.+)").Groups["word"].Value;

        }

        static MatchCollection getParts(string contents, string part)
        {
            string value = Regex.Match(contents, part + @"=\{(?<word>.+)},").Groups["word"].Value;

            return Regex.Matches(value, @"(?:(?<=\()[^()\s]+(?=\)))|(?:(?<!\()[^()\s,]+(?!\)))");
        }

        static string[] getProductions(string contents, string part)
        {
            string value = Regex.Match(contents, part + @"=\{(?<word>.+)},").Groups["word"].Value;
            return Regex.Split(value, @"(?<!\(),(?!\))");
        }

        static void mapToProductions(string production)
        {
            string[] parts = Regex.Split(production, "->");
            string key = parts[0];
            string[] values = Regex.Split(parts[1], @"[|]");
            //daca nu e neterminal nu avem ce mapa -> date gresite
            if (!neterminale.Contains(key))
            {
                Console.WriteLine($"{key} nu se afla in multimea de neterminale! Va rog sa corectati");
                System.Environment.Exit(1);
            }

            //verificam daca toate caracterele sunt fie terminale fie neterminale
            string merged = $"^\\(?[{string.Join("", terminale.ToArray())}{string.Join("", neterminale.ToArray())}]+\\)?$";

            foreach (string value in values)
            {
                if (!Regex.IsMatch(value, merged))
                {
                    Console.WriteLine($"{value} nu contine doar terminale sau neterminale! Va rog sa verificati lista de productii si sa corectati");
                    System.Environment.Exit(1);
                }

                productii.Add(key, value);
            }


        }

    }
}
