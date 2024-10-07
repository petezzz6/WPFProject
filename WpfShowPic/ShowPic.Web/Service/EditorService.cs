using ShowPic.Entity;
using ShowPic.Web;

namespace ShowPic.Web.Service
{
    public class EditorService:IEditorService
    {

        private picturestoreContext ?_context;

        public EditorService(picturestoreContext dataContext)
        {
            this._context = dataContext;
        }

        public int AddEditor(Pictureeditor editor)
        {
           var res= this._context.Pictureeditors.Where(r => r.Name == editor.Name);
            if (res.Any())
            {
                return 1;
            }
            this._context.Pictureeditors.Add(editor);
            this._context.SaveChanges();
            return 0;
        }

        //public int GetEditor(Pictureeditor editor)
        //{
        //    return 0;
        //}

        public List<Pictureeditor> GetEditors()
        {
            IQueryable<Pictureeditor> editors = null;
            editors=  this._context.Pictureeditors.Where(r => true).OrderBy(r => r.Id);
            return editors.ToList();
        }

       public bool DeleteEditor(int Id)
        {
            Pictureeditor pictureeditor = null;
            pictureeditor = this._context.Pictureeditors.FirstOrDefault(r => r.Id==Id);
            if (pictureeditor!=null)
            {
                this._context.Pictureeditors.Remove(pictureeditor);
                this._context.SaveChanges();
            }
            return true;
        }
    }
}
