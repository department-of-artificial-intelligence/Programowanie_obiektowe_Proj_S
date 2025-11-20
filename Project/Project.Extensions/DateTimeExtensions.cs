namespace Project.Extensions
{
    public static class DateTimeExtensions
    {
        public static int YearsBetween(this DateTime dateTime, DateTime anotherDateTime)
        {
            int years = anotherDateTime.Year - dateTime.Year;

            if (anotherDateTime.Month < dateTime.Month || (anotherDateTime.Month == dateTime.Month && anotherDateTime.Day < dateTime.Day))
            {
                years--;
            }

            return Math.Abs(years);
        }
    }
}
