using ShowPic.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowPic.Utils.HttpUtils
{
    public  class PictureHttp:Httpbase
    {

        public static bool AddSPicutre(Pictureentity picture)
        {
            var res = Post<Pictureentity>(UrlConfig.PICTURE_ADDPICTURES, picture);

            return Boolean.Parse(res);
        }

        public static Page<Pictureentity> GetPictures(string no,string tag,int pageNum,int pageSize)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["no"] = no;
            data["tag"] = tag;
            data["pageNum"] = pageNum;
            data["pageSize"] = pageSize;
            var str = Get(UrlConfig.PICTURE_GETPICTURES, data);
            var pictures = StrToObject<Page<Pictureentity>>(str);
            return pictures;
        }

       public static bool DeletePicture(int Id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["Id"] = Id.ToString();
            var res = Delete(UrlConfig.PICTURE_DELETE, data);
            return Boolean.Parse(res);
        }

        public static bool UpdateComment(int Id, string comment)
        {
            var res = Put(UrlConfig.PICTURE_COMMENT,new Pictureentity() { PicId = Id,PicDescription = comment});
            return Boolean.Parse(res);
        }


        public static Pictureentity GetPicture(string id)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Id"] = id;
            var str = Get(UrlConfig.PICTURE_GETPICTURE, data);
            var picture = StrToObject<Pictureentity>(str);
            return picture;
        }

        public static bool RatePicture(int Id,int Rate)
        {
            var str = Put(UrlConfig.PICTURE_RATE, new Pictureentity() { PicId = Id, PicRate = (sbyte?)Rate });
            return Boolean.Parse(str);
        }

    }
}
