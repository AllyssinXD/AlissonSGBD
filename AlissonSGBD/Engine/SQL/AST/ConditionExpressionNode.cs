using AlissonSGBD.Engine.SQL.AST;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

namespace AlissonSGBD.Engine.SQL.Models
{
    public class ConditionExpressionNode : StatementNode
    {
        public string Type { get; set; }
        public new List<ConditionExpressionNode> Children;

        public ConditionExpressionNode() {
            Value = null;
            Children = new List<ConditionExpressionNode>();
        }
        public ConditionExpressionNode(string value)
        {
            Value = value;
            Children = new List<ConditionExpressionNode>();
        }
        public ConditionExpressionNode(string value, List<ConditionExpressionNode> children)
        {
            Value = value;
            if (children.Count > 2) return;
            Children = children;
        }


        public override void Describe(int tabCount)
        {
            for (int i = 0; i < tabCount; i++)
            {
                Debug.Write("\t");
            }
            Debug.Write(Value + "\n");
            foreach (ConditionExpressionNode child in Children)
            {
                child.Describe(tabCount + 1);
            }
        }

        public string Resolve()
        {
            if (Children.Count == 0) return Value;
            string a = Children[0].Resolve();
            string b = Children[1].Resolve();
            return null;
        }

        public void Add(ConditionExpressionNode child) {
            if (Children.Count > 2) return;
            Children.Add(child);
        }
    }
}
