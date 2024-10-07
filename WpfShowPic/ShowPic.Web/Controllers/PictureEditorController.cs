using ShowPic.Web.Service;
using ShowPic.Entity;
using Microsoft.AspNetCore.Mvc;
namespace ShowPic.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PictureEditorController : ControllerBase
    {

        IEditorService _editorService;
        public PictureEditorController(IEditorService editorService)
        {
            this._editorService = editorService;
        }

        [HttpPost]
        public int AddEditor(Pictureeditor pictureeditor)
        {
            return _editorService.AddEditor(pictureeditor);
        }

        [HttpGet]
        public List<Pictureeditor>? GetEditors()
        {
            return _editorService.GetEditors();
        }

        [HttpDelete]
        public bool DeleteEditor(int Id)
        {
          return _editorService.DeleteEditor(Id);

        }
    }
}
