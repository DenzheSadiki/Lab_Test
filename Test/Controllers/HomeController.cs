using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                StreamReader csvreader = new StreamReader(model.File.InputStream);
                List<int> csvValues = new List<int>();

                while (!csvreader.EndOfStream)
                {
                    string line = csvreader.ReadLine();
                    string[] character = line.Split(';');
                    csvValues.Add(int.Parse(character[0].ToString()));
                }

                int mostOccuringValue = csvValues
                                        .GroupBy(i => i)
                                        .OrderByDescending(grp => grp.Count())
                                        .Select(grp => grp.Key).First();

                double numberOfTimesValueOccurred = csvValues.Where(x => x.Equals(mostOccuringValue))
                                        .Select(x => x)
                                        .Count();

                double threshhold = csvValues.Count() / 2;

                if (numberOfTimesValueOccurred > threshhold)
                {
                    ViewBag.Message = "The Leading Value is  " + mostOccuringValue;
                }
                else
                {

                    ViewBag.Message = "Result : - 1";
                }

                return View();

            }
            catch (System.Exception ex)
            {
                ErrorLog logger = new ErrorLog();

                logger.logError(ex);

                return View();

            }
        }
    }
}