using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlissonSGBD.Engine.SQL.Parsers.Errors
{
    public class ParserError : Exception
    {
        public int Code { get; }

        public ParserError(int code) : base(GetMessageFromCode(code)) {
            Code = code;
        }

        public ParserError(int code, string message) : base(message)
        {
            Code = code;
        }

        private static string GetMessageFromCode(int code)
        {
            switch (code)
            {
                case 1: return "Erro de sintaxe SQL genérico.";
                case 2: return "Token inesperado encontrado.";
                case 3: return "Fim inesperado da instrução SQL.";
                case 4: return "Identificador inválido ou não permitido.";
                case 5: return "Número inválido ou malformado.";
                case 6: return "Aspas não fechadas em string literal.";
                case 7: return "Parêntese ou colchete não fechado.";
                case 8: return "Comentário não encerrado corretamente.";
                case 9: return "Delimitador ou separador ausente.";
                case 10: return "Palavra-chave SQL fora de contexto.";
            }

            return "Unknown error";
        }
    }
}
