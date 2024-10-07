using ShowPic.Entity;
namespace ShowPic.Web.Service
{
    public interface IEditorService
    {

        public int AddEditor(Pictureeditor editor);

        //public int GetEditor(Pictureeditor editor);

        public List<Pictureeditor> GetEditors();

        public bool DeleteEditor(int Id);
    }
}
