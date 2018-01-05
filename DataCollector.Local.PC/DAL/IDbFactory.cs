using NPoco;

namespace DataCollector.Local.PC.DAL
{
    public interface IDbFactory
    {
        IDatabase CreateDatabase();
    }
}