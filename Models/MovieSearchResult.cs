using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOOMDBApi.Models
{
    public class MovieSearchResult
    {
        public string SearchTitle { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }

        public string Type { get; set; }

       
        [JsonProperty(PropertyName = "Poster")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "totalResults")]
        public int ResultsCount { get; set; }

       
    }
}