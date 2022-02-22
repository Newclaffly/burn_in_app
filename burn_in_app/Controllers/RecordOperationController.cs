using burn_in_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace burn_in_app.Controllers
{
    public class RecordOperationController : Controller
    {

        BURN_INEntities db = new BURN_INEntities(); // declare goalbal
        // GET: RecordOperation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult operation_record()
        {
            return View();
        }

        public ActionResult ajax_get_mc_mst()
        {
            var data = db.BI_MC_MST
                    .Select(x => new { x.BI_MC_NO })
                    .Distinct()
                    .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ajax_get_board_status(string board_no_temp)
        {
            var data = db.BI_BORD_MST
                .Select(x => new { x.BI_BOARD_NO, x.BI_BOARD_STATUS})
                .Where(x => x.BI_BOARD_NO == board_no_temp)
                .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult insert_transaction_operation_bi(List<BI_OPERATION_TRANSACTION> covers)
        {
            BI_OPERATION_TRANSACTION cover = new BI_OPERATION_TRANSACTION();
            using (BURN_INEntities entities = new BURN_INEntities())
            {

                foreach (BI_OPERATION_TRANSACTION cover_page in covers)
                {
                    entities.BI_OPERATION_TRANSACTION.Add(cover_page);
                }
                int insertedRecords = entities.SaveChanges();
                return Json(insertedRecords, JsonRequestBehavior.AllowGet);
            }
        }// END INSERT


    }
}