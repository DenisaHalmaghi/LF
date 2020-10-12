using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {

            string[] neterminale = { "E", "T", "F" };
            string[] terminale = { "a", "+", "*", "," };
            Dictionary<string, string[]> productii = new Dictionary<string, string[]>();

            //Map all productions to keys
            productii.Add("E", new string[] { "E+T", "T" });
            productii.Add("T", new string[] { "T*F", "F" });
            productii.Add("F", new string[] { "a", "E" });

            //Starting string
            string S = "E";

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
            Console.WriteLine(output.Length);

            //keep iterating over each symbol

            //when you find a non terminal replace it
            //go back 1 position

        }

        static string getRandomValue(string[] array, int length)
        {
            return array[rand.Next(0, length)];
        }
    }
}
