public enum RollType
{
  Advantage,
  Disadvantage,
  Normal
}

public class Roll
{
  public Dice[] dice { get; set; }
  public int modifier { get; set; }
  public RollType type { get; set; }
  public List<int> results
  {
    get
    {
      var res = new List<int>();
      foreach (Dice die in dice)
      {
        res.Add(die.result);
      }
      return res;
    }
  }

  public Roll(Dice[] dice, RollType type = RollType.Normal, int modifier = 0)
  {
    this.modifier = modifier;
    this.dice = dice;
    this.type = type;
  }

  public int RollDice()
  {
    int total = 0;
    foreach (Dice die in dice)
    {
      if (this.type == RollType.Advantage)
      {
        total += Math.Max(die.Roll(), die.Roll());
        results.Add(total);
      }
      else if (this.type == RollType.Disadvantage)
      {
        total += Math.Min(die.Roll(), die.Roll());
        results.Add(total);
      }
      else
      {
        total += die.Roll();
        results.Add(total);
      }
    }
    return total + this.modifier;
  }
}
