using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using AlissonSGBD.Engine.FileHelpers;
using AlissonSGBD.Engine.SQL;
using AlissonSGBD.Engine.SQL.AST;

namespace AlissonSGBD.Engine
{
	//Responsabilidade -> Gerenciar todos os componentes
	public class Core
	{	
		FileHelper fh = new FileHelper();
		Parser parser = new Parser();
        Lexer lexer = new Lexer();

        public Core()
		{
			Init();
		}
		
		void Init(){
			
		}
		
		public void TestLexer(string sql) {
			List<Token> tokens = lexer.Tokenize(sql);
			foreach(Token token in tokens){
				Debug.WriteLine("[" + token.Value + " : " + token.Type + "]");
			}

			TSqlStatement tree = parser.Parse(tokens);
			if (tree != null)
				tree.Describe(0);
			else
				Debug.WriteLine("Query is not valid");
		}
	}
}
