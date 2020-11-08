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


    }


}
