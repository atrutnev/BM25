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
            var webClient = new WebClient();
            webClient.Encoding = Encoding.GetEncoding(1251);
            var textOriginal = webClient.DownloadString(
                "http://lib.ru/KING/r_king-33.txt");

            var regexSentences = new Regex("[А-Яа-я0-9\\)\\(\\,+\\.+\\:+\\-+\"+\\s+\\?+\\!+]+(?!\\?\\!\\.\\s+[А-Я]+)");
            var sentences =
                regexSentences.Matches(textOriginal)
                    .OfType<Match>()
                    .Select(match => match.Value)
            //.Select(s => s.Trim())
            //.Select(x => x.ToLower())
            .ToList();

            var listSentences = sentences
            //        .GroupBy(s => s)
            //        .OrderByDescending(g => g.Count())
            //        .Select(g => new
            //        {
            //            Word = g.Key,
            //            P = g.Count() * 1.0 / words.Count
            //        })
                    //.Take(50)
                    .ToArray();

            foreach (var item in listSentences)
            {
                File.WriteAllLines("textSentences.txt", listSentences);
            }

            var textSentences = File.ReadAllText("2.txt");
            var regexWords = new Regex("\\w+(?=\\W)");
            var words =
                regexWords.Matches(textSentences)
                    .OfType<Match>()
                    .Select(match => match.Value)
                    //.Select(s => s.Trim())
                    //.Select(x => x.ToLower())
                    .ToList();

            var list = words
                    .ToArray();

            int wordsQuantity = list.Count();

            foreach (var item in list)
            {
                //File.WriteAllLines("textWords.txt", list);
            }
            
            Console.WriteLine("Слов в тексте - {0}", wordsQuantity);

        }
    }
}
