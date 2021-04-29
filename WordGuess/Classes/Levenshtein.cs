using System;

namespace WordGuess.Classes
{
    public class Levenshtein
    {
        public static int LevenshteinDistance(string value1, string value2)
        {
            int different;
            int[,] matrix = new int[value1.Length + 1, value2.Length + 1];

            for (int i = 0; i <= value1.Length; i++)
            {
                matrix[i, 0] = i; 
                
            }

            for (int j = 0; j <= value2.Length; j++)
            {
                matrix[0, j] = j;
            }

            for (int i = 1; i <= value1.Length; i++)
            {
                for (int j = 1; j <= value2.Length; j++)
                {
                    different = (value1[i - 1] == value2[j - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + different);
                }
            }
            return matrix[value1.Length, value2.Length];
        }
    }
}