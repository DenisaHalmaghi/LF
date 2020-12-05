using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    class Reducere : Action
    {
        protected Tuple<string, string> productie;
        public Reducere(String number) : base(number)
        {
            productie = Program.productii.get(Int32.Parse(number) - 1);
        }

        public override void run()
        {

            //scoate din stiva vechile stari si simboluri
            String right = productie.Item1;
            stiva.Pop(productie.Item2.Substring(0, 1));
            //ia din tabela de salt starea urmatoare
            String state = stiva.LatestState();
            state = Program.tabelaSalt.getValue(Int32.Parse(state), right);
            //inlocuieste in stiva perechea
            stiva.Push(right, state);
        }
    }
}
