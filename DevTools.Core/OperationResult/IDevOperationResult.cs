using System.Collections.Generic;

namespace DevTools.Core.OperationResult
{
	public interface IDevOperationResult
	{
		List<string> Messages { get; set; }
		bool Succeeded { get; set; }
	}
}