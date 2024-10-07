using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowPic.Entity;
namespace ShowPic.Utils.HttpUtils
{
    public class EditorHttpUtil:Httpbase
    {



        public static List<Pictureeditor> GetEditors()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["Id"] = "";

            var str = Get(UrlConfig.EDITOR_GETALL, data);
            var editors = StrToObject<List<Pictureeditor>>(str);
            return editors;
        }

        public static bool DeleteEditor(int Id)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["Id"] = Id.ToString();

            var str = Delete(UrlConfig.EDITOR_DELETE, data);
           return Boolean.Parse(str);
        }
    }
}
