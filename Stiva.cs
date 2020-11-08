using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    class Stiva

    {
        protected List<Tuple<string, string>> pairs = new List<Tuple<string, string>>();

        public Stiva()
        {
            pairs.Add(Tuple.Create("$", "0"));
        }

        public void Push(string symbol, string state)
        {
            pairs.Add(Tuple.Create(symbol, state));
        }

        public void Pop(string value)
        {
            int index = pairs.FindIndex(pair => pair.Item1 == value);
            pairs.RemoveRange(index, pairs.Count - index);
        }

        public String LatestState()
        {
            return pairs[pairs.Count - 1].Item2;
        }

    }
}
