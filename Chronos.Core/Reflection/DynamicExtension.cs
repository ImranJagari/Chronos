using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Chronos.Core.Reflection
{
    public static class DynamicExtension
    {
        public static T CreateDelegate<T>(this ConstructorInfo ctor)
        {
            List<ParameterExpression> list = (
                from param in ctor.GetParameters()
                select Expression.Parameter(param.ParameterType)).ToList<ParameterExpression>();
            Expression<T> expression = Expression.Lambda<T>(Expression.New(ctor, list), list);
            return expression.Compile();
        }

        public static Delegate CreateDelegate(this ConstructorInfo ctor)
        {
            List<ParameterExpression> list = (
                from param in ctor.GetParameters()
                select Expression.Parameter(param.ParameterType)).ToList<ParameterExpression>();
            LambdaExpression lambdaExpression = Expression.Lambda(Expression.New(ctor, list), list);
            return lambdaExpression.Compile();
        }

        public static Delegate CreateDelegate(this MethodInfo method, params Type[] delegParams)
        {
            Type[] array = (
                from p in method.GetParameters()
                select p.ParameterType).ToArray<Type>();
            if (delegParams.Length != array.Length)
            {
                throw new Exception("Method parameters count != delegParams.Length");
            }
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, null, new Type[]
			{
				typeof(object)
			}.Concat(delegParams).ToArray<Type>(), true);
            ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
            if (!method.IsStatic)
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(method.DeclaringType.IsClass ? OpCodes.Castclass : OpCodes.Unbox, method.DeclaringType);
            }
            for (int i = 0; i < delegParams.Length; i++)
            {
                iLGenerator.Emit(OpCodes.Ldarg, i + 1);
                if (delegParams[i] != array[i])
                {
                    if (!array[i].IsSubclassOf(delegParams[i]) && !array[i].HasInterface(delegParams[i]))
                    {
                        throw new Exception(string.Format("Cannot cast {0} to {1}", array[i].Name, delegParams[i].Name));
                    }
                    iLGenerator.Emit(array[i].IsClass ? OpCodes.Castclass : OpCodes.Unbox, array[i]);
                }
            }
            iLGenerator.Emit(OpCodes.Call, method);
            iLGenerator.Emit(OpCodes.Ret);
            return dynamicMethod.CreateDelegate(Expression.GetActionType(new Type[]
			{
				typeof(object)
			}.Concat(delegParams).ToArray<Type>()));
        }

        public static Delegate CreateFuncDelegate(this MethodInfo method, Type returnType, params Type[] delegParams)
        {
            Type[] array = (
                from p in method.GetParameters()
                select p.ParameterType).ToArray<Type>();
            if (delegParams.Length != array.Length)
            {
                throw new Exception("Method parameters count != delegParams.Length");
            }
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, returnType, new Type[]
			{
				typeof(object)
			}.Concat(delegParams).ToArray<Type>(), true);
            ILGenerator iLGenerator = dynamicMethod.GetILGenerator();
            if (!method.IsStatic)
            {
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(method.DeclaringType.IsClass ? OpCodes.Castclass : OpCodes.Unbox, method.DeclaringType);
            }
            for (int i = 0; i < delegParams.Length; i++)
            {
                iLGenerator.Emit(OpCodes.Ldarg, i + 1);
                if (delegParams[i] != array[i])
                {
                    if (!array[i].IsSubclassOf(delegParams[i]) && !array[i].HasInterface(delegParams[i]))
                    {
                        throw new Exception(string.Format("Cannot cast {0} to {1}", delegParams[i].Name, array[i].Name));
                    }
                    iLGenerator.Emit(array[i].IsClass ? OpCodes.Castclass : OpCodes.Unbox, array[i]);
                }
            }
            iLGenerator.Emit(OpCodes.Call, method);
            if (returnType != null && returnType != method.ReturnType)
            {
                if (!method.ReturnType.IsSubclassOf(returnType) && !method.ReturnType.HasInterface(returnType))
                {
                    throw new Exception(string.Format("Cannot cast {0} to {1}", method.ReturnType.Name, returnType));
                }
                if (method.ReturnType.IsClass && returnType.IsClass)
                {
                    iLGenerator.Emit(OpCodes.Castclass, returnType);
                }
                else
                {
                    if (returnType == typeof(object))
                    {
                        iLGenerator.Emit(OpCodes.Box, method.ReturnType);
                    }
                    else
                    {
                        if (method.ReturnType.IsClass)
                        {
                            iLGenerator.Emit(OpCodes.Unbox, returnType);
                        }
                    }
                }
            }
            iLGenerator.Emit(OpCodes.Ret);
            return dynamicMethod.CreateDelegate(Expression.GetFuncType(new Type[]
			{
				typeof(object)
			}.Concat(delegParams).Concat(new Type[]
			{
				returnType
			}).ToArray<Type>()));
        }
    }
}