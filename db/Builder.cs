using System.Reflection;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace db;
public class Builder
{
  private static readonly string _assembly = Assembly.GetExecutingAssembly().Location;
  private static readonly string _directory = Path.GetDirectoryName(_assembly) ?? "/db";
  private static readonly string _path = Path.Join(_directory, "rolls.db");
  private static readonly string _connectionString = $"Data Source={_path}";

  public static void CreateRoll(Roll roll)
  {
    using (var connection = new SqliteConnection(_connectionString))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
        INSERT INTO Rolls (Name, Modifier, Type, Dice)
        VALUES ($Name, $Modifier, $Type, &Dice)
      ";
      command.Parameters.AddWithValue("$Name", roll.Name);
      command.Parameters.AddWithValue("$Modifier", roll.Modifier);
      command.Parameters.AddWithValue("$Type", roll.Type);
      command.Parameters.AddWithValue("$Dice", JsonConvert.SerializeObject(roll.Dice));
    }
  }

  public static void InitializeDb()
  {
    using (var connection = new SqliteConnection(_connectionString))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
        CREATE TABLE IF NOT EXISTS Rolls (
          Name TEXT PRIMARY KEY,
          Modifier INTEGER,
          Type INTEGER,
          Dice TEXT
        )
      ";
      command.ExecuteNonQuery();
    }
  }
  public static void UpdateRoll(Roll roll)
  {
    if (!Fetcher.RollExists(roll.Name))
    {
      throw new ArgumentException("Roll does not exist.");
    }
    using (var connection = new SqliteConnection(_connectionString))
    {
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = @"
        UPDATE Rolls
        SET Modifier = $Modifier, Type = $Type, Dice = $Dice
        WHERE Name = $Name
      ";
      command.Parameters.AddWithValue("$Name", roll.Name);
      command.Parameters.AddWithValue("$Modifier", roll.Modifier);
      command.Parameters.AddWithValue("$Type", roll.Type);
      command.Parameters.AddWithValue("$Dice", JsonConvert.SerializeObject(roll.Dice));
    }
  }
}
