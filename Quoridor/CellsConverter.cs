using System;
using System.Text.RegularExpressions;
using Quoridor.Model.Common;

namespace Quoridor
{
    public static class CellsConverter
    {
        public static Coordinates MixedToNumber(string mixedCoordinates)
        {
            if (mixedCoordinates.Length < 2)
            {
                throw new ArgumentException();
            }

            if (!Regex.IsMatch(mixedCoordinates, "[A-Z]+[0-9]+", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException();
            }

            Regex regex = new Regex("([a-zA-Z]+)|([0-9]+)");
            MatchCollection matches = regex.Matches(mixedCoordinates);
            string lettersString = matches[0].Value;
            string numbersString = matches[1].Value;
            
            int row = int.Parse(numbersString) - 1;
            int column = LettersToNumber(lettersString.ToUpper());
            
            return new Coordinates(row, column);
        }
        public static string NumberToMixed(Coordinates numberCoordinates)
        {
            string letters = NumberToLetters(numberCoordinates.column);
            string numbers = (numberCoordinates.row + 1).ToString();

            return letters + numbers;
        }

        private static int LettersToNumber(string letters)
        {
            return letters switch
            {
                "A" => 0,
                "B" => 1,
                "C" => 2,
                "D" => 3,
                "E" => 4,
                "F" => 5,
                "G" => 6,
                "H" => 7,
                "I" => 8,
                _ => throw new ArgumentOutOfRangeException(nameof(letters), letters, null)
            };
        }
        private static string NumberToLetters(int number)
        {
            return number switch
            {
                0 => "A",
                1 => "B",
                2 => "C",
                3 => "D",
                4 => "E",
                5 => "F",
                6 => "G",
                7 => "H",
                8 => "I",
                _ => throw new ArgumentOutOfRangeException(nameof(number), number, null)
            };
        }
    }
}
