using System.Collections.Generic;
using System.Text;
using WebApp.Core;

namespace WebApp.Data
{
    public interface IFyleData
    {
        IEnumerable<Fyle> GetAll();
        IEnumerable<Fyle> GetFilesFromFolder(string folder);
        Fyle GetFileById(int id);
        Fyle AddFile(Fyle fyle);
        int Commit();
        
    }
}
