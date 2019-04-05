using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FacialRecognitionSystem.Models;
using FacialRecognitionSystem.Services;

namespace FacialRecognitionSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string AuthoritySearch()
        {
            var collectionId = "PoliceAuthorityCollection";
            var collectionCreated = false;
            if (CreateCollection.Collection(collectionId).Equals("OK"))
            {
                collectionCreated = true;
            }

            if(collectionCreated)
            {
                return AddFaces.FaceAddition(collectionId);
            }
            return SearchFacesMatchingImage.RecogniseFaceMatch(collectionId);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
