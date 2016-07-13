using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.DynamicLinq;

namespace KendoUIApp2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        

        private static List<Summary> _db = new List<Summary>()
        {
            new Summary {Id= 1,state = 1, pool_name = "pool1", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 2,state = 1, pool_name = "pool2", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 3,state = 1, pool_name = "pool3", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 4,state = 1, pool_name = "pool4", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 5,state = 1, pool_name = "pool5", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 6,state = 1, pool_name = "pool6", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 7,state = 1, pool_name = "pool7", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 8,state = 1, pool_name = "pool8", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 9,state = 1, pool_name = "pool9", submitter = "jimmy", created = new DateTime(2012,1,1)},
            new Summary {Id= 10,state = 1, pool_name = "pool10", submitter = "jimmy", created = new DateTime(2012,1,1)}
        };

        [HttpPost]
        public ActionResult GetData( DataSourceRequest request, DateTime startDate , DateTime endDate, int state = -1, string poolName = null, string submitter = null)
        {
            poolName = string.IsNullOrEmpty(poolName) ? null : poolName;
            submitter = string.IsNullOrEmpty(submitter) ? null : submitter;

            
            var rslt = _db.Where(k => k.created >= startDate && k.created <= endDate)
                     .Where(k => state != -1 ? k.state == state : k.state > state)
                     .Where(k => poolName != null ? k.pool_name == poolName : k.pool_name != null)
                     .Where(k => submitter != null ? k.submitter == submitter : k.submitter != null);


            return Json(rslt.AsQueryable().ToDataSourceResult(request.Take, request.Skip, request.Sort, request.Filter));
        }
    }

    public class Summary
    {

        public int Id { get; set; }

        //See here for list of answers
        public int state { get; set; }

        public DateTime created { get; set; }
        public string pool_name { get; set; }
        public string submitter { get; set; }
    }

}
