namespace Project.Extensions
{
    public static class DateTimeExtensions
    {
        public static int YearsBetween(this DateTime dateTime, DateTime anotherDateTime)
        {
            return Math.Abs(dateTime.Year - anotherDateTime.Year);
        }
    }
}
