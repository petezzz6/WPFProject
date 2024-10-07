using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowPic.Entity;
namespace ShowPic.Utils.HttpUtils
{
    public class LoginHttpUtil:Httpbase
    {
        public static int Login(string username, string password)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["username"] = username;
            data["password"] = password;
            var str = Get(UrlConfig.LOGIN_LOGIN, data);
            if (!string.IsNullOrEmpty(str))
            {
                return int.Parse(str);
            }
            return 0;
        }


        public static int Register(Pictureeditor pictureeditor)
        {
            var str = Post(UrlConfig.LOGIN_REGISTER, pictureeditor);
            if (!string.IsNullOrEmpty(str))
            {
                return int.Parse(str);
            }
            return 0;
        }
    }
}
