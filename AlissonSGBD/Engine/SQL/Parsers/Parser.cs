/*
 * Created by SharpDevelop.
 * User: aliss
 * Date: 24/10/2025
 * Time: 14:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using AlissonSGBD.Engine.SQL.AST;
using AlissonSGBD.Engine.SQL.Models;
using AlissonSGBD.Engine.SQL.Parsers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace AlissonSGBD.Engine.SQL
{	
	public class Parser
	{
        public TSqlStatement Parse(List<Token> tokens)
		{
			if (tokens[0].Type != "keyword")
			{
				Debug.WriteLine("Expecting command or keyword");
				return null;
			}

			// Select
			switch (tokens[0].Value.ToLower()) {
                case "select":
                    return SelectParser.Parse(tokens);
            }

			Debug.WriteLine("Unknown Error");
            return null;
		}

        public static StatementNode ConvertValToStatementNode(string val) {
            if (val.Contains("\""))
            {
                return new ConditionStatementNode<string>(val.Replace("\"", ""));
            }
            else if (Lexer.isNumber(val))
            {
                return new ConditionStatementNode<int>(int.Parse(val));
            }
            else {
                
                return new ColumnStatementNode(val);
            }
        }
	}
}
