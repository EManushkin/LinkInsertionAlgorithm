using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkInsertionAlgorithm
{
    public static class Algorithm
    {
        public static string InsertLinks(string text, IEnumerable<string> links)
        {
            if (string.IsNullOrEmpty(text) || links == null || !links.Any())
                return text;

            string pattern = @"(\s*(?:[^<>\.]+?(?:<a .+?>.+?</a>)?[^<>\.]*?|<a .+?>.+?</a>)[\.\?\!])";

            var rnd = new Random();

            var document = new HtmlDocument();

            document.LoadHtml(text);

            var paragraphNodes = document.DocumentNode.SelectNodes($"//p")?.ToList() ?? null;

            if (paragraphNodes == null || !paragraphNodes.Any())
                return text;

            foreach (var link in links)
            {
                var pIndex = rnd.Next(0, paragraphNodes.Count);

                string[] sentences = Regex.Split(paragraphNodes[pIndex].InnerHtml, pattern).Where(s => !string.IsNullOrEmpty(s)).ToArray();

                var sIndex = rnd.Next(0, sentences.Length + 1);

                var pos = sIndex != sentences.Length ? paragraphNodes[pIndex].InnerHtml.IndexOf(sentences[sIndex]) : paragraphNodes[pIndex].InnerHtml.Length;

                paragraphNodes[pIndex].InnerHtml = paragraphNodes[pIndex].InnerHtml.Insert(pos, link);
            }

            return document.DocumentNode.InnerHtml;
        }
    }
}
