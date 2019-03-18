using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace FrontEndApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ConvertNumber(string number)
        {
            try {
                string result;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51881/api/numberconverter/"+number);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = client.GetStringAsync(number);
                    var output = JsonConvert.DeserializeObject<object>(response.Result);
                    result = output.ToString();
                    return Json(result, JsonRequestBehavior.AllowGet);

                }
            }catch(Exception ex){

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

           

        }

    }
}