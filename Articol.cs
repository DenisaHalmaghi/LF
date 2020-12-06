using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace First_App
{
    class Articol : IEquatable<Articol>
    {
        protected int punct;
        Tuple<string, string> productie;


        public Articol(Tuple<string, string> prod, int p = 0)
        {
            productie = prod;
            punct = p;
        }
        public bool isTerminal()
        {
            string current = getCurrentSymbol();
            if (current == Program.DEPASESTE)
            {
                return true;
            }
            return Program.terminale.Contains(current);
        }

        public string getCurrentSymbol()
        {
            if (punct >= productie.Item2.Length)
            {
                return Program.DEPASESTE;
            }
            return Char.ToString(productie.Item2[punct]);
        }

        public string getPrevSymbol()
        {
            return Char.ToString(productie.Item2[punct - 1]);
        }

        public void toString()
        {
            Console.WriteLine($"{productie.Item1}->{productie.Item2.Insert(punct, ".")}");
        }

        public Articol salt(string symbol)
        {
            string current = getCurrentSymbol();
            if (current == symbol)
            {
                //sari
                return new Articol(productie, punct + 1);
            }
            return null;
        }

        public Tuple<string, string> getProduction()
        {
            return productie;
        }

        public int getPunct()
        {
            return punct;
        }

        public bool Equals([AllowNull] Articol other)
        {
            Tuple<string, string> otherProd = other.getProduction();
            return otherProd.Item1 == productie.Item1
                && otherProd.Item2 == productie.Item2
                && other.getPunct() == punct;
        }
    }
}
