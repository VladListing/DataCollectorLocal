using System;

namespace DataCollector.Local.PC.DAL
{
    public interface IRepository
    {
        void AddRecord(DiskStateRecord record);
    }

    class Repository : IRepository
    {
        private readonly IDbFactory _dbFactory;

        public Repository(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void AddRecord(DiskStateRecord record)
        {
            using (var db = _dbFactory.CreateDatabase())
            {
                db.Insert(record);
            }
        }
    }
}