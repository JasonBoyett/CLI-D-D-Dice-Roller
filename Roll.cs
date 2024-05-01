public enum RollType
{
  Advantage,
  Disadvantage,
  Normal
}

public class Roll
{
  private List<int> results = new List<int>();
  public Dice[] Dice { get; set; }
  public int Modifier { get; set; }
  public RollType Type { get; set; }
  public string Name { get; set; }
  public List<int> Results
  {
    get
    {
      var resultList = new List<int>();
      foreach (Dice die in Dice)
      {
        resultList.Add(die.Result);
      }
      return resultList;
    }
  }

  public Roll(
    Dice[] dice,
    RollType type = RollType.Normal,
    int modifier = 0,
    string name = ""
  )
  {
    this.Modifier = modifier;
    this.Dice = dice;
    this.Type = type;
    this.Name = name;
  }
  public Roll()
  {
    this.Modifier = 0;
    this.Dice = new Dice[0];
    this.Type = RollType.Normal;
    this.Name = "";
  }

  public int RollDice()
  {
    int total = 0;
    foreach (Dice die in Dice)
    {
      if (this.Type == RollType.Advantage)
      {
        total += Math.Max(die.Roll(), die.Roll());
        results.Add(total);
      }
      else if (this.Type == RollType.Disadvantage)
      {
        total += Math.Min(die.Roll(), die.Roll());
        Results.Add(total);
      }
      else
      {
        total += die.Roll();
        Results.Add(total);
      }
    }
    return total + this.Modifier;
  }
}
