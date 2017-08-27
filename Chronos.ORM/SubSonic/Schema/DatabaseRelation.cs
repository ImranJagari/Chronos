using System;

namespace Chronos.ORM.SubSonic.Schema
{
    public class DatabaseRelation : IRelation
    {
        public ITable Table { get; private set; }
        public string Name { get; private set; }
        
        public IColumn JoinKey { get; set; }

        public ITable TargetTable { get; set; }
        public Type TargetType { get; set; }
        public IColumn TargetJoinKey { get; set; }

        public Qualifier Qualifier { get; set; }

        public DatabaseRelation(string name, ITable table)
        {
            Table = table;
            Name = name;
        }
    }
}
