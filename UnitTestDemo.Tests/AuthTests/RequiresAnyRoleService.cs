using ServiceStack;
using UnitTestDemo.ServiceModel;

namespace UnitTestDemo.Tests.AuthTests
{
    [RequiresAnyRole("TheRole", "TheRole2")]
    public class RequiresAnyRoleService : Service
    {
        public object Any(RequiresAnyRole request)
        {
            return new RequiresAnyRoleResponse { Result = request.Roles };
        }
    }
}