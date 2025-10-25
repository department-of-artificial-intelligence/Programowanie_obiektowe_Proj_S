namespace Project.Utils
{
    public static class ArrayHandler
    {
        public static bool AddUniqueStringToMax5NlementsArray(List<string> array, string item, uint n)
        {
            if (array.Contains(item) || array.Count >= n)
                return false;

            array.Add(item);
            return true;
        }

        public static string StringArrayToString(List<string> array)
        {
            string result = string.Empty;

            foreach (string item in array)
            {
                result += item + ", ";
            }

            return result;
        }

        public static bool DeleteElFromStringArray(List<string> array, string item)
        {
            if (!array.Remove(item))
            {
                return false;
            }

            return true;
        }
    }
}