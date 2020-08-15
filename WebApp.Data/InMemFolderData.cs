using System.Collections.Generic;
using WebApp.Core;
using System.Linq;

namespace WebApp.Data
{
    public class InMemFolderData : IFolderData
    {
        List<Folder> folders;
        public InMemFolderData()
        {
            folders = new List<Folder>()
            {
                new Folder {Id = "abc"},
                new Folder {Id = "xyz"},
            };
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Folder> GetAll()
        {
            return from f in folders
                   orderby f.Id
                   select f;

        }
    }
}
