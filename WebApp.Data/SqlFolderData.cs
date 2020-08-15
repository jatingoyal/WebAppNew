using System;
using System.Collections.Generic;
using WebApp.Core;
using System.Linq;

namespace WebApp.Data
{
    public class SqlFolderData : IFolderData
    {
        private readonly WebAppDbContext db;

        public SqlFolderData(WebAppDbContext db)
        {
            this.db = db;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public IEnumerable<Folder> GetAll()
        {
            var query = from f in db.Folders
                        orderby f.Id
                        select f;
            return query;
        }
    }
}
