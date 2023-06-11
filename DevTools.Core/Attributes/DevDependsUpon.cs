using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Core.Attributes
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
	public class DevDependsUpon : Attribute
	{
		public string MemberName;
		public DevDependsUpon(string memberName)
		{
			MemberName = memberName;
		}
	}
}
