using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowPic.Utils
{
    public class UserInfo
    {
        private static UserInfo instance;

        public static UserInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserInfo();
                }
                return instance;
            }
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Roles { get; set; }

    }
}
