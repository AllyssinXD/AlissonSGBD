using AlissonSGBD.Engine.SQL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlissonSGBD.Engine.SQL.AST
{
    class TSqlSelect : TSqlStatement
    {
        public TSqlSelect(EntityStatementNode[] entities, ColumnStatementNode[] columns, ConditionStatementNode conditions)
        {
            StatementNode entityNodes = new StatementNode();
            entityNodes.Value = "FROM";
            entityNodes.Children = new List<StatementNode>(entities);
            Children.Add(entityNodes);

            StatementNode columnNodes = new StatementNode();
            columnNodes.Value = "Columns";
            columnNodes.Children = new List<StatementNode>(columns);
            Children.Add(columnNodes);

            if (conditions != null) { 
                StatementNode conditionsNodes = new StatementNode();
                conditionsNodes.Value = "Conditions";
                conditionsNodes.Children = new List<StatementNode>() { conditions };
                Children.Add(conditionsNodes);
            }

            Value = "SELECT";
        }
    }
}