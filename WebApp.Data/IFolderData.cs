using System.Collections.Generic;
using System.Text;
using WebApp.Core;

namespace WebApp.Data
{
    public interface IFolderData
    {
        IEnumerable<Folder> GetAll();
        int Commit();
    }
}
