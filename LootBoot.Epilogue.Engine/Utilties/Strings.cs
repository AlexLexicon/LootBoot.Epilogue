namespace LootBoot.Epilogue.Engine.Utilties;
public static class Strings
{
    public static bool Compare(string s1, string s2, bool ignoreCase = true) => ignoreCase ? string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase) : s1 == s2;
}
