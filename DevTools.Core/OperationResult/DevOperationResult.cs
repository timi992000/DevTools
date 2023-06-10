using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Core.OperationResult
{
	[Serializable]
	public class DevOperationResult<T> : IDevOperationResult
	{
		public DevOperationResult()
		{
			Messages = new List<string>();
			Succeeded = false;
			OperationResult = default;
		}

		[DataMember]
		public bool Succeeded { get; set; }

		[DataMember]
		public T OperationResult { get; set; }

		[DataMember]
		public List<string> Messages { get; set; }


		public static DevOperationResult<T> Ok(T operationResult)
		{
			return new DevOperationResult<T>
			{
				Succeeded = true,
				OperationResult = operationResult,
				Messages = new List<string>(),
			};
		}

		public static DevOperationResult<T> Fail(string message)
				=> Fail(new List<string> { message });

		public static DevOperationResult<T> Fail(List<string> messages)
		{
			return new DevOperationResult<T>
			{
				Succeeded = false,
				Messages = messages,
			};
		}
	}
}
