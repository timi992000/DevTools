using DevTools.Core.Exceptions;
using DevTools.Core.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Core.Extender;
public static class OperationResultExtender
{
    public static T ValidateAndRetrieveResult<T>(this DevOperationResult<T> result)
    {
        if (!result.Succeeded)
            throw new OperationFailedException(result.OperationName);
        return result.OperationResult;
    }
}
