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
    for (var i = 0; i < roll.results.Count; i++)
    {
      if (i == roll.results.Count - 1)
      {
        AnsiConsole.MarkupLine($"and [blue]{roll.results[i]}[/].");
      }
      else
      {
        AnsiConsole.Markup($"[blue]{roll.results[i]}[/], ");
      }
    }
    AnsiConsole.MarkupLineInterpolated($"The total is [bold green]{total}[/]!");
  }

  private static void Invalid(int num)
  {
    AnsiConsole.MarkupLineInterpolated(
        $@"
          [red]{num}[/] is not a valid dice number. 
          [yellow]The number must be greater than 0.[/]
        "
        );
  }
}
