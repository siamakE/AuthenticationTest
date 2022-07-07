using System;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Auth;
using ServiceStack;

namespace UnitTestDemo.Tests.AuthTests
{
    public class CustomAuthProvider : AuthProvider
    {
        public CustomAuthProvider()
        {
            Provider = "custom";
        }

        public override bool IsAuthorized(IAuthSession session, IAuthTokens tokens, Authenticate request = null)
        {
            return false;
        }

        public override Task<object> AuthenticateAsync(IServiceBase authService, IAuthSession session, Authenticate request,
            CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}