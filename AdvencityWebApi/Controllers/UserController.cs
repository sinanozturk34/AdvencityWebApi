using AdvencityWebApi.Model;
using AdvencityWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvencityWebApi.Controllers
{
    public class UserController : Controller
    {
        UserServices userServices = new UserServices();
        private readonly IConfiguration configuration;
         public UserController(IConfiguration conf)
        {
            this.configuration = conf;
            DBUtil.DBUtil.conf = conf;
        }
        [Route("userLogin")]
        [HttpPost]
        public ActionResult<ResponseModel> userLogin([FromBody] UserModel user)
        {
            return userServices.userLogin(user);
        }
        [Route("resetPassword")]
        [HttpPost]
        public ActionResult<ResponseModel> resetPassword([FromBody] UserModel user)
        {
            return userServices.resetPassword(user);
        }
    }
}
