using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlissonSGBD.Engine.SQL
{
	public class Token : UntypedToken
	{
		public string Type {get; set;}
        public Token(string value, string type, int charIndex) : base (value, charIndex)
        {
            Type = type;
        }
        public Token(UntypedToken b, string type) : base(b.Value, b.FirstCharIndex)
        {
           
            Type = type;
        }
    }

    public class UntypedToken
    {
        public string Value { get; set; }
        public int FirstCharIndex { get; set; }

        public UntypedToken(string Value, int FirstCharIndex) {
            this.Value = Value;
            this.FirstCharIndex = FirstCharIndex;
        }
    }

    public class Lexer
	{
        static string[] keywords = {
			"select", "where", "from", "insert", "into", "values", "alter",
            "table", "add", "remove", "update", "set", "on", "do"
        };
        static string[] operators = {
			"=", ">=", "<=", "!=", ">", "<", "+", "-", "and", "or"
		};

        static string[] logicalOperators = {
            "and", "or"
        };

        static char[] operatorsChars;
        static char[] symbols = {
            ',', '.', '(', ')', ';', '"', '\'', '*'
        };

        public Lexer()
		{
            List<char> founded = new List<char>();
            foreach (string op in operators) {
                foreach (char c in op) {
                    if (!founded.Contains(c)) founded.Add(c);
                }
            }
            Debug.Write(founded[2]);
            operatorsChars = founded.ToArray();
		}
		
		public List<Token> Tokenize(string sql){
            List<Token> tokens = new List<Token>();
            //Separar strings por espaços
            //Analizar cada parte, buscando conjunto de caracteres de mesmo tipo
            //: alphanumericos, simbolos e operações

            //Separated by type
            List<UntypedToken> splitedByType = new List<UntypedToken>();

            bool isStr = false;

            int actualIndex = 0;
            string lastToken = "";
            string lastTokenType = "";

            for (int j = 0; j < sql.Length; j++) {
                char c = sql[j];

                string type = GetTypeOfChar(c);

                if (c == '\'' || c == '\"')
                {
                    if (isStr)
                    {
                        splitedByType.Add(new UntypedToken("\"" + lastToken + "\"", actualIndex));
                        lastToken = "";
                        lastTokenType = "literal";
                        isStr = false;
                    }
                    else {
                        if (lastToken.Length > 0) splitedByType.Add(new UntypedToken(lastToken, actualIndex));
                        lastToken = "";
                        isStr = true;
                    }
                    continue;
                }

                if (isStr) {
                    lastToken += c;
                    continue;
                }

                if (c == ' ') {
                    if (lastToken.Length > 0)
                    {
                        // adiciona token passado
                        splitedByType.Add(new UntypedToken(lastToken, actualIndex));
                    }
                    lastToken = "";
                    lastTokenType = "separator";
                    continue;
                }

                if (type == null) continue;

                //Solo tokens
                if (type == "symbol") {
                    //Verifica se há um token sendo criado antes dele
                    if (lastToken.Length > 0)
                    {
                        // adiciona token passado
                        splitedByType.Add(new UntypedToken(lastToken, actualIndex));
                    }
                    //Adiciona separadamente
                    splitedByType.Add(new UntypedToken(c.ToString(), actualIndex));
                    lastToken = "";
                }
                //Mesmo tipo que o caractere anterior
                else if (lastTokenType == type)
                {
                    lastToken += c;
                }
                else
                {
                    // tipo diferente
                    if (lastToken.Length > 0)
                    {
                        // adiciona token passado
                        splitedByType.Add(new UntypedToken(lastToken, actualIndex - 1));
                    }
                    // inicia novo token
                    lastToken = c.ToString();
                }

                // Se é o ultimo caractere
                if (j == sql.Length - 1)
                {
                    //Adiciona
                    if(lastToken.Length > 0) splitedByType.Add(new UntypedToken(lastToken, actualIndex));
                }

                lastTokenType = type;
                actualIndex += 1;
            }

            foreach (var sby in splitedByType) {
                // Type tokens

                if (sby.Value.StartsWith("\"") && sby.Value.EndsWith("\"")) tokens.Add(new Token(sby, "literal"));
                else if (isNumber(sby.Value))
                {
                    if (sby.Value.Contains(".")) tokens.Add(new Token(sby, "float"));
                    else tokens.Add(new Token(sby, "int"));
                }
                else if (isKeyword(sby.Value)) tokens.Add(new Token(sby, "keyword"));
                else if (isOperator(sby.Value)) tokens.Add(new Token(sby, "operator"));
                else if (isSymbol(sby.Value[0])) tokens.Add(new Token(sby, "symbol"));
                else tokens.Add(new Token(sby, "identifier"));
            }

            return tokens;
		}

		public static bool isAlpha(char s) {
			return Char.IsLetterOrDigit(s) || s == '_';
		}

        public static bool isOperatorChar(char s)
        {
            bool isOperatorChar = false;
            foreach (char c in operatorsChars)
            {
                if (c == s)
                {
                    isOperatorChar = true;
                    break;
                }
            }
            return isOperatorChar;
        }

        public static bool isOperator(string s)
        {
            bool isOperator = false;
            foreach (string c in operators)
            {
                if (c == s.ToLower())
                {
                    isOperator = true;
                    break;
                }
            }
            return isOperator;
        }

        public static bool isLogicalOperator(string s)
        {
            bool isOperator = false;
            foreach (string c in logicalOperators)
            {
                if (c == s.ToLower())
                {
                    isOperator = true;
                    break;
                }
            }
            return isOperator;
        }

        public static bool isSymbol(char s) {
			bool isSymbol = false;
			foreach (char symbol in symbols)
			{
				if (symbol == s) {
					isSymbol = true;
					break;
				}
			}
			return isSymbol;
		}

        public static bool isKeyword(string s)
        {
            s = s.ToLower();
            bool isKey = false;
            foreach (string keyword in keywords)
            {
                if (keyword == s)
                {
                    isKey = true;
                    break;
                }
            }
            return isKey;
        }

        public static bool isNumber(string s)
        {
            bool isNumber = true;
            foreach (char c in s)
            {
                if (!Char.IsDigit(c) && c != '.')
                {
                    isNumber = false;
                    break;
                }
            }
            return isNumber;
        }

        public static string GetTypeOfChar(char c) {
            if (isAlpha(c)) return "alpha";
            if (isOperatorChar(c)) return "operator";
            if (isSymbol(c)) return "symbol";

            return null;
        }
    }
}	
