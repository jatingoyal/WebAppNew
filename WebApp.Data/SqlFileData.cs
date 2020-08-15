using System;
using System.Collections.Generic;
using WebApp.Core;
using System.Linq;

namespace WebApp.Data
{
    public class SqlFileData : IFyleData
    {
        private readonly WebAppDbContext db;

        public SqlFileData(WebAppDbContext db)
        {
            this.db = db;
        }

        public Fyle AddFile(Fyle fyle)
        {
            var query = from f in db.Files
                        where f.Name == fyle.Name
                        select f;
            if(query.Count<Fyle>() < 1)
                db.Add(fyle);
            return fyle;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public IEnumerable<Fyle> GetAll()
        {
            var query = from f in db.Files
                        orderby f.Id
                        select f;
            return query;
        }

        public Fyle GetFileById(int id)
        {
            return db.Files.Find(id);
        }

        public IEnumerable<Fyle> GetFilesFromFolder(string folder)
        {
            var query = from f in db.Files
                        where f.Folder == folder
                        select f;
            return query;
        }
    }
}
