using ShowPic.Web.Service;
using ShowPic.Entity;
using Microsoft.AspNetCore.Mvc;
using ShowPic.Utils;

namespace ShowPic.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PictureController:ControllerBase
    {

        IPictureService _pictureService;
        public PictureController(IPictureService pictureService)
        {
            this._pictureService = pictureService;
        }


        [HttpPost]
        public bool AddPicture(Pictureentity Picture)
        {
            return _pictureService.AddPicture(Picture);
        }

        [HttpGet]
        public Page<Pictureentity> GetPictures(int pageNum, int pageSize, string? no = null, string? tag = null)
        {
            return _pictureService.GetPictures(pageNum,pageSize,no,tag);
        }

        [HttpDelete]
        public bool DeletePicture(string Id)
        {
            return _pictureService.DeletePicture(int.Parse(Id));
        }
        
        [HttpPut]
        public bool UpdateComment(Pictureentity Picture)
        {
            return _pictureService.UpdateComment( Picture.PicId,Picture.PicDescription);
        }

        [HttpGet]
        public Pictureentity GetPicture(string Id)
        {
            return _pictureService.GetPicture(Id);
        }

        [HttpPut]
        public bool RatePicture(Pictureentity Picture)
        {
            return _pictureService.RatePicture(Picture.PicId, Picture.PicRate);
        }
    } 
}
