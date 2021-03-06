namespace HighPaw.Web.Infrastructure.Extensions
{
    using System.Security.Claims;

    using static Areas.Admin.AdminConstants;
    using static HighPaw.Services.GlobalConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static string FullName(this ClaimsPrincipal user)
            => user.Identity.Name;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdminRoleName);

        public static bool IsVolunteer(this ClaimsPrincipal user)
            => user.IsInRole(VolunteerRoleName);
    }
}
