using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace First_App
{
    class ActionFactory
    {
        public static Action build(String action)
        {
            if (action == Program.ACCEPT)
            {
                return null;
            }

            MatchCollection matches = Regex.Matches(action, @"(r|d)|([\d]+)");
            String actionType = matches[0].Value;
            String actionNumber = matches[1].Value;

            if (actionType == "d")
            {
                return new Deplasare(actionNumber);
            }

            if (actionType == "r")
            {
                return new Reducere(actionNumber);
            }

            throw new FormatException("Nu e bun formatul valorilor");
        }


    }
}
