using ServiceStack;
using UnitTestDemo.ServiceModel;

namespace UnitTestDemo.ServiceInterface
{
    [Authenticate(Provider = "custom")]
    public class RequiresCustomAuthService : Service
    {
        public RequiresCustomAuthResponse Any(RequiresCustomAuth request)
        {
            return new RequiresCustomAuthResponse { Result = request.Name };
        }
    }
}