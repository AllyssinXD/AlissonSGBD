using System;
using System.Diagnostics;

namespace AlissonSGBD.Engine.SQL.AST
{
    class ColumnStatementNode : StatementNode
    {
        public string Name { get; set; }
        public string Entity { get; set; }

        public ColumnStatementNode(string name)
        {
            Name = name;
            Debug.WriteLine("CREATED ENTITY NODE WITH " + Name);
        }
        public ColumnStatementNode(string name, string entity)
        {
            Name = name;
            Entity = entity;
        }

        public override void Describe(int tabCount)
        {
            for (int i = 0; i < tabCount; i++)
            {
                Debug.Write("\t");
            }
            Debug.Write(Name + "\n");
            foreach (StatementNode child in Children)
            {
                child.Describe(tabCount + 1);
            }
        }
    }
}
