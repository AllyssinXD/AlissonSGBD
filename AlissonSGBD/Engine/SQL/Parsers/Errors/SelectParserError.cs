using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlissonSGBD.Engine.SQL.Parsers.Errors
{
    public class SelectParserError : ParserError
    {
        public SelectParserError(int code)
            : base(code, GetMessageFromCode(code))
        {
            
        }

        private static string GetMessageFromCode(int code)
        {
            switch (code)
            {
               case 10: return "Erro de sintaxe na cláusula SELECT.";
               case 20: return "Falta o nome da tabela após FROM.";
               case 30: return "Parêntese ou aspas não fechados.";
               case 40: return "Token inesperado encontrado durante o parsing.";
               case 50: return "Erro genérico de parsing na expressão WHERE.";
               case 51: return "Where vazio";
               case 101: return "Nenhuma coluna especificada após SELECT.";
            }


            return "Unknown error";
        }
    }
}
