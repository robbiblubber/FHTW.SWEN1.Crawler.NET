using System;
using System.Collections.Concurrent;



namespace FHTW.SWEN1.WebCrawler
{
    /// <summary>This class implements a multi-threaded parallel crawler.</summary>
    public class ParallelCrawler: Crawler, ICrawler
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private static members                                                                                   //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Synchronization object.</summary>
        private static object _Sync = new();

        /// <summary>Started tasks.</summary>
        private static ConcurrentBag<Task> _Tasks = new();



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Creates a new instance of this class.</summary>
        public ParallelCrawler(): base()
        {}



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [override] Crawler                                                                                       //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Crawls an URL at the next depth level.</summary>
        /// <param name="uel">Target URL.</param>
        protected override void _CrawlInto(string url)
        {
            if(Depth >= MaxDepth) return;

            lock(_Sync)
            {
                _Tasks.Add(Task.Run(() => { new ParallelCrawler().Crawl(url, Depth + 1); }));
            }
        }


        /// <summary>Waits for the crawler to finish.</summary>
        public override void Wait()
        {
            int n = 0;

            while(_Tasks.Count > n)
            {
                n = _Tasks.Count;
                Task.WaitAll(_Tasks.ToArray());
                Thread.Sleep(0);
            }
        }
    }
}
