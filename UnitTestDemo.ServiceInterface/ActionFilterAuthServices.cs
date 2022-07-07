using ServiceStack;
using UnitTestDemo.ServiceModel;

namespace UnitTestDemo.ServiceInterface;

public class ActionFilterAuthServices : Service
{
    [Authenticate]
    public object Any(RequiresAuthAction request) => request;
    [RequiredRole(nameof(RequiresRoleAction))]
    public object Any(RequiresRoleAction request) => request;
    [Authenticate, RequiredRole(nameof(RequiresAuthRoleAction))]
    public object Any(RequiresAuthRoleAction request) => request;
}
