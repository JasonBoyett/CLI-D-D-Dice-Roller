public class Dice
{
  public int Sides { get; set; }
  public int result { get; set; }

  public Dice(int sides)
  {
    this.Sides = sides;
  }

  public int Roll()
  {
    this.result = new System.Random().Next(1, Sides + 1);
    return this.result;
  }
}
