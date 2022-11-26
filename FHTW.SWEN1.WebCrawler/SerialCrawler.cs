using System;



namespace FHTW.SWEN1.WebCrawler
{
    /// <summary>This class implements a single-threaded serial crawler.</summary>
    public class SerialCrawler: Crawler
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Creates a new instance of this class.</summary>
        public SerialCrawler(): base()
        {}



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [override] Crawler                                                                                       //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Crawls an URL at the next depth level.</summary>
        /// <param name="uel">Target URL.</param>
        protected override void _CrawlInto(string url)
        {
            if(Depth >= MaxDepth) return;

            CrawlerResult.__X.Add((Depth + 1).ToString() + " " + url);
            new SerialCrawler().Crawl(url, Depth + 1);
        }
    }
}
