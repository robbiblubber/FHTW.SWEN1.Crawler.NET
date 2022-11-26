using System;
using System.Collections.Concurrent;



namespace FHTW.SWEN1.WebCrawler
{
    /// <summary>This class cumulates crawler results.</summary>
    public static class CrawlerResult
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private static members                                                                                   //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Visited links.</summary>
        //private static Dictionary<string, int> _Visited = new();
        private static ConcurrentDictionary<string, int> _Visited = new();



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static properties                                                                                 //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets an array of visited URLs.</summary>
        public static IReadOnlyDictionary<string, int> Visited
        {
            get
            {
                lock(_Visited) { return _Visited; }
            }
        }

        /// <summary>Gets the number of visited sites.</summary>
        public static int VisitedCount
        {
            get { return _Visited.Count; }
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static methods                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Checks if an URL has been visited and visits it if it hasn't.</summary>
        /// <param name="url">URL.</param>
        /// <returns>Returns TRUE for previously unvisited URLS, otherwise returns FALSE.</returns>
        public static bool TryVisiting(string url)
        {
            lock(_Visited)
            {
                if(_Visited.ContainsKey(url))
                {
                    _Visited[url]++;
                    return false;
                }
                while(!_Visited.TryAdd(url, 1)) Thread.Sleep(0);
            }

            return true;
        }


        /// <summary>Dumps the crawler results.</summary>
        public static void Dump()
        {
            foreach(KeyValuePair<string, int> i in _Visited.OrderBy(m => m.Key).OrderByDescending(m => m.Value))
            {
                Console.WriteLine(i.Value.ToString().PadLeft(4, ' ') + " " + i.Key);
            }
        }
    }
}
