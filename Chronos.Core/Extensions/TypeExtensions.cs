using System;

namespace Chronos.Core.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSubclassOfGeneric(this Type type, Type genericType)
        {
            Type baseType = type.BaseType;
            bool result;
            while (baseType != null && !baseType.IsValueType)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == genericType)
                {
                    result = true;
                    return result;
                }
                baseType = baseType.BaseType;
            }
            result = false;
            return result;
        }
    }
}