using System;
using System.Text.RegularExpressions;
using Quoridor.Model.Common;

namespace Quoridor
{
    public static class WallsConverter
    {
        public static Coordinates MixedToNumber(string mixedCoordinates)
        {
            if (mixedCoordinates.Length < 2)
            {
                throw new ArgumentException();
            }

            if (!Regex.IsMatch(mixedCoordinates, @"([a-zA-z]+)([0-9]+)(h?)", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException();
            }

            Regex regex = new Regex(@"([a-zA-Z]+)|([0-9]+)");
            MatchCollection matches = regex.Matches(mixedCoordinates);
            string lettersString = matches[0].Value;
            string numbersString = matches[1].Value;

            int number = int.Parse(numbersString);

            int row = number * 2 - 2;
            int column = LettersToNumber(lettersString.ToUpper());

            if (matches.Count == 3)
            {
                row += 1;
            }

            return new Coordinates(row, column);
        }
        public static string NumberToMixed(Coordinates numberCoordinates)
        {
            string letters = NumberToLetters(numberCoordinates.column);
            string numbers = ((numberCoordinates.row + 2) / 2).ToString();

            if (numberCoordinates.row % 2 == 1)
            {
                return letters + numbers + "h";
            }
            
            return letters + numbers;
        }

        private static int LettersToNumber(string letters)
        {
            return letters switch
            {
                "S" => 0,
                "T" => 1,
                "U" => 2,
                "V" => 3,
                "W" => 4,
                "X" => 5,
                "Y" => 6,
                "Z" => 7,
                _ => throw new ArgumentOutOfRangeException(nameof(letters), letters, null)
            };
        }
        private static string NumberToLetters(int number)
        {
            return number switch
            {
                0 => "S",
                1 => "T",
                2 => "U",
                3 => "V",
                4 => "W",
                5 => "X",
                6 => "Y",
                7 => "Z",
                _ => throw new ArgumentOutOfRangeException(nameof(number), number, null)
            };
        }
    }
}
