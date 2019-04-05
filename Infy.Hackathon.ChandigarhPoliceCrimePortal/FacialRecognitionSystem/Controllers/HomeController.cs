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
            var bucket = "bhavesh-aws-bucket";
            var photo = "recognition/BM.jpg";
            var photoToBeAdded = "download-2.jpg";

            if (CreateCollection.Collection(collectionId).Equals("OK"))
            {
                AddFaces.FaceAddition(collectionId, bucket, photoToBeAdded);
            }
            else
            {
                return "Error occurred";
            }
            return SearchFacesMatchingImage.RecogniseFaceMatchFromS3(collectionId, bucket, photo);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
