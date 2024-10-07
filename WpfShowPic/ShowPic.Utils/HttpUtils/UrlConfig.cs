using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowPic.Utils.HttpUtils
{
    public static class UrlConfig
    {

        //picture 功能
        public static string   PICTURE_GETPICTURES = "api/Picture/GetPictures";

        public static string   PICTURE_ADDPICTURES = "api/Picture/AddPicture";

        public static string   PICTURE_GETPICTURE = "api/Picture/GetPicture";

        public static string   PICTURE_COMMENT = "api/Picture/UpdateComment";

        public static string   PICTURE_DELETE = "api/Picture/DeletePicture";

        public static string   PICTURE_RATE = "api/Picture/RatePicture";

        //login界面

        public static string LOGIN_LOGIN = "api/Login/Login";

        public static string LOGIN_REGISTER = "api/PictureEditor/AddEditor";

        //用户
        public static string EDITOR_GETALL = "api/PictureEditor/GetEditors";

        public static string EDITOR_DELETE = "api/PictureEditor/DeleteEditor";

    }
}
