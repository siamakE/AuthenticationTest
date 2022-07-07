using System;
using ServiceStack;
using UnitTestDemo.ServiceModel;

namespace UnitTestDemo.ServiceInterface
{
    [Authenticate]
    public class SecuredService : Service
    {
        public object Post(Secured request)
        {
            return new SecuredResponse { Result = request.Name };
        }
    }
}