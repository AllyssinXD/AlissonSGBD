using AlissonSGBD.Engine.SQL.AST;
using AlissonSGBD.Engine.SQL.Models;
using AlissonSGBD.Engine.SQL.Parsers.Errors;
using AlissonSGBD.Engine.SQL.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlissonSGBD.Engine.SQL.Parsers
{
    internal class SelectParser
    {
        public static TSqlSelect Parse(List<Token> tokens)
        {
            CertifyStruture(tokens);

            List<ColumnStatementNode> columns = ProcessColumns(tokens);
            List<EntityStatementNode> entities = ProcessEntities(tokens);
            ConditionExpressionNode conditions = ProcessConditions(tokens);

            Debug.WriteLine("CREATED SELECT");
            return new TSqlSelect(entities.ToArray(), columns.ToArray(), conditions);
        }
        private static void CertifyStruture(List<Token> tokens)
        {
            // SELECT (pelo menos 1 token do tipo identificador ou *) FROM
            int fromI = tokens.FindIndex(t => t.Value.ToLower() == "from");

            if(fromI == -1) throw new SelectParserError(10);

            List<Token> tokensInBetween = tokens.GetRange(1, fromI - 1);
            if (tokensInBetween.Count < 1)
            {
                throw new SelectParserError(101);
            }

            int nextKeywordIndex = tokens.Skip(fromI+1).ToList().FindIndex(t => t.Type == "keyword");
            if (nextKeywordIndex != -1)
                tokensInBetween = tokens.GetRange(fromI+1, nextKeywordIndex);
            else
                tokensInBetween = tokens.Skip(fromI + 1).ToList() ;
            if (tokensInBetween.Count < 1)
            {
                throw new SelectParserError(20);
            }

            int whereI = tokens.FindIndex(t => t.Value.ToLower() == "where");
            if (whereI != -1) { 
                nextKeywordIndex = tokens.Skip(whereI + 1).ToList().FindIndex(t => t.Type == "keyword");
                if (nextKeywordIndex != -1)
                    tokensInBetween = tokens.GetRange(whereI + 1, nextKeywordIndex);
                else
                    tokensInBetween = tokens.Skip(whereI + 1).ToList();
                if (tokensInBetween.Count < 1)
                {
                    throw new SelectParserError(51);
                }
            }
        }

        static List<EntityStatementNode> ProcessEntities(List<Token> allTokens) {
            List <EntityStatementNode> entities = new List <EntityStatementNode>();
            int fromI = allTokens.FindIndex(t => t.Value.ToLower() == "from");
            int nextKeywordIndex = allTokens.Skip(fromI+1).ToList().FindIndex(t => t.Type == "keyword");
            List<Token> entityTokens = allTokens.Skip(fromI + 1).ToList();
            if (nextKeywordIndex >= 0) entityTokens = entityTokens.GetRange(0, nextKeywordIndex-1);

            bool expectingSeparator = false;
            foreach (var t in entityTokens)
            {
                if (expectingSeparator)
                {
                    if (t.Value != ",") throw new ParserError(9);
                    expectingSeparator = false;
                }
                else
                {
                    if (t.Type != "identifier") throw new ParserError(2);
                    expectingSeparator = true;
                    entities.Add(new EntityStatementNode(t.Value));
                }
            }

            return entities;
        }

        static List<ColumnStatementNode> ProcessColumns(List<Token> allTokens) {
            List<ColumnStatementNode> columns = new List<ColumnStatementNode>();

            int fromIndex = allTokens.FindIndex(t => t.Value.ToLower() == "from");
            List<Token> columnTokens = allTokens.GetRange(1, fromIndex - 1);

            bool expectingSeparator = false;
            foreach (var t in columnTokens) {
                Debug.WriteLine(t.Value);
                if (expectingSeparator)
                {
                    if (t.Value != ",") throw new ParserError(9);
                    expectingSeparator = false;
                }
                else { 
                    if(t.Type != "identifier" && t.Value != "*") throw new ParserError(2);
                    expectingSeparator = true;
                    columns.Add(new ColumnStatementNode(t.Value));
                }
            }
            return columns;
        }
        static ConditionExpressionNode ProcessConditions(List<Token> allTokens) {
            ConditionExpressionNode root = new ConditionExpressionNode();

            int whereKeywordIndex = allTokens.FindIndex(t => t.Value.ToLower() == "where");
            if (whereKeywordIndex != -1)
            {
                // Remove todos os tokens antes do where
                allTokens = allTokens.Skip(whereKeywordIndex + 1).ToList();
                // Remove todos os tokens da proxima keyword pra frente
                int nextKeyword = allTokens.FindIndex(t => t.Type == "keyword");
                if (nextKeyword >= 0) allTokens = allTokens.GetRange(whereKeywordIndex + 1, nextKeyword);
            }
            else {
                return root;
            }

            // Proximo operador lógico
            List<Token> right = allTokens; //Tokens do lado direto do operador
            int nextLogOp = right.FindIndex(t => Lexer.isLogicalOperator(t.Value)); //Inicia loop, mesmo se não tiver operadores logicos
            ConditionExpressionNode lastOperator = root;

            if (nextLogOp > -1)
            {
                while (nextLogOp != -1)
                {

                    List<Token> left = allTokens;

                    //TEM OPERADOR LOGICO
                    //Cria o operador ex AND
                    ConditionExpressionNode logOp = new ConditionExpressionNode(right[nextLogOp].Value);
                    //LEFT é tudo antes do AND
                    left = right.Take(nextLogOp).ToList();
                    right.RemoveRange(0, left.Count + 1);
                    BuildAritmaticTree(logOp, left);
                    lastOperator.Add(logOp);
                    lastOperator = logOp;

                    nextLogOp = right.FindIndex(t => Lexer.isLogicalOperator(t.Value));
                }

                BuildAritmaticTree(lastOperator, right);
            }
            else
            {
                if (right.Count > 0) BuildAritmaticTree(lastOperator, right);
                else
                {
                    throw new SelectParserError(51);
                }
            }

            return root;
        }

        static void BuildAritmaticTree(ConditionExpressionNode parent, List<Token> tokens)
        {
            Token a;
            List<Token> b = tokens;

            if(tokens.Count < 3)
            {
                throw new SelectParserError(50);
            }

            ConditionExpressionNode lastNode = parent;
            int nextOp = b.FindIndex(t => Lexer.isOperator(t.Value));
            while (nextOp != -1)
            {
                a = b[0];
                Token op = b[nextOp];

                //Debug
                Debug.Write(a.Value + " ");
                foreach (var resto in b)
                {
                    Debug.Write(resto.Value + ",");
                }
                Debug.Write("\n");

                b.RemoveAt(0);
                b.RemoveAt(nextOp - 1);

                ConditionExpressionNode opNode = new ConditionExpressionNode(op.Value);
                lastNode.Add(opNode);
                lastNode = opNode;
                lastNode.Add(new ConditionExpressionNode(a.Value));
                nextOp = b.FindIndex(t => Lexer.isOperator(t.Value));
            }
            if (b.Count == 0) throw new SelectParserError(50);
            lastNode.Add(new ConditionExpressionNode(b[0].Value));
        }
    }
}
