using System.Data;

namespace DAL.Helper
{
  public class BaseRepository
  {
    protected readonly Command _command = new();
    protected readonly Database db = new();
    public IDbConnection GetDbConnection()
    {
      var connection = db.Connection;
      return connection;
    }

    public string QueryLimit(string query)
    {
      var p = db.Symbol();
      if (!query.Contains("order by"))
      {
        throw new Exception("Query must contains 'Order By' clause.");
      }

      if (db.Type() == "MySQL")
      {
        query += $" limit {p}Limit offset 0";
      }
      else
      {
        query += $" offset 0 rows fetch first {p}Limit rows only";
      }

      return query;
    }
    public string QueryLimitOffset(string query)
    {
      var p = db.Symbol();
      if (!query.Contains("order by"))
      {
        throw new Exception("Query must contains 'Order By' clause.");
      }

      if (db.Type() == "MySQL")
      {
        query += $" limit {p}Limit offset {p}Offset";
      }
      else
      {
        query += $" offset {p}Offset rows fetch first {p}Limit rows only";
      }

      return query;
    }
  }
}