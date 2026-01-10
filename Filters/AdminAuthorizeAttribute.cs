using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebBanGiayTheThao.Filters
{
    public class AdminAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;

            var userId = session.GetInt32("UserId");
            var vaiTro = session.GetInt32("VaiTro");

            // Chưa đăng nhập hoặc không phải admin
            if (userId == null || vaiTro != 1)
            {
                context.Result = new RedirectToActionResult(
                    "TrangChu",
                    "Home",
                    new { area = ""}
                );
            }
        }
    }
}
