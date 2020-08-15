using System;
using WebApp.Core;
using System.Linq;

namespace WebApp.Data
{
    public class SqlPataData : IPathData
    {
        private readonly WebAppDbContext db;

        public SqlPataData(WebAppDbContext db)
        {
            this.db = db;
        }

        public BasePath GetBasePath()
        {
            var query = from f in db.BasePath
                   select f;
            return query.ToList()[0];
        }
    }
}
