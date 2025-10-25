namespace Project.Utils
{
    public static class IdHandler
    {
        public static string CreateId()
        {
            var dateNow = DateTime.Now;
            return dateNow.ToString("yyyyMMddHHmmssfff");
        }
    }
}