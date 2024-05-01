using System.Reflection;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace db;
public class Fetcher
{
  private static string _assembly = Assembly.GetExecutingAssembly().Location;
  private static string _directory = Path.GetDirectoryName(_assembly) ?? "/db";
  private static string _path = Path.Join(_directory, "rolls.db");
  private static string _connectionString = $"Data Source={_path}";

  public static Roll GetRoll(string name)
  {
    using (var connection = new SqliteConnection(_connectionString))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
        SELECT * 
        FROM Rolls
        WHERE Name = $Name
      ";
      command.Parameters.AddWithValue("$Name", name);

      using (var reader = command.ExecuteReader())
      {
        if (reader.Read())
        {
          var dice = parseRoll(reader);
          return new Roll(
            dice,
            (RollType)reader.GetInt32(2),
            reader.GetInt32(1),
            reader.GetString(0)
          );
        }
      }
    }
    return new Roll();
  }

  public static bool RollExists(string name)
  {
    Console.WriteLine($"Checking if {name} exists");
    using (var connection = new SqliteConnection(_connectionString))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
        SELECT COUNT(*) 
        FROM Rolls
        WHERE Name = $Name
      ";
      command.Parameters.AddWithValue("$Name", name);

      using (var reader = command.ExecuteReader())
      {
        if (reader.Read())
        {
          return reader.GetInt32(0) > 0;
        }
      }
    }
    return false;
  }

  private static Dice[] parseRoll(SqliteDataReader reader)
  {
    try
    {
      return JsonConvert.DeserializeObject<Dice[]>(reader.GetString(3))
        ?? new Dice[1] { new Dice(20) };
    }
    catch
    {
      return new Dice[1] { new Dice(20) };
    }
  }
}
