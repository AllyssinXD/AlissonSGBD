using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AlissonSGBD.Engine.SQL.AST
{
    public class StatementNode
    {

        public string Value { get; set; }
        public List<StatementNode> Children { get; set; }
        public StatementNode() {
            Children = new List<StatementNode>();
        }

        public void AddChild(StatementNode child) {
           Children.Add(child);
        }

        public virtual void Describe(int tabCount) {
            for (int i = 0; i < tabCount; i++)
            {
                Debug.Write("\t");
            }
            Debug.Write(Value + "\n");
            foreach (StatementNode child in Children)
            {
                child.Describe(tabCount+1);
            }
        }
    }
}
