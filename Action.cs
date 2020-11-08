using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    abstract class Action
    {
        protected String number;
        protected Input intrare = Program.intrare;
        protected Stiva stiva = Program.stiva;
        public abstract void run();

        public Action(String number)
        {
            this.number = number;
        }
    }
}
