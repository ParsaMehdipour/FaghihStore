using SH.Application.Models;

namespace SH.Application.Interfaces
{
    public interface IPermissionExposer
    {
        Dictionary<string, List<RoleClaimsViewModel>> Expose();
    }
}