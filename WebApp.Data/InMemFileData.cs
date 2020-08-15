using System.Collections.Generic;
using System.Linq;
using WebApp.Core;

namespace WebApp.Data
{
    public class InMemFileData : IFyleData
    {
        List<Fyle> fyles;

        public InMemFileData()
        {
            fyles = new List<Fyle>()
            {
                new Fyle {Id=1, Folder="abc", Name = "1.txt"},
                new Fyle {Id=2, Folder="abc", Name = "2.txt"},
                new Fyle {Id=3, Folder="xyz", Name = "3.txt"},
                new Fyle {Id=4, Folder="xyz", Name = "4.txt"},
                new Fyle {Id=5, Folder="abc", Name = "win64.iso"}

            };
        }

        public Fyle AddFile(Fyle fyle)
        {
            fyles.Add(fyle);
            return fyle;
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Fyle> GetAll()
        {
            return from f in fyles
                   orderby f.Id
                   select f;
        }

        public Fyle GetFileById(int id)
        {
            return fyles.SingleOrDefault(f => f.Id == id);
        }

        public IEnumerable<Fyle> GetFilesFromFolder(string folder)
        {
            return from f in fyles
                   where f.Folder == folder
                   select f;
        }
    }
}
