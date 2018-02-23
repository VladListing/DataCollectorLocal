using System;
using System.Collections.Generic;

namespace DataCollector.Local.PC.DAL
{
    public interface IRepository
    {
        void AddRecord(DiskStateRecord record);

        ICollection<DiskStateRecord> GetUnarchivedRecords();

        void MarkRecordsAsArchived(ICollection<DiskStateRecord> records);
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

        public ICollection<DiskStateRecord> GetUnarchivedRecords()
        {
            using (var database = _dbFactory.CreateDatabase())
            {
                return database
                    .Query<DiskStateRecord>()
                    .Where(record => !record.IsArchived)
                    .ToList();
            }
        }

        public void MarkRecordsAsArchived(ICollection<DiskStateRecord> records)
        {
            using (var db = _dbFactory.CreateDatabase())
            {
                foreach (var record in records)
                {
                    record.IsArchived = true;
                    db.Update(record);
                }
            }
        }
    }
}