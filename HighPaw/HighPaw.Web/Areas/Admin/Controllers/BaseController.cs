namespace HighPaw.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static AdminConstants;

    [Authorize(Roles = AdminRoleName)]
    [Area("Admin")]
    public class BaseController : Controller
    {
    }
}
