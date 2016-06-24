using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculateTF();
        }

        public static void CalculateTF()
        {
            SortedDictionary<string, int> dicWords = new SortedDictionary<string, int>();
            foreach (string word in GetWords())
            {
                int count;
                dicWords[word] = dicWords.TryGetValue(word, out count) ? count + 1 : 1;
            }
            
            foreach (KeyValuePair<string, int> pair in dicWords)
            {
                double tf;
                tf = pair.Value / (double)dicWords.Count();
                Console.WriteLine("{1,8} {0}", pair.Key, tf);
            }
        }

        
        public static IEnumerable<string> GetSentences()
        {
            Encoding enc = Encoding.GetEncoding(1251);
            var textOriginal = File.ReadAllText("1.txt", enc);

            var regexSentences = new Regex("[А-Я].*?[.!?]\\s+(?=[А-Я])");
            var sentences =
                regexSentences.Matches(textOriginal)
                    .OfType<Match>()
                    .Select(match => match.Value)
            .ToList();

            foreach (var item in sentences)
            {
                File.WriteAllLines("textSentences.txt", sentences);
            }
            return sentences;
        }

        public static IEnumerable<string> GetWords()
        {
            GetSentences();
            var textSentences = File.ReadAllText("textSentences.txt");
            var regexWords = new Regex("\\w+(?=\\W)");
            var words =
                regexWords.Matches(textSentences)
                    .OfType<Match>()
                    .Select(match => match.Value)
                    .ToList();

            int wordsQuantity = words.Count();

            foreach (var item in words)
            {
                File.WriteAllLines("textWords.txt", words);
            }

            Console.WriteLine("Слов в тексте - {0}", wordsQuantity);

            return words;
        }
    }
}

