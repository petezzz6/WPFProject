using ShowPic.Entity;
using ShowPic.Utils;

namespace ShowPic.Web.Service
{
    public interface IPictureService
    {
        public Pictureentity GetPicture(string id);

        public bool AddPicture(Pictureentity picture);

        public Page<Pictureentity> GetPictures(int pageNum, int pageSize, string? no = null, string? tag = null);

        public bool DeletePicture(int id);

        public bool UpdateComment(int Id,string comment);

        public bool RatePicture(int Id, sbyte? Rate);
    }
}
