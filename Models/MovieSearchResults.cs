
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;


namespace WOOMDBApi.Models
{
    public class MovieSearchResults
    {
        [Required]
        public string searchMovieTitle { get; set; }
        public string searchMovieYear { get; set; }
        public int totalPageCount { get; set; }

        public int CurrentPageIndex { get; set; }
        public IList<MovieSearchResult> Results { get; set; }

        public void LoadSearchResults(string searchMovieTitle, string searchMovieYear, string pageIndex)
        {
            //string apiKey = "c279cfec";


            string apiKey = ConfigurationManager.AppSettings["omdbAPIKey"];
            string baseUri = $"http://www.omdbapi.com/?apikey=" + apiKey;

            if (!string.IsNullOrEmpty(searchMovieYear))
            {
                baseUri += "&y=" + searchMovieYear;
            }
            if (!string.IsNullOrEmpty(searchMovieTitle))
            {
                baseUri += "&s=" + searchMovieTitle;
            }

            if (!string.IsNullOrEmpty(pageIndex))
            {
                baseUri += "&p=" + pageIndex;
            }


            //string baseUri = "$"+string.Format("https://www.omdbapi.com/?apikey={0}&s={1}", apiKey, string.IsNullOrEmpty(searchMovieTitle)?string.Empty: searchMovieTitle);
            //string name = "maniac";
            //string type = "series";

            var sb = new StringBuilder(baseUri);
            //sb.Append($"&s={name}");
            //sb.Append($"&type={type}");

            var request = WebRequest.Create(sb.ToString());
            request.Timeout = 1000;
            request.Method = "GET";
            request.ContentType = "application/json";

            string result = string.Empty;

            try
            {
                using (var response = request.GetResponse())
                {

                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                        {
                           
                            result = reader.ReadToEnd();
                            if(!string.IsNullOrEmpty(result))
                            {
                                JObject parsedJson = JObject.Parse(result);
                                if(parsedJson != null && parsedJson["Search"].Count()>0)
                                {
                                    JArray a = (JArray)parsedJson["Search"];
                                    Results = a.ToObject<IList<MovieSearchResult>>();
                                    
                                    double pageCount = (double)((decimal)Convert.ToInt32(parsedJson["totalResults"].ToString()) / Convert.ToDecimal(5));

                                    Console.WriteLine();
                                    totalPageCount = (int)Math.Ceiling(pageCount);
                                    CurrentPageIndex = 1;
                                }
                                

                                
                            }
                            
                            
                           
                        }
                    }
                }
            }
            catch (WebException e)
            {
                WriteToLog("WebException" + e.ToString());
            }
            catch (Exception e)
            {
                WriteToLog("Exception" + e.ToString());
            }

          
        }
        #region WritetoLog
        public void WriteToLog(string Message)
        {
            StreamWriter log;
            string filename = AppDomain.CurrentDomain.BaseDirectory + "logfile.txt";
            if (!System.IO.File.Exists(filename))
            {
                log = new StreamWriter(filename);
            }
            else
            {
                log = System.IO.File.AppendText(filename);
            }

            //Write to the file:
            log.WriteLine(DateTime.Now);
            log.WriteLine(Message);
            log.WriteLine();

            // Close the stream:    
            log.Close();
        }
        #endregion

    }

}