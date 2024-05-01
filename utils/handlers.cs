using db;
using Spectre.Console;

namespace Utils;

public class Handlers
{
  public static void HandleStandardFormat(string[] args)
  {
    var sides = int.Parse(args[0].Substring(1));
    Roll d = new Roll(new Dice[] { new Dice(sides) });
    AnsiConsole.MarkupLineInterpolated($"Rolling a d{sides}...");
    AnsiConsole.MarkupLineInterpolated($"You rolled a [green]{d.RollDice()}[/]!");
  }

  public static void HandleMultieRollFormat(string[] args)
  {
    Console.WriteLine($"Rolling {args[0]} {args[1]}...");
    var dice = new List<Dice>();
    var count = int.Parse(args[0]);
    var message = new List<Markup>();
    var total = 0;
    Roll roll;
    for (var i = 0; i < count; i++)
    {
      dice.Add(new Dice(int.Parse(args[1].Substring(1))));
    }
    roll = new Roll(dice.ToArray());
    total = roll.RollDice();
    AnsiConsole.Markup("You rolled ");
    for (var i = 0; i < roll.Results.Count; i++)
    {
      if (i == roll.Results.Count - 1)
      {
        AnsiConsole.MarkupLine($"and [blue]{roll.Results[i]}[/].");
      }
      else
      {
        AnsiConsole.Markup($"[blue]{roll.Results[i]}[/], ");
      }
    }
    AnsiConsole.MarkupLineInterpolated($"The total is [bold green]{total}[/]!");
  }

  public static void HandleCreateFormat(string[] args)
  {
    if (args.Length < 2)
    {
      handleCreateUnnamed();
    }
  }

  private static void handleCreateUnnamed()
  {
    var name = getRollName();

    if (Fetcher.RollExists(name))
    {
      var keyInfo = Console.ReadKey();

      AnsiConsole.MarkupLine("[yellow]A roll with that name already exists.[/]");
      AnsiConsole.MarkupLine(@"
        [yellow]would you like to overwrite it?\n
        Yes([blue]y[\]) | No([blue]n[/])\n
        Press [blue]q[/] to quit.
        [/]
      ");
      if (keyInfo.Key == ConsoleKey.Y)
      {
        HandleOverwirteRoll();
      }
      if (keyInfo.Key == ConsoleKey.N)
      {
        var roll = db.Fetcher.GetRoll(name);
        roll.RollDice();
      }
    }
  }

  private static void HandleOverwirteRoll()
  {
    throw new NotImplementedException();
  }

  private static string getRollName()
  {
    try
    {
      AnsiConsole.MarkupLine("What would you like to name this roll?");
      var name = Console.ReadLine() ?? "";
      if (!isValidRollName(name))
      {
        AnsiConsole.MarkupLine("[red]Invalid name.[/]");
        return getRollName();
      }
      return "";
    }
    catch (Exception e)
    {
      AnsiConsole.MarkupLineInterpolated($"[red]{e.Message}[/]");
      System.Environment.Exit(1);
      return "";
    }
  }

  private static bool isValidRollName(string name)
  {
    if (name.Length < 1)
    {
      AnsiConsole.MarkupLine("[yellow]The name must be at least one character long.[/]");
      return false;
    }
    if (name.Contains(" "))
    {
      AnsiConsole.MarkupLine("[yellow]The name must not contain any whitespace.[/]");
      return false;
    }
    return true;
  }

  private static void Invalid(int num)
  {
    AnsiConsole.MarkupLineInterpolated(
        $@"
          [red]{num}[/] is not a valid Dice number. 
          [yellow]The number must be greater than 0.[/]
        "
        );
  }

  internal static void HandleCustomFormat(string[] args)
  {
    throw new NotImplementedException();
  }
}
