using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CombatCarsWinformsClient
{
    public class FontParser
    {
        static int HeaderSize = 4;

        private static int GetValue(string s)
        {
            string value = s.Substring(s.IndexOf('=') + 1);
            return int.Parse(value);
        }

        public static Dictionary<char, CharacterData> Parse(string filePath)
        {
            Dictionary<char, CharacterData> charDictionary = new Dictionary<char, CharacterData>();
            string[] lines = File.ReadAllLines(filePath);
            for (int i = HeaderSize; i < lines.Length; i++)
            {
                string firstLine = lines[i];

                if (firstLine.StartsWith("char"))
                {
                    string[] typesAndValues = firstLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    CharacterData charData = new CharacterData
                    {
                        Id = GetValue(typesAndValues[1]),
                        X = GetValue(typesAndValues[2]),
                        Y = GetValue(typesAndValues[3]),
                        Width = GetValue(typesAndValues[4]),
                        Height = GetValue(typesAndValues[5]),
                        XOffset = GetValue(typesAndValues[6]),
                        YOffset = GetValue(typesAndValues[7]),
                        XAdvance = GetValue(typesAndValues[8]),
                    };
                    //Console.WriteLine((char)charData.Id);
                    charDictionary.Add((char)charData.Id, charData);
                }
            }
            return charDictionary;
        }
    }
}
