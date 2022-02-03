using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WOOMDBApi.Models;

namespace WOOMDBApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new MovieSearchResults();
            
            model.LoadSearchResults(string.Empty, string.Empty, string.Empty);
            //model.LoadSampleData();
            return View(model);
        }
    

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult SearchResults(MovieSearchResults model)
        {
            if (ModelState.IsValid)
            {
                
                string strTitle = model.searchMovieTitle;
                string strYear = model.searchMovieYear;
                string pageIndex = model.CurrentPageIndex==0?string.Empty:model.CurrentPageIndex.ToString();
                if (model.CurrentPageIndex>0)
                {
                    pageIndex = model.CurrentPageIndex.ToString();
                }
                

                model.LoadSearchResults(strTitle,strYear,pageIndex);
               
            }

            return View("Index", model);
        }
       
        public ActionResult MovieDetails(string Id)
        {
            ViewBag.Message = "MovieDetails";
           
            var model = new MovieDetail();
            
            model = model.LoadMovieDetail(Id);
            //SetResponseHeaders();
            
            return View(model);
           

            
        }

    }
}