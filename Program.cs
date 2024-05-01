// See https://aka.ms/new-console-template for more information

using Spectre.Console;

class Program
{
  public static void Main(string[] args)
  {
    if (args.Length > 0)
    {
      try
      {
        HandleArgs(args);
      }
      catch (Microsoft.Data.Sqlite.SqliteException e)
      {
        if (e.Message.Contains("no such table"))
        {
          AnsiConsole.MarkupLine("[yellow]Database not initialized. Initializing and restarting.[/]");
          db.Builder.InitializeDb();
          Main(args);
        }
        else
        {
          Console.WriteLine(e.Message);
        }
      }
      catch (ArgumentException e)
      {
        Console.WriteLine(e.Message);
      }
    }
    else
    {
      Roll d20 = new Roll(new Dice[] { new Dice(20) });
      Console.WriteLine("Rolling a d20...");
      Console.WriteLine($"You rolled a {d20.RollDice()}!");
    }
  }

  ///<summary>
  /// Handles command line arguments.
  /// Trhows an ArgumentException if no arguments are provided.
  ///</summary>
  private static void HandleArgs(string[] args)
  {
    if (args.Length < 1)
    {
      throw new ArgumentException("No arguments provided.");
    }
    if (Utils.Checkers.CheckStandardFormat(args))
    {
      Utils.Handlers.HandleStandardFormat(args);
      return;
    }
    if (Utils.Checkers.CheckMultieRollFormat(args))
    {
      Utils.Handlers.HandleMultieRollFormat(args);
      return;
    }
    if (Utils.Checkers.CheckCreateFormat(args))
    {
      Utils.Handlers.HandleCreateFormat(args);
      return;
    }
    if (Utils.Checkers.CheckCustomFormat(args))
    {
      Utils.Handlers.HandleCustomFormat(args);
      return;
    }
    else
    {
      throw new ArgumentException("Invalid argument format.");
    }
  }
}
