namespace Korepetycje.Model
{
    public class Lesson
    {
        public enum Subject
        {
            Math,
            English
        }
        public enum DayOfWeek
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        public enum PlaceOfLesson   //At who's house is the lesson to take place at
        {
            Tutor,
            Student
        }
    }
}
