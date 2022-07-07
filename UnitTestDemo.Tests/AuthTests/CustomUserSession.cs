using ServiceStack.Auth;
using System.Collections.Generic;
using ServiceStack;

namespace UnitTestDemo.Tests.AuthTests
{
    public class CustomUserSession : WebSudoAuthUserSession
    {
        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            
        }

        public int Counter { get; set; }
    }
}