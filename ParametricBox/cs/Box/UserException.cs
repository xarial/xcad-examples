using System;
using Xarial.XCad.Exceptions;

namespace Xarial.XCad.Examples.Sw.ParametricBox
{
    public class UserException : Exception, IUserException
    {
        public UserException(string msg) : base(msg)
        {
        }
    }
}
