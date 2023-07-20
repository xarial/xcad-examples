using System;
using Xarial.XCad.Exceptions;

namespace Xarial.XCad.Examples
{
    public class UserException : Exception, IUserException 
    {
        public UserException(string message) : base(message) 
        {
        }
    }
}
