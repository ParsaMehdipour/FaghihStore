using SH.Application.Interfaces;
using SH.Application.Models;

namespace CD.Infrastructure.EfCore;

public class CountryDivisionManagementPermissionExposer : IPermissionExposer
{
    public Dictionary<string, List<RoleClaimsViewModel>> Expose()
    {
        return new Dictionary<string, List<RoleClaimsViewModel>>
        {
            {
                "CountryDivision", new List<RoleClaimsViewModel>
                {
                    new(){Value = Permissions.COUNTRYDIVISION_FullManagement, Type = "Permission"},
                }
            }
        };
    }

    public static class Permissions
    {
        public const string COUNTRYDIVISION_FullManagement = "Permissions.CountryDivisions.FullManagement";
    }
}
