using AlissonSGBD.Engine.SQL.AST;
using AlissonSGBD.Engine.SQL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlissonSGBD.Engine.SQL.Parsers
{
    internal class SelectParser
    {

        public static TSqlSelect Parse(List<Token> tokens) {
            int fromI = 1;
            for (int i = 1; i < tokens.Count; i++)
            {
                if (tokens[i].Value.ToLower() == "from")
                {
                    fromI = i;
                }
            }
            if (fromI < 2)
            {
                Debug.WriteLine("Cant select 0 columns");
                return null;
            }

            Debug.WriteLine("SEARCHING COLUMNS ---------");
            List<ColumnStatementNode> columns = new List<ColumnStatementNode>();
            for (int i = 1; i < fromI; i++)
            {
                Debug.WriteLine("FROMI " + fromI + " INDEX " + i + " TOKEN " + tokens[i].Value);
                if (tokens[i].Type == "symbol" && tokens[i].Value == ",")
                {
                    if (i % 2 == 1 || i == fromI - 1)
                    {
                        Debug.WriteLine("Unexpected \",\" at " + tokens[i].CharIndex);
                        return null;
                    }
                }
                if (tokens[i].Type == "identifier" || tokens[i].Value == "*")
                    columns.Add(new ColumnStatementNode(tokens[i].Value));
            }

            Debug.WriteLine("SEARCHING ENTITIES ---------");
            int whereI = tokens.Count;
            for (int i = fromI; i < tokens.Count; i++)
            {
                if (tokens[i].Value.ToLower() == "where")
                {
                    whereI = i;
                }
            }

            List<EntityStatementNode> entities = new List<EntityStatementNode>();
            for (int i = fromI; i < whereI; i++)
            {
                Debug.WriteLine("FROMI " + fromI + "WHEREI " + whereI + " INDEX " + i + " TOKEN " + tokens[i].Value);
                if (tokens[i].Type == "symbol" && tokens[i].Value == ",")
                {
                    if (i % 2 == 1 || i == fromI - 1)
                    {
                        Debug.WriteLine("Unexpected \",\" at " + tokens[i].CharIndex);
                        return null;
                    }
                }
                if (tokens[i].Type == "identifier")
                    entities.Add(new EntityStatementNode(tokens[i].Value));
            }

            Debug.WriteLine("SEARCHING CONDITIONS ---------");

            ConditionStatementNode con = null;
            for (int i = whereI; i < tokens.Count; i++)
            {
                Debug.WriteLine("WHEREI " + whereI + " INDEX " + i + " TOKEN " + tokens[i].Value);
                if (tokens[i].Type == "operator")
                {
                    if (tokens[i - 1].Type != "value" && tokens[i - 1].Type != "identifier")
                    {
                        Debug.WriteLine("Expecting left side of operation to be a value or a identifier");
                        return null;
                    }
                    if (tokens[i + 1].Type != "value")
                    {
                        Debug.WriteLine("Expecting right side of operation to be a value");
                        return null;
                    }
                    string a = tokens[i - 1].Value;
                    string b = tokens[i + 1].Value;
                    Debug.WriteLine("TRYING TO CREATE " + a + " " + b);
                    con = new ConditionStatementNode();
                    con.Value = tokens[i].Value;
                    con.AddChild(Parser.ConvertValToStatementNode(a));
                    con.AddChild(Parser.ConvertValToStatementNode(b));
                }
            }

            Debug.WriteLine("CREATED SELECT");
            return new TSqlSelect(entities.ToArray(), columns.ToArray(), con);
        }

    }
}
