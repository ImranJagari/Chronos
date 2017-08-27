using Chronos.Core;
using Chronos.Core.Reflection;
using Chronos.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace Chronos.Server.Initialization
{
	public class InitializationManager : Singleton<InitializationManager>
	{
		private readonly List<Type> m_initializedTypes = new List<Type>();
		private readonly Dictionary<Type, List<InitializationMethod>> m_dependances = new Dictionary<Type, List<InitializationMethod>>();
		private readonly Dictionary<InitializationPass, List<InitializationMethod>> m_initializer = new Dictionary<InitializationPass, List<InitializationMethod>>();
		public event Action<string> ProcessInitialization;
		private void OnProcessInitialization(string text)
		{
			Action<string> processInitialization = this.ProcessInitialization;
			if (processInitialization != null)
			{
				processInitialization(text);
			}
		}
		private InitializationManager()
		{
			foreach (InitializationPass key in Enum.GetValues(typeof(InitializationPass)))
			{
				this.m_initializer.Add(key, new List<InitializationMethod>());
			}
		}
		public void AddAssemblies(IEnumerable<Assembly> assemblies)
		{
			foreach (Assembly current in assemblies)
			{
				this.AddAssembly(current);
			}
		}
		public void AddAssembly(Assembly assembly)
		{
			Type[] types = assembly.GetTypes();
			for (int i = 0; i < types.Length; i++)
			{
				Type type = types[i];
				MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				for (int j = 0; j < methods.Length; j++)
				{
					MethodInfo methodInfo = methods[j];
					InitializationAttribute customAttribute = methodInfo.GetCustomAttribute<InitializationAttribute>();
					if (customAttribute != null)
					{
						if (type.IsGenericType)
						{
							throw new Exception("Initialization method is within a generic type.");
						}
						if (methodInfo.IsGenericMethod)
						{
							throw new Exception("Initialization method must not be generic.");
						}
						if (methodInfo.ReturnType != typeof(void))
						{
							throw new Exception("Invalid initialization method return type.");
						}
						if (methodInfo.GetParameters().Length != 0)
						{
							throw new Exception("Invalid initialization cannot have parameters");
						}
						if (!this.m_initializer.ContainsKey(customAttribute.Pass))
						{
							this.m_initializer.Add(customAttribute.Pass, new List<InitializationMethod>());
						}
						InitializationMethod initializationMethod = new InitializationMethod(customAttribute, methodInfo);
						if (methodInfo.IsStatic)
						{
							initializationMethod.Caller = null;
						}
						else
						{
							if (!type.IsDerivedFromGenericType(typeof(Singleton<>)))
							{
								throw new Exception("Method have to be static or class must inherit Singleton<>");
							}
							PropertyInfo property = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
							initializationMethod.Caller = property.GetValue(null, new object[0]);
						}
						this.m_initializer[customAttribute.Pass].Add(initializationMethod);
					}
				}
			}
		}
		private void ExecuteInitializationMethod(InitializationMethod method)
		{
			if (!method.Initialized)
			{
				if (method.Attribute.Dependance != null && !this.m_initializedTypes.Contains(method.Attribute.Dependance))
				{
					if (!this.m_dependances.ContainsKey(method.Attribute.Dependance))
					{
						this.m_dependances.Add(method.Attribute.Dependance, new List<InitializationMethod>());
					}
					this.m_dependances[method.Attribute.Dependance].Add(method);
				}
				else
				{
					if (!method.Attribute.Silent && !string.IsNullOrEmpty(method.Attribute.Description))
					{
						ConsoleUtils.WriteMessageDB(method.Attribute.Description);
						this.OnProcessInitialization(method.Attribute.Description);
					}
					else
					{
						if (!method.Attribute.Silent)
						{
							string text = string.Format("Initialize '{0}'", method.Method.DeclaringType.Name);
							ConsoleUtils.WriteMessageDB(text);
							this.OnProcessInitialization(text);
						}
					}
					method.Method.Invoke(method.Caller, new object[0]);
					method.Initialized = true;
					if (!this.m_initializedTypes.Contains(method.Method.DeclaringType))
					{
						this.m_initializedTypes.Add(method.Method.DeclaringType);
					}
					if (this.m_dependances.ContainsKey(method.Method.DeclaringType))
					{
						foreach (InitializationMethod current in this.m_dependances[method.Method.DeclaringType])
						{
							this.ExecuteInitializationMethod(current);
						}
						this.m_dependances.Remove(method.Method.DeclaringType);
					}
				}
			}
		}
		public void InitializeAll()
		{
			foreach (InitializationPass current in 
				from InitializationPass pass in Enum.GetValues(typeof(InitializationPass))
				where pass != InitializationPass.Database
				select pass)
			{
				this.Initialize(current);
			}
		}
		public void Initialize(InitializationPass pass)
		{
			foreach (InitializationMethod current in this.m_initializer[pass])
			{
				this.ExecuteInitializationMethod(current);
			}
			this.m_initializer[pass].Clear();
		}
	}
}
