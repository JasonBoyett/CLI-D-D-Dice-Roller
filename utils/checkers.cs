using System.Text.RegularExpressions;

namespace Utils;

public class Checkers
{
  public static bool CheckStandardFormat(string[] args)
  {
    return Regex.IsMatch(args[0], @"^d\d+$");
  }
  public static bool CheckMultieRollFormat(string[] args)
  {
    return Regex.IsMatch(args[0], @"^\d") && Regex.IsMatch(args[1], @"d\d+$");
  }
}
