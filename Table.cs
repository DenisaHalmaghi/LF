using System;
using System.Collections.Generic;
using System.Text;

namespace First_App
{
    class Table
    {
        protected Dictionary<string, string>[] tabel;

        public Table(int rows = 12)
        {
            tabel = new Dictionary<string, string>[rows];
            //Initialize each row
            for (int i = 0; i < rows; i++)
            {
                tabel[i] = new Dictionary<string, string>();
            }
        }

        public Dictionary<string, string> getRow(int row)
        {
            return tabel[row];
        }

        public String getValue(int row, String column)
        {
            String value;
            if (!tabel[row].TryGetValue(column, out value))
            {
                throw new InvalidOperationException("Nu avem nicio actiune pentru aceasta combinatie de intrari in tabela");
            }
            return value;
        }

        public void addValue(int row, string column, string value)
        {

            if (!tabel[row].ContainsKey(column))
            {
                tabel[row].Add(column, value);
            }

        }
        public void toString()
        {
            //tabel.Length
            for (int i = 0; i < tabel.Length; i++)
            {
                Dictionary<string, string> row = tabel[i];
                foreach (KeyValuePair<string, string> pair in row)
                {
                    if (pair.Value != "")
                    {
                        Console.WriteLine($"({i},{pair.Key})={pair.Value}");
                    }
                }
            }
        }

    }
}
