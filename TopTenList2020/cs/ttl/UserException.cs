using System;
using Xarial.XCad.Exceptions;

namespace ttl
{
    public class UserException : Exception, IUserException
    {
        public UserException(string message) : base(message)
        {
        }
    }
}