using ServiceStack;
using UnitTestDemo.ServiceModel;

namespace UnitTestDemo.ServiceInterface
{
    [RequiredRole("TheRole")]
    public class RequiresRoleService : Service
    {
        public object Any(RequiresRole request)
        {
            return new RequiresRoleResponse { Result = request.Name };
        }
    }
}