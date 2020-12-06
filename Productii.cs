using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    class Productii
    {
        public List<Tuple<string, string>> productii = new List<Tuple<string, string>>();
        public void Add(String left, String right)
        {
            productii.Add(Tuple.Create(left, right));
        }

        public Tuple<string, string> get(int productie)
        {
            return productii[productie];
        }

        public List<Tuple<string, string>> get(string derivat)
        {
            List<Tuple<string, string>> cautate = new List<Tuple<string, string>>();
            foreach (Tuple<string, string> productie in productii)
            {
                if (productie.Item1 == derivat)
                {
                    cautate.Add(productie);
                }
            }

            return cautate;
        }

        public int getIndex(Tuple<string, string> cautat)
        {
            return productii.FindIndex(productie => productie.Item1 == cautat.Item1 && productie.Item2 == cautat.Item2);
        }


    }


}
