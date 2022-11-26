using System;



namespace FHTW.SWEN1.WebCrawler
{
    /// <summary>Crawlers implement this interface.</summary>
    public interface ICrawler
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // methods                                                                                                  //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Crawls the URL.</summary>
        /// <param name="url">URL.</param>
        /// <param name="depth">Current depth.</param>
        public void Crawl(string url, int depth);


        /// <summary>Waits for the crawler to finish.</summary>
        public void Wait();
    }
}
