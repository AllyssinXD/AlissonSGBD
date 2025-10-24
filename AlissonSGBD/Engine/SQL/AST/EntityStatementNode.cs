using System;
using System.Diagnostics;

namespace AlissonSGBD.Engine.SQL.AST
{
    class EntityStatementNode : StatementNode
    {
        public string Name { get; set; }
        public string Alias { get; set; }

        public EntityStatementNode(string Name) { 
            this.Name = Name;
            Debug.WriteLine("CREATED ENTITY NODE WITH " + Name);
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
