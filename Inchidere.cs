using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace First_App
{
    class Inchidere : IEquatable<Inchidere>
    {
        List<Articol> inchidere;

        public Inchidere(List<Articol> inc)
        {
            inchidere = inc;
        }

        public void toString()
        {
            foreach (Articol articol in inchidere)
            {
                articol.toString();
            }
        }

        public int size()
        {
            return inchidere.Count;
        }

        public List<Articol> articole()
        {
            return inchidere;
        }

        public bool Equals([AllowNull] Inchidere other)
        {
            if (other.size() != inchidere.Count)
            {
                return false;
            }

            List<Articol> otherArticles = other.articole();

            for (int i = 0; i < inchidere.Count; i++)
            {
                if (!inchidere[i].Equals(otherArticles[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
