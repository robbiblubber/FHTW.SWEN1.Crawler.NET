using System;
using System.Text.RegularExpressions;



namespace FHTW.SWEN1.WebCrawler
{
    /// <summary>This class implements a crawler.</summary>
    public abstract class Crawler: ICrawler
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // protected members                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Web page HTML.</summary>
        protected string _Page = "";

        /// <summary>URL.</summary>
        protected string _Url = "";

        /// <summary>URL base.</summary>
        protected string _UrlBase = "";



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Creates a new instance of this class.</summary>
        public Crawler()
        {}



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public static properties                                                                                 //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets or sets the maximum crawling depth.</summary>
        public static int MaxDepth
        {
            get; set;
        } = 2;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public properties                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Gets or sets the crawled URL.</summary>
        public string URL
        {
            get { return _Url; }
            set
            {
                _Url = value;
                
                if(value.EndsWith("/"))
                {
                    _UrlBase = value;
                }
                else if(value.Count(m => m == '/') == 2)
                {
                    _UrlBase = (value + '/');
                }
                else { _UrlBase = value.Substring(0, value.LastIndexOf('/') + 1); }
            }
        }


        /// <summary>Gets or sets the current depth.</summary>
        public int Depth
        {
            get; set;
        } = 0;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // public methods                                                                                           //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Crawls the URL.</summary>
        /// <param name="url">URL.</param>
        /// <param name="depth">Current depth.</param>
        public virtual void Crawl(string url, int depth)
        {
            URL = url;
            Depth = depth;

            _GetPage();

            foreach(string i in _GetLinks())
            {
                if(CrawlerResult.TryVisiting(i)) _CrawlInto(i);
            }
        }


        /// <summary>Waits for the crawler to finish.</summary>
        public virtual void Wait()
        {}



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // protected methods                                                                                        //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Crawls an URL at the next depth level.</summary>
        /// <param name="uel">Target URL.</param>
        protected abstract void _CrawlInto(string url);


        /// <summary>Retreives the web page from the URL.</summary>
        protected void _GetPage()
        {
            using(HttpClient cl = new HttpClient())
            {
                try
                {
                    _Page = cl.GetStringAsync(URL).Result;
                }
                catch(Exception) {}                                             // retreival failed, _Page stays empty
            }
        }


        /// <summary>Makes an URL from a link.</summary>
        /// <param name="stub">Location stub as referred in the link.</param>
        /// <returns>URL.</returns>
        protected string _MakeURL(string stub)
        {
            if(stub.StartsWith("http://"))  return stub;
            if(stub.StartsWith("https://")) return stub;
            
            return _UrlBase + stub.TrimStart('/');
        }


        /// <summary>Gets a list of the links contained in the page.</summary>
        /// <returns>List of links.</returns>
        protected List<string> _GetLinks()
        {
            List<string> rval = new List<string>();

            Regex exp = new Regex("(?<=<a\\s*?href=(?:'|\"))[^'\"]*?(?=(?:'|\"))");

            foreach(Match i in exp.Matches(_Page))
            {
                rval.Add(_MakeURL(i.Value));
            }

            return rval;
        }
    }
}
