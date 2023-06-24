using DevTools.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Core.OperationResult
{
	[Serializable]
	public class DevOperationResult<T> : IDevOperationResult
	{
		public DevOperationResult([CallerMemberName]string operationName = "")
		{
			Messages = new List<string>();
			Succeeded = false;
			OperationResult = default;
			OperationName = operationName;
		}

		[DataMember]
		public bool Succeeded { get; set; }

		[DataMember]
		public T OperationResult { get; set; }

		[DataMember]
		public List<string> Messages { get; set; }

		[DataMember]
		public string OperationName { get; private set; }


		public static DevOperationResult<T> Ok(T operationResult, [CallerMemberName]string operationName = "")
		{
			return new DevOperationResult<T>(operationName)
			{
				Succeeded = true,
				OperationResult = operationResult,
				Messages = new List<string>(),
			};
		}

		public static DevOperationResult<T> Fail(string message, [CallerMemberName]string operationName = "")
				=> Fail(new List<string> { message }, operationName);

		public static DevOperationResult<T> Fail(List<string> messages, [CallerMemberName]string operationName = "")
		{
			return new DevOperationResult<T>(operationName)
			{
				Succeeded = false,
				Messages = messages,
			};
		}

		
	}
}
