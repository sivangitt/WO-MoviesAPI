using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
namespace WOOMDBApi.Models
{
    public class MovieDetail
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }

        public string Awards { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }

        public List<Rating> Ratings { get; set; }

        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        public string imdbVotes { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string DVD { get; set; }
        public string BoxOffice { get; set; }
        public string Production { get; set; }
        public string Website { get; set; }
        public string Response { get; set; }

        public MovieDetail Results { get; set; }

        public class Rating
        {
            public string Source { get; set; }
            public string Value { get; set; }
        }


        public MovieDetail LoadMovieDetail(string searchMovieID)
        {
            //string apiKey = "c279cfec";
            string baseUri = $"http://www.omdbapi.com/?apikey=c279cfec&i=" + searchMovieID + "&plot=full";

            //string baseUri = "$"+string.Format("https://www.omdbapi.com/?apikey={0}&s={1}", apiKey, string.IsNullOrEmpty(searchMovieTitle)?string.Empty: searchMovieTitle);
            //string name = "maniac";
            //string type = "series";

            var sb = new StringBuilder(baseUri);
           
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
                            if (!string.IsNullOrEmpty(result))
                            {

                                JObject parsedJson = JObject.Parse(result);
                                if (parsedJson != null)
                                {
                                    foreach (var item in parsedJson)
                                    {
                                        Results = new MovieDetail();
                                        Results.Title = parsedJson["Title"].ToString();
                                        Results.Year = parsedJson["Year"].ToString();
                                        Results.Rated = parsedJson["Rated"].ToString();
                                        Results.Released = parsedJson["Released"].ToString();
                                        Results.Runtime = parsedJson["Runtime"].ToString();
                                        Results.Genre = parsedJson["Genre"].ToString();
                                        Results.Director = parsedJson["Director"].ToString();
                                        Results.Writer = parsedJson["Writer"].ToString();
                                        Results.Actors = parsedJson["Actors"].ToString();
                                        Results.Plot = parsedJson["Plot"].ToString();
                                        Results.Language = parsedJson["Language"].ToString();
                                        Results.Country = parsedJson["Country"].ToString();
                                        Results.Poster = parsedJson["Poster"].ToString();
                                        Results.Metascore = parsedJson["Metascore"].ToString();
                                        Results.imdbRating = parsedJson["imdbRating"].ToString();
                                        Results.imdbVotes = parsedJson["imdbVotes"].ToString();
                                        Results.imdbID = parsedJson["imdbID"].ToString();
                                        Results.Type = parsedJson["Type"].ToString();
                                        Results.DVD = parsedJson["DVD"].ToString();
                                        Results.BoxOffice = parsedJson["BoxOffice"].ToString();
                                        Results.Production = parsedJson["Production"].ToString();
                                        Results.Website = parsedJson["Website"].ToString();
                                        Results.Response = parsedJson["Response"].ToString();

                                        List<Rating> ratinglist = new List<Rating>();
                                        if (parsedJson["Ratings"] != null)
                                        {
                                            foreach (var rat in parsedJson["Ratings"].Children())
                                            {
                                                Rating rt = new Rating();
                                                rt.Source = rat["Source"].ToString();
                                                rt.Value = rat["Value"].ToString();
                                                ratinglist.Add(rt);
                                            }
                                        }
                                        Results.Ratings = ratinglist;
                                    }
                                   
                                    return Results;

                                }

                                return null;

                            }

                            return null;

                        }
                    }
                }
            }
            catch (WebException e)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
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