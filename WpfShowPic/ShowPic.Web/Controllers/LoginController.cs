using Microsoft.AspNetCore.Mvc;
using ShowPic.Web.Service;
namespace ShowPic.Web.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> logger;

        private readonly ILoginService loginService;

        public LoginController(ILogger<LoginController> logger, ILoginService loginAppService)
        {
            this.logger = logger;
            this.loginService = loginAppService;
        }


        [HttpGet]
        public int? Login(string username, string password)
        {
            return loginService.Login(username, password);
        }
    }

}