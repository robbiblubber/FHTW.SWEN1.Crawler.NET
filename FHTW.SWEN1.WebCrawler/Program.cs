using System;



namespace FHTW.SWEN1.WebCrawler
{
    /// <summary>Main program class.</summary>
    public class Program
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Entry point                                                                                              //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Program entry point.</summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            bool parallel = true;
            bool dump = false;
            string url = "";
            int n = 2;

            foreach(string i in args)
            {

                if(int.TryParse(i, out n))
                {
                    Crawler.MaxDepth = n;
                }
                else if(i.StartsWith("--"))
                {
                    switch(i)
                    {
                        case "--serial": parallel = false; break;
                        case "--dump":   dump = true; break;
                    }
                }
                else if(i.StartsWith("-"))
                {
                    if(i.Contains('s')) { parallel = false; }
                    if(i.Contains('d')) { dump = true; }
                }
                else
                {
                    url = i;
                }
            }

            CrawlerTimer.Start();

            ICrawler c = (parallel ? new ParallelCrawler() : new SerialCrawler());
            c.Crawl(url, 0);
            c.Wait();

            CrawlerTimer.Stop();

            if(dump)
            {
                CrawlerResult.Dump();
                Console.WriteLine();
            }

            Console.Write("Processed " + CrawlerResult.VisitedCount.ToString() + " sites in ");
            Console.WriteLine(Convert.ToInt64(CrawlerTimer.TimeElapsed.TotalMilliseconds).ToString() + " ms.");
        }
    }
}