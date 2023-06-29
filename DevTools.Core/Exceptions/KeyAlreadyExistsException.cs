using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Core.Exceptions;
public class KeyAlreadyExistsException : Exception
{
    public KeyAlreadyExistsException(string keyName) : base($"A key with the name '{keyName}' already exists.") { }
}
