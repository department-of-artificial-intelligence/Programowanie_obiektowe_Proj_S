using System;

namespace Project.Model
{
    public static class Metodyrozszerzajace
    {
        public static string FormatujNumerTelefonu(this string numer)
        {
            if (string.IsNullOrEmpty(numer) || numer.Length != 9)
            {
                return numer;
            }

            return $"+48 {numer.Substring(0, 3)} {numer.Substring(3, 3)} {numer.Substring(6, 3)}";
        }

        public static bool CzyZaliczony(this int ocena, int prog = 60)
        {
            return ocena >= prog;
        }
    }
}
