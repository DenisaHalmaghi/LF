using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    class Helper
    {
        public Inchidere inchidere(List<Articol> articole)
        {
            //might have to clone it!!!!
            List<Articol> inchidere = articole;
            List<string> dejaDerivate = new List<string>();

            for (int i = 0; i < inchidere.Count; i++)
            {
                Articol articol = inchidere[i];
                if (!articol.isTerminal())
                {
                    string symbol = articol.getCurrentSymbol();
                    if (!dejaDerivate.Contains(symbol))
                    {
                        List<Tuple<string, string>> productii = Program.productii.get(symbol);
                        foreach (Tuple<string, string> productie in productii)
                        {
                            Articol articolNou = new Articol(productie);
                            //if new articol not already in inchidere
                            if (!inchidere.Contains(articolNou))
                            {
                                inchidere.Add(articolNou);
                            }
                        }
                        dejaDerivate.Add(symbol);
                    }
                }
            }

            return new Inchidere(inchidere);


            //trebe sa avem un punct virtual
            //T->.F
            //nu schimbam productia
            //retinem pozitia punctului
        }

        public Inchidere genSalt(List<Articol> articole, string symbol)
        {
            Articol sarit;
            List<Articol> sarite = new List<Articol>();

            foreach (Articol articol in articole)
            {
                sarit = articol.salt(symbol);
                if (sarit != null)
                {
                    sarite.Add(sarit);
                }
            }
            return inchidere(sarite);
        }

        public void colectie()
        {
            List<Articol> articole = new List<Articol>();
            articole.Add(new Articol(Tuple.Create("E", "E"), 0));
            Inchidere inchidere0 = inchidere(articole);

            Inchidere inc;

            Console.WriteLine("inchidere initialaaa");
            inchidere0.toString();
            Console.WriteLine("---------------------");
            //colectia noastra, lista de inchideri
            List<Inchidere> col = new List<Inchidere>();
            col.Add(inchidere0);

            for (int i = 0; i < col.Count; i++)
            {
                foreach (string symbol in Program.simboluri)
                {
                    inc = genSalt(col[i].articole(), symbol);
                    //daca inchiderea nu mai e prezenta in colectie, bag-o
                    if (inc.size() > 0 && !col.Contains(inc))
                    {
                        Console.WriteLine($"-------------{i}-{symbol}------------");
                        Console.WriteLine($"I{col.Count}:");
                        col.Add(inc);
                        inc.toString();

                    }
                }
            }


        }



    }
}
