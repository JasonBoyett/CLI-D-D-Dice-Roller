using DatabaseWrapper.Core;
using Watson.ORM.Core;

public class Dice
{
  [Column("sides", false, DataTypes.Int, false)]
  public int Sides { get; set; }
  public int Result { get; set; }

  public Dice(int sides)
  {
    this.Sides = sides;
  }

  public int Roll()
  {
    this.Result = new System.Random().Next(1, Sides + 1);
    return this.Result;
  }
}
