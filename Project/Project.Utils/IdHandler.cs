namespace Project.Utils
{
    public static class IdGenerator
    {
        public static string Generate() => DateTime.Now.ToString("yyyyMMddHHmmssfff");
    }
}