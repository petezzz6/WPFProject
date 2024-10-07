using ShowPic.Entity;
using ShowPic.Utils;
namespace ShowPic.Web.Service
{
    public class PictureService : IPictureService
    {
        private picturestoreContext? _dataContext;


        public PictureService(picturestoreContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public int AddPicture(Pictureentity picture)
        {
            _dataContext.Pictureentities.Add(picture);
            this._dataContext.SaveChanges();
            LoggerHelper.loggerHelper.Trace(" AddPicture success");

            return 0;
        }

        public Pictureentity GetPicture(string Id)
        {
            Pictureentity p = new Pictureentity();
            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    int PicId = int.Parse(Id);
                    p = _dataContext.Pictureentities.FirstOrDefault(r => r.PicId == PicId);
                }
                catch (Exception ex)
                {
                    p = _dataContext.Pictureentities.FirstOrDefault(r => r.PicTag == Id);
                }
            }
            LoggerHelper.loggerHelper.Trace(" DeleteEditor success");

            return p;
        }

        public bool DeletePicture(int Id)
        {
            Pictureentity p = _dataContext.Pictureentities.FirstOrDefault(r => r.PicId == Id);
            if (p != null)
            {
                _dataContext.Remove(p);
                _dataContext.SaveChanges();
            }
            return true;
        }

        public Page<Pictureentity> GetPictures(int pageNum, int pageSize, string? no = null, string? tag = null)
        {
            IQueryable<Pictureentity> pictures = null;

            pictures = _dataContext.Pictureentities.Where(r => true).OrderBy(r => r.PicId);

            int count = pictures.Count();
            List<Pictureentity> items;
            if (pageSize > 0)
            {
                items = pictures.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                items = pictures.ToList();
            }
            LoggerHelper.loggerHelper.Trace(" GetPictures success");

            return new Page<Pictureentity>()
            {
                count = count,
                items = items
            };
        }

        public bool UpdateComment(int Id, string comment)
        {
            Pictureentity p = _dataContext.Pictureentities.FirstOrDefault(r => r.PicId == Id);

            if (p != null)
            {
                p.PicDescription = comment;
                _dataContext.SaveChanges();
            }
            return true;
        }

        public bool RatePicture(int Id, sbyte? Rate)
        {
            Pictureentity p = _dataContext.Pictureentities.FirstOrDefault(r => r.PicId == Id);
            if (p != null)
            {
                p.PicRate = Rate;
                _dataContext.SaveChanges();
            }
            return true;
        }
    }
}
