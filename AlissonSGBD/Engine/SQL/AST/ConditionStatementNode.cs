using AlissonSGBD.Engine.SQL.AST;
using System;
using System.Diagnostics;
using System.Xml.Linq;

namespace AlissonSGBD.Engine.SQL.Models
{
    public class ConditionStatementNode : TSqlStatement
    {
        public bool isOperator = true;

        public override void Describe(int tabCount)
        {
            for (int i = 0; i < tabCount; i++)
            {
                Debug.Write("\t");
            }
            Debug.Write(Value + "\n");
            foreach (StatementNode child in Children)
            {
                child.Describe(tabCount + 1);
            }
        }
    }

    public class ConditionStatementNode<T> : ConditionStatementNode
    {
        public new T Value { get; set; }
        public ConditionStatementNode(T Value){
            this.Value = Value;
            isOperator = false;
        }

        public override void Describe(int tabCount)
        {
            for (int i = 0; i < tabCount; i++)
            {
                Debug.Write("\t");
            }
            Debug.Write(Value + "\n");
            for (int i = 0; i < tabCount; i++)
            {
                Debug.Write("\t");
            }
            Debug.Write(typeof(T).Name + "\n");
            foreach (StatementNode child in Children)
            {
                child.Describe(tabCount + 1);
            }
        }
    }


}
