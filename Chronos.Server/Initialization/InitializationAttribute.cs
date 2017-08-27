using System;
namespace Chronos.Server.Initialization
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class InitializationAttribute : Attribute
	{
		public Type Dependance
		{
			get;
			set;
		}
		public string Description
		{
			get;
			set;
		}
		public bool Silent
		{
			get;
			set;
		}
		public InitializationPass Pass
		{
			get;
			set;
		}
		public InitializationAttribute(Type dependance)
		{
			this.Dependance = dependance;
		}
		public InitializationAttribute(InitializationPass pass)
		{
			this.Pass = pass;
		}
		public InitializationAttribute(InitializationPass pass, string desciption = "")
		{
			this.Description = desciption;
			this.Pass = pass;
		}
		public InitializationAttribute(Type dependantOf, string desciption = "")
		{
			this.Description = desciption;
			this.Dependance = dependantOf;
		}
	}
}
