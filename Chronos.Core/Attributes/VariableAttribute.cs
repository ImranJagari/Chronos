
using System;

namespace Chronos.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class VariableAttribute : Attribute
    {
        public VariableAttribute()
        {
            Priority = 1;
        }

        public VariableAttribute(bool definableByConfig = false)
        {
            DefinableRunning = definableByConfig;
            Priority = 1;
        }

        ///<summary>
        ///  Sets a value indicating whether this variable can be set when server is running
        ///</summary>
        ///<value><c>true</c> if this variable can be set when server is running; otherwise, <c>false</c>.</value>
        public bool DefinableRunning
        {
            get;
            set;
        }

        public int Priority
        {
            get;
            set;
        }
    }
}