using System;

namespace Orion.Service.Shared.Exceptions
{
    internal class AuthenticateRequiredException : Exception
    {
        public AuthenticateRequiredException() : base("Authenticate required.") { }
    }
}