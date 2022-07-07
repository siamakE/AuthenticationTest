using ServiceStack;
using UnitTestDemo.ServiceModel;

namespace UnitTestDemo.ServiceInterface
{
    [RequiredPermission("ThePermission")]
    public class RequiresPermissionService : Service
    {
        public RequiresPermissionResponse Any(RequiresPermission request)
        {
            return new RequiresPermissionResponse { Result = request.Name };
        }
    }
}