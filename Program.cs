using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace First_App
{
    class Program
    {
        static Random rand = new Random();
        static List<string> neterminale = new List<string>();
        static List<string> terminale = new List<string>();
        static Dictionary<string, string[]> productii = new Dictionary<string, string[]>();

        static void Main(string[] args)
        {


            setup();
            //Map all productions to keys

            //Starting string
            /*string S = "E";

            StringBuilder output = new StringBuilder(S); //Start with the starting string

            string[] values = { };
            string value = "";
            productii.TryGetValue("E", out values);
            // Console.WriteLine(value[0]);

            //Derivare extrem stanga 

            //while no non-terminals found
            for (int i = 0; i < output.Length && output.Length < 60; i++)
            {
                string key = output[i].ToString();
                if (productii.TryGetValue(key, out values))
                {
                    value = getRandomValue(values, values.Length);
                    output = output.Replace(key, value, i, 1);
                    i--;
                }

            }

            Console.WriteLine(output);
            Console.WriteLine(output.Length);*/

            //keep iterating over each symbol

            //when you find a non terminal replace it
            //go back 1 position

        }

        static string getRandomValue(string[] array, int length)
        {
            return array[rand.Next(0, length)];
        }

        static void setup()
        {
            StreamReader sr = new StreamReader(System.IO.Path.GetFullPath(@"..\..\..\") + "setup.txt");
            string contents = sr.ReadToEnd();
           
            MatchCollection matches = getParts(contents,"T");
            foreach (Match match in matches)
            {
                terminale.Add(match.ToString());
            }

            matches = getParts(contents, "N");
            foreach (Match match in matches)
            {
                neterminale.Add(match.ToString());
            }

            matches = getParts(contents, "P");
            foreach (Match match in matches)
            {
                mapToProductions(match.ToString());
            }
           
        }

        static MatchCollection getParts(string contents,string part)
        {
            string value = Regex.Match(contents, part + @"=\{(?<word>.+)}").Groups["word"].Value;

            return Regex.Matches(value, @"(?:(?<=\()[^()\s]+(?=\)))|(?:(?<!\()[^()\s,]+(?!\)))");
        }

        static void mapToProductions(string production)
        {
            string[] parts=Regex.Split(production, "->");
            string key = parts[0];
            string[] values=Regex.Split(parts[1], @"[|]");
            //daca nu e neterminal nu avem ce mapa -> date gresite
            if (!neterminale.Contains(key))
            {
                Console.WriteLine($"{key} nu se afla in multimea de neterminale! Va rog sa corectati");
                System.Environment.Exit(1);
            }

            //verificam daca toate caracterele sunt fie terminale fie neterminale
            //merge neterminale+terminale 
            //^[{merged}]+$
            string merged = $"^[{string.Join("", terminale.ToArray())}{string.Join("", neterminale.ToArray())}]+$";

            foreach (string value in values)
            {
                if (!Regex.IsMatch(value, merged))
                {
                    Console.WriteLine($"{value} nu contine doar terminale sau neterminale! Va rog sa verificati lista de productii si sa corectati");
                    System.Environment.Exit(1);
                }
            }

            productii.Add(key,values);
            
        }

    }
}
