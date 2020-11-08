using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    class Deplasare : Action
    {
        public Deplasare(String number) : base(number) { }

        public override void run()
        {
            //scoate simbolul din sirul de intrare
            String symbol = intrare.popNextSymbol();
            //baga-l in stiva impreuna cu nr actiunii
            stiva.Push(symbol, number);
        }
    }
}
