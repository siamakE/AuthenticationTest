using System;
using Funq;
using ServiceStack.Auth;
using ServiceStack.Caching;
using ServiceStack.WebHost.IntegrationTests.Services;
using System.Collections.Generic;
using ServiceStack;
using UnitTestDemo.ServiceInterface;
using UnitTestDemo.Tests2;

namespace UnitTestDemo.Tests.AuthTests
{
    public class AuthAppHost
        : AppSelfHostBase
    {
        private readonly string webHostUrl;
        private readonly Action<Container> configureFn;

        public AuthAppHost(string webHostUrl, Action<Container> configureFn = null)
            : base(nameof(IntegrationTest), typeof(MyServices).Assembly)
        {
            this.webHostUrl = webHostUrl;
            this.configureFn = configureFn;
        }

        private InMemoryAuthRepository userRep;

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig { WebHostUrl = webHostUrl, DebugMode = true });

            Plugins.Add(new AuthFeature(() => new WebSudoAuthUserSession(),
                GetAuthProviders(), "~/" + IntegrationTest.LoginUrl)
            {
                AllowGetAuthenticateRequests = req => true,
                RegisterPlugins = { new WebSudoFeature() },
            });

            container.Register(new MemoryCacheClient());
            userRep = new InMemoryAuthRepository();
            container.Register<IAuthRepository>(userRep);

            if (configureFn != null)
            {
                configureFn(container);
            }

            CreateUser(1, IntegrationTest.UserName, null, IntegrationTest.Password, new List<string> { "TheRole" }, new List<string> { "ThePermission" });
            //CreateUser(2, AuthTests.UserNameWithSessionRedirect, null, AuthTests.PasswordForSessionRedirect);
            //CreateUser(3, null, AuthTests.EmailBasedUsername, AuthTests.PasswordForEmailBasedAccount);
        }

        public virtual IAuthProvider[] GetAuthProviders()
        {
            return new IAuthProvider[] { //Www-Authenticate should contain basic auth, therefore register this provider first
                    new BasicAuthProvider(), //Sign-in with Basic Auth
                    //new CredentialsAuthProvider(), //HTML Form post of UserName/Password credentials
                    //new CustomAuthProvider()
                };
        }

        private void CreateUser(int id, string username, string email, string password, List<string> roles = null, List<string> permissions = null)
        {
            new SaltedHash().GetHashAndSaltString(password, out var hash, out var salt);

            userRep.CreateUserAuth(new UserAuth
            {
                Id = id,
                DisplayName = "DisplayName",
                Email = email ?? "as@if{0}.com".Fmt(id),
                UserName = username,
                FirstName = "FirstName",
                LastName = "LastName",
                PasswordHash = hash,
                Salt = salt,
                Roles = roles,
                Permissions = permissions
            }, password);
        }

        protected override void Dispose(bool disposing)
        {
            // Needed so that when the derived class tests run the same users can be added again.
            userRep.Clear();
            base.Dispose(disposing);
        }
    }
}