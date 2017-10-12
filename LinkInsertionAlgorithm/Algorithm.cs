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
            var document = new HtmlDocument();

            document.LoadHtml(text);

            var paragraphNodes = document.DocumentNode.SelectNodes($"//p")?.ToList() ?? null;

            string pattern = @"(\s*<a .+?>.+?</a>\.?|[^<>]+?\.)|(\s*<a .+?>.+?</a>\??|[^<>]+?\?)|(\s*<a .+?>.+?</a>!?|[^<>]+?!)";

            foreach (var item in paragraphNodes)
            {
                string[] sentences = Regex.Split(item.InnerHtml, pattern).Where(s => !string.IsNullOrEmpty(s)).ToArray(); ;
                foreach (string s in sentences)
                {
                    Console.WriteLine("{0}", s);
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
            }
            

            //paragraphNodes[0].InnerHtml = "test";



            //if (nodes != null && nodes.Count > 0)
            //{
            //    return nodes.Select((x, i) => $"<a href='#article-text__subtitle--{i + 1}' class='article-header__anchor-link'>{x.InnerText}</a>").ToArray();
            //}
            //return new string[] { };
            string result = document.DocumentNode.InnerHtml;

            return result;
        }
    }
}
