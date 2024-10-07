using ShowPic.Entity;
namespace ShowPic.Web.Service
{
    public class LoginService : ILoginService
    {
        private picturestoreContext _dataContext;

        public LoginService(picturestoreContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public int? Login(string username, string password)
        {
            var item = _dataContext.Pictureeditors.FirstOrDefault(i => i.Name == username && i.Password == password);
            return item?.Id;
        }
    }
}
