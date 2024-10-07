using Microsoft.AspNetCore.Mvc;
using ShowPic.Utils;
using ShowPic.Web.Service;
namespace ShowPic.Web.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController( ILoginService loginAppService)
        {
            this.loginService = loginAppService;
        }


        [HttpGet]
        public int? Login(string username, string password)
        {
            LoggerHelper.loggerHelper.Trace("  Login controller");
            return loginService.Login(username, password);
        }
    }

}