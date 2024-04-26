// See https://aka.ms/new-console-template for more information

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
    else
    {
      throw new ArgumentException("Invalid argument format.");
    }
  }
}
