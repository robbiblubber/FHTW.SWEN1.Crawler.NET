using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FHTW.SWEN1.WebCrawler
{
    public static class CrawlerTimer
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static properties                                                                                 //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Start time.</summary>
        public static DateTime StartTime
        {
            get; private set;
        }

        /// <summary>End time.</summary>
        public static DateTime EndTime
        {
            get; private set;
        }


        /// <summary>Gets the time between start and end of measurement.</summary>
        public static TimeSpan TimeElapsed
        {
            get { return EndTime - StartTime; }
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static methods                                                                                    //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Starts measuring time.</summary>
        public static void Start()
        {
            StartTime = DateTime.Now;
        }


        /// <summary>Stops measuring time.</summary>
        public static void Stop()
        {
            EndTime = DateTime.Now;
        }
    }
}
