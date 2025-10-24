using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlissonSGBD.Engine.SQL
{
	public class Token
	{
		public string Value {get; set;}
		public string Type {get; set;} //keyword, identifier, operator, value
        public string CharIndex { get; set; }
    }
	
	public class Lexer
	{
        static string[] keywords = {
			"select", "where", "from", "insert", "into", "values", "alter",
            "table", "add", "remove", "update"
        };
        static string[] operators = {
			"=", ">=", "<=", "!=", ">", "<"
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
			List<string> spaceSplited = new List<string>(sql.Split(' '));
            List<string> splitedValues = new List<string>();

            List<int> charIndexes = new List<int>();
            int spaceCounter = 0;

            while (spaceSplited.Count > 0) {
                string lastType = null;
                string formed = "";

                for (int i = 0; i < spaceSplited[0].Length; i++)
                {
                    char c = spaceSplited[0][i];
                    string type = null;
                    if (isAlpha(c)) type = "alpha";
                    if (isOperatorChar(c)) type = "operator";
                    if (isSymbol(c)) type = "symbol";

                    if (type == null) {
                        Debug.WriteLine("ERROR: unknown token");
                        break;
                    }

                    Debug.WriteLine(c+" "+formed);

                    if (i == spaceSplited[0].Length - 1)
                    {
                        Debug.WriteLine("É FINAL " + (i == spaceSplited[0].Length - 1));
                        splitedValues.Add(formed + c);
                    }
                    else if (type == "symbol") {
                        if (formed.Length > 0)
                        {
                            charIndexes.Add(spaceCounter + i);
                            splitedValues.Add(formed);
                        }
                        splitedValues.Add(c.ToString());
                        charIndexes.Add(spaceCounter + i + 1);
                        formed = "";
                    }
                    else if (lastType != type || lastType == null)
                    {
                        Debug.WriteLine("DIFERENTE DO ANTERIOR, " + formed + " " + c);
                        if (formed.Length > 0)
                        {
                            Debug.WriteLine("ADICIONANDO " + formed);
                            splitedValues.Add(formed);
                            formed = "";
                        }
                        formed += c;
                        Debug.WriteLine("FORMED " + formed);
                    }
                    else { 
                        formed += c;
                    }
                    lastType = type;
                }

                spaceSplited.RemoveAt(0);
                spaceCounter++;
            }

            string str = "";
            bool isStr = false;
            int strStart;
            int strEnd;

            for (int i = 0; i < splitedValues.Count; i++)
            {
                string ss = splitedValues[i];
                Debug.WriteLine(ss);
                bool symbol = false;
                bool allDigits = true;
                foreach (char c in ss) { 
                    if (isSymbol(c))
                    {
                        if (c == '"')
                        {
                            isStr = !isStr;
                            if (isStr) strStart = i;
                            else strEnd = i;
                            continue;
                        }

                        symbol = true;
                        Token t = new Token();
                        t.Value = c.ToString();
                        t.Type = "symbol";
                        tokens.Add(t);

                        
                    }
                    if (!Char.IsDigit(c))
                    {
                        allDigits = false;
                    }
                }
                if (symbol) continue;

                if (isStr)
                {
                    str += ss;
                }
                else if (str.Length > 0)
                {
                    Token t = new Token();
                    t.Value = str + "\"";
                    t.Type = "value";
                    tokens.Add(t);

                    str = "";
                }
                else if (isOperator(ss))
                {
                    Token t = new Token();
                    t.Value = ss;
                    t.Type = "operator";
                    tokens.Add(t);
                }

                else if (isKeyword(ss.ToLower()))
                {
                    Token t = new Token();
                    t.Value = ss;
                    t.Type = "keyword";
                    tokens.Add(t);
                }
                else if (allDigits) {
                    Token t = new Token();
                    t.Value = ss;
                    t.Type = "value";
                    tokens.Add(t);
                }
                else
                {
                    Token t = new Token();
                    t.Value = ss;
                    t.Type = "identifier";
                    tokens.Add(t);
                }
            }

            return tokens;
		}

		public static bool isAlpha(char s) {
			return Char.IsLetterOrDigit(s);
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
                if (c == s)
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
                if (!Char.IsDigit(c))
                {
                    isNumber = false;
                    break;
                }
            }
            return isNumber;
        }
    }
}	
