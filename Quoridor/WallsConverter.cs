using System;
using System.Text.RegularExpressions;
using Quoridor.Model.Common;

namespace Quoridor
{
    public static class WallsConverter
    {
        public static Coordinates MixedToNumber(string mixedCoordinates)
        {
            CheckCompatibility(mixedCoordinates);
            
            SplitCoordinates(mixedCoordinates, out string letters, out string number, out bool isHorizontal);

            int row = int.Parse(number) * 2 - 2;
            int column = LettersToNumber(letters.ToUpper());

            if (isHorizontal)
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
        
        private static void CheckCompatibility(string mixedCoordinates)
        {
            if (mixedCoordinates.Length < 2)
            {
                throw new ArgumentException();
            }

            if (!Regex.IsMatch(mixedCoordinates, @"(\w+)(\d+)(h?)", RegexOptions.IgnoreCase))
            {
                throw new ArgumentException();
            }
        }
        private static void SplitCoordinates(string mixedCoordinates, out string letters, out string number, out bool isHorizontal)
        {
            Regex regex = new Regex(@"[\w+]|[\d+]|[h?]");
            MatchCollection matches = regex.Matches(mixedCoordinates);
            letters = matches[0].Value;
            number = matches[1].Value;
            isHorizontal = false;

            if (matches.Count == 3)
            {
                isHorizontal = true;
            }
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
