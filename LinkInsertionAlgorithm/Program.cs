using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LinkInsertionAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = @"<img src='test.jpg'>
                <p>Это первый абзац текста. Он состоит из трех предложений. Это первое предложение. Это второе предложение? Это третье предложение!</p>
                <h2>Подзаголовок</h2>
                <p>А это второй абзац текста, он в отличие от первого состоит из двух предложений. Это первое. <a href='http://test0.com'>Эта ссылка тут изначально. Она состоит из двух предложений</a>. А это второе.</p>
                <p>Это третий абзац текста. Он состоит из трех предложений. Это первое предложение. Это второе предложение? Это третье предложение!</p>
                <p>Это четвертый абзац текста. В нем интересное предложение. Эта <a href=’http://test0.com’>ссылка</a> тут изначально. А это уже новое предложение.</p>
                <p>Это пятый абзац текста. В нем еще интереснее ссылка. Вот такая <a href=’http://test0.com’>ссылка</a>. А это уже новое предложение после ссылки.</p>"; 

            //text = Regex.Replace(text, @"\s+", " ");

            bool repeat;
            do
            {
                repeat = false;
                Console.Clear();

                Console.Write("Количество ссылок для вставки в базовый текст: ");

                int count = 0;

                int.TryParse(Console.ReadLine(), out count);

                List<string> links = GenerateLinks(count);

                string resultText = Algorithm.InsertLinks(text, links);

                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Базовый текст:");
                Console.WriteLine(text);


                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Текст после вставки ссылок:");
                Console.WriteLine(resultText);

                Console.WriteLine();
                Console.Write("Повторить тест? (Yes - 1/ No - 0): ");

                int temp;
                if (int.TryParse(Console.ReadLine(), out temp))
                {
                    if (temp == 1)
                    {
                        repeat = true;
                    }
                    else
                    {
                        repeat = false;
                    }
                }
            } while (repeat);
            
        }

        public static List<string> GenerateLinks(int count)
        {
            const string linkTemplate1 = "<a href='http://test{0}.com'>Это ссылка #{0}.</a>.";
            const string linkTemplate2 = "<a href='http://test{0}.com'>Это ссылка #{0}. Она состоит из нескольких предложений</a>.";

            var result = new List<string>();
            Random rnd = new Random();

            for (int i = 0; i < count; i++)
            {
                result.Add(rnd.Next(0, 2) == 0 ? string.Format(linkTemplate1, i + 1) : string.Format(linkTemplate2, i + 1));
            }

            return result;
        }
    }
}
