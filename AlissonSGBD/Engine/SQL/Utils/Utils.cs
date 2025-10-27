using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlissonSGBD.Engine.SQL.Utils
{
    internal class Utils
    {

        public static bool isTokenValue(Token token)
        {
            string[] valTypes = { "int", "float", "literal" };
            foreach (string valType in valTypes)
            {
                if (token.Type == valType) return true;
            }
            return false;
        }
    }
}
