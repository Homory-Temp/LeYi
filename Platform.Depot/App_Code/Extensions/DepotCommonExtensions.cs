
public static class DepotCommonExtensions
{
    public static string Formatted(this string format, params object[] objects)
    {
        return string.Format(format, objects);
    }
}
