using System.Text.RegularExpressions;

namespace Utils;

internal class Checkers
{
  internal static bool CheckStandardFormat(string[] args)
  {
    return Regex.IsMatch(args[0], @"^d\d+$");
  }
  internal static bool CheckMultieRollFormat(string[] args)
  {
    return Regex.IsMatch(args[0], @"^\d") && Regex.IsMatch(args[1], @"d\d+$");
  }
  internal static bool CheckCreateFormat(string[] args)
  {
    return args[0].ToLower() == "create";
  }

  internal static bool CheckCustomFormat(string[] args)
  {
    return args.Length == 1;
  }
}
