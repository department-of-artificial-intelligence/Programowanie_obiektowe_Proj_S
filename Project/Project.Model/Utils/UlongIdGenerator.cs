namespace Project.Model.Utils
{
    public static class UlongIdGenerator
    {
        private const int CountOffset = 16;
        
        private const ulong CountMask = 0xFFFFFFFFFFFFFFFF >> (64 - CountOffset);

        private static ulong s_generatedAt = 0; // in seconds from unix time start

        private static ulong s_count = 0;

        public static ulong GenerateId() /* 2^16 id/s w/o collisions */
        {
            var unixTime = (ulong) DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            s_count = (s_generatedAt == unixTime) ? s_count + 1 : 0;
            s_generatedAt = unixTime;

            // 0..48 bits: generated at (seconds since unix epoch)
            // 48..64 bits: count within the second
            return (s_count & CountMask) | (s_generatedAt << CountOffset);
        }
        
        public static (DateTime, ulong) ExtractInfo(ulong id)
        {
            var generatedAt = id >> CountOffset;
            var count = id & CountMask;
            
            var dateTime = DateTimeOffset.FromUnixTimeSeconds((long) generatedAt).UtcDateTime;
            
            return (dateTime, count);
        }
    }
}
