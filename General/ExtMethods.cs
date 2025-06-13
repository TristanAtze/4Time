namespace _4Time.General;

public static class ExtMethods
{
    public static string Capitalise(this string str)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        
        return char.ToUpper(str[0]) + str[1..];
    }

    public static string Verstraussen(this string str)
    {
        return !str.Contains("strauss", StringComparison.CurrentCultureIgnoreCase) ? str : str.Replace("ss", "ß").Replace("SS", "ẞ");
    }
}