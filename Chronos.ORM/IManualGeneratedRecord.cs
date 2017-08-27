using Chronos.ORM.SubSonic.DataProviders;
using Chronos.ORM.SubSonic.Schema;

namespace Chronos.ORM
{
    public interface IManualGeneratedRecord
    {
        ITable GetTableInformation(IDataProvider provider); 
    }
}