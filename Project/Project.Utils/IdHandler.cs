namespace Project.Utils
{
    public static class IdGenerator
    {
        public static string Generate()
        {
            var now = DateTime.Now;
            return now.ToString("yyyyMMddHHmmss") + now.ToString("fffffff");
        }

    }
}