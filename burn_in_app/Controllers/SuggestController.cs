using burn_in_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace burn_in_app.Controllers
{
    public class SuggestController : Controller
    {
        BURN_INEntities db = new BURN_INEntities(); // declare goalbal Database

        // GET: Suggest
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult suggest_board()
        {
            return View();
        }

        public ActionResult ajax_display_suggest_board_information()
        {
            var data = db.sp_display_suggest_board_information().ToList();
            //return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

   
    }
}