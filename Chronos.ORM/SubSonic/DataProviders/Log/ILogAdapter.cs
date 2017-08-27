using System;

namespace Chronos.ORM.SubSonic.DataProviders.Log
{
    public interface ILogAdapter
    {
        void Log(String message);
    }
}