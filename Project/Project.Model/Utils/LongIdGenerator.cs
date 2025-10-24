namespace Project.Model.Utils
{
    public static class LongIdGenerator
    {
        private const int CountOffset = 4;

        private static long s_generatedAt = 0; // in seconds from unix time start

        private static int s_count = 0;

        public static long GenerateId() /* approx. 21 id/ms */
        {
            var unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            s_count = (s_generatedAt == unixTime) ? s_count + 1 : 0;
            s_generatedAt = unixTime;

            return s_count ^ (s_generatedAt << CountOffset);
        }
    }
}
