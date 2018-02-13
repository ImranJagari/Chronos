using Chronos.ORM.SubSonic.SQLGeneration.Schema;

namespace Chronos.Server.Databases.Maps
{
    public class MapRecordRelator
    {
        public const string FetchQuery = "SELECT * FROM world_maps";
    }
    [TableName("world_maps")]
    public class MapRecord
    {
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
    }
}
