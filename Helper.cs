using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    class Helper
    {
        protected Dictionary<Tuple<int, string>, string> perechi = new Dictionary<Tuple<int, string>, string>();

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
                            //AICI TREBE MODIFICAT - 
                            //la comparatie ->
                            //sa nu fie productia pusa deja - indiferent de poz punctului
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
                    if (inc.size() > 0)
                    {
                        if (!col.Contains(inc))
                        {
                            perechi.Add(Tuple.Create(i, symbol), col.Count.ToString());
                            Console.WriteLine($"-------------{i}-{symbol}------------");
                            Console.WriteLine($"I{col.Count}:");
                            col.Add(inc);
                            inc.toString();
                        }
                        else
                        {
                            //ia aia care e bagata deja
                            int index = col.FindIndex(x => x.Equals(inc));
                            perechi.Add(Tuple.Create(i, symbol), index.ToString());
                        }
                    }
                }
            }

            construiesteTabele(col);
        }

        public void construiesteTabele(List<Inchidere> colectie)
        {
            for (int i = 0; i < colectie.Count; i++)
            {
                List<Articol> articole = colectie[i].articole();
                for (int j = 0; j < articole.Count; j++)
                {
                    Articol articol = articole[j];
                    string symbol = articol.getCurrentSymbol();
                    string val;
                    perechi.TryGetValue(Tuple.Create(i, symbol), out val);
                    if (symbol == Program.DEPASESTE) //adica e punctul la final
                    {
                        //to be implemented
                        symbol = articol.getPrevSymbol();
                        if (symbol == Program.START)
                        {
                            Program.tabelaActiuni.addValue(i, "$", Program.ACCEPT);
                        }
                        else
                        {
                            string leftSymbol = articol.getProduction().Item1;
                            List<string> urm;
                            int index = Program.productii.getIndex(articol.getProduction());
                            Program.urmatori.TryGetValue(leftSymbol, out urm);
                            foreach (var urmator in urm)
                            {
                                Program.tabelaActiuni.addValue(i, urmator, $"r{index + 1}");
                            }
                        }

                    }
                    else if (articol.isTerminal())
                    {
                        Program.tabelaActiuni.addValue(i, symbol, $"d{val}");
                    }
                    else
                    {
                        //Console.WriteLine(symbol);
                        Program.tabelaSalt.addValue(i, symbol, val);
                    }
                }
            }
            Console.WriteLine("\ntabela de salt --------------------------------\n");
            Program.tabelaSalt.toString();
            Console.WriteLine("\ntabela de actiuni --------------------------------\n");
            Program.tabelaActiuni.toString();

        }

        public string prim(string symbol, List<string> primi)
        {
            //daca e terminal
            if (!Program.neterminale.Contains(symbol))
            {
                return symbol;
            }

            foreach (var productie in Program.productii.get(symbol))
            {
                string primul = Char.ToString(productie.Item2[0]);
                if (primul != symbol)
                {
                    string p = prim(primul, primi);
                    if (p != "")
                    {
                        primi.Add(prim(primul, primi));
                    }
                }
            }
            return "";
            //daca nu prim de symbol e prim de primul simbol din partea dreapta a productiei
        }

        /* public string urmator(string symbol, List<string> primi)
         {
             //daca e simbolul de start baga dolaru
             //daca e la final (nu mai urmeaza nik dupa el=> e egal cu urm (ce-i in stanga productiei)
             //daca nu 
             //daca urmatorul simbol e terminal atunci ala ramane
             //daca urmatorul simbol e neterminal atunci avem prim de el
             //daca prim are un dolar bagat
         }*/
    }
}
