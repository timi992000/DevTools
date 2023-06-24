using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Core.Exceptions;
public class OperationFailedException : Exception
{
    public OperationFailedException(string operationName) : base($"The operation '{operationName}' failed.") { }
}
