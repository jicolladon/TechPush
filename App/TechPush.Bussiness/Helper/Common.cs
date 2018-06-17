using System;
using System.Collections.Generic;
using System.Text;

namespace TechPush.Bussiness.Helper
{
    public static class Common
    {

        public const string BaseUrl = "http://techpushcoreapi20180528074908.azurewebsites.net/";
        //public const string BaseUrl = "http://192.168.1.18:45455/";
        //public const string BaseUrl = "http://192.168.1.103:55556/";

        public static string LoremIpsum(int minWords, int maxWords,
                                        int minSentences, int maxSentences,
                                        int numParagraphs)
        {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences)
                + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            StringBuilder result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                result.Append("<p>");
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(" "); }
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
                result.Append("</p>");
            }

            return result.ToString();
        }
    }
}
