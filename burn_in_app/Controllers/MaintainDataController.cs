using burn_in_app.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace burn_in_app.Controllers
{
    public class MaintainDataController : Controller
    {
        BURN_INEntities db = new BURN_INEntities(); // declare goalbal Database

        // GET: MaintainData
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult maintain_bi_board_master()
        {
            return View();
        }

        public ActionResult maintain_bi_board_model_master()
        {
            return View();
        }

        public ActionResult maintain_mc_model_bi_master()
        {
            return View();
        }

        public ActionResult maintain_mc_bi_master()
        {
            return View();
        }

        public ActionResult ajax_display_board_master()
        {
            var data = db.sp_display_maintain_board_master().ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult ajax_display_board_socket(string get_board_no)
        //{
        //    var data = db.sp_display_maintain_board_socket().Where(x => x.BI_BOARD_NO == get_board_no).ToList();
        //    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult ajax_get_board_status()
        {
            //var data = (from s in db.BI_BORD_MST
            //             join c in db.BI_BOARD_STATUS_MST on s.BI_BOARD_STATUS equals c.BI_BOARD_STATUS
            //  select new { c.BI_BOARD_STATUS,c.BI_BOARD_STAUS_NAME }).Distinct();
            var data = db.BI_BOARD_STATUS_MST
                    .Select(x => new { x.BI_BOARD_STATUS,x.BI_BOARD_STAUS_NAME})
                    .Distinct()
                    .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ajax_get_board_model_master()
        {
            var data = db.BI_BOARD_MODEL_PRODUCT_MST
                       .Select(x => x.BI_BOARD_MODEL)
                       .Distinct()
                       .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ajax_display_board_model_master()
        {
            var data = db.BI_BOARD_MODEL_PRODUCT_MST.ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ajax_add_board_model_master(BI_BOARD_MODEL_PRODUCT_MST add_board_model_master)
        {
                string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["dbinlineEntities2"].ConnectionString;
                SqlConnection cnn = new SqlConnection(cnnString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cnn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_add_board_model_master";
                cmd.Parameters.AddWithValue("@BI_BOARD_MODEL", add_board_model_master.BI_BOARD_MODEL);
                cmd.Parameters.AddWithValue("@PRODUCT_TYPE", add_board_model_master.PRODUCT_TYPE);
                cmd.Parameters.AddWithValue("@BI_BOARD_COORDINATE_X", add_board_model_master.BI_BOARD_COORDINATE_X);
                cmd.Parameters.AddWithValue("@BI_BOARD_COORDINATE_Y", add_board_model_master.BI_BOARD_COORDINATE_Y);
                cnn.Open();
                object o = cmd.ExecuteNonQuery();
                cnn.Close();
                return new EmptyResult();                   
        }

        [HttpPost]
        public ActionResult ajax_edit_board_model_master(BI_BOARD_MODEL_PRODUCT_MST edit_board_model_master)
        {
            var result = db.BI_BOARD_MODEL_PRODUCT_MST.SingleOrDefault(b => b.BI_BOARD_MODEL_NO == edit_board_model_master.BI_BOARD_MODEL_NO);
            if (result != null)
            {
                result.BI_BOARD_MODEL = edit_board_model_master.BI_BOARD_MODEL;
                result.PRODUCT_TYPE = edit_board_model_master.PRODUCT_TYPE;
                result.BI_BOARD_COORDINATE_X = edit_board_model_master.BI_BOARD_COORDINATE_X;
                result.BI_BOARD_COORDINATE_Y = edit_board_model_master.BI_BOARD_COORDINATE_Y;
                db.SaveChanges();
            }
            return new EmptyResult();
            //db.BI_BOARD_MODEL_PRODUCT_MST.Add(edit_board_model_master);
            //ctx.Entry(myBook).State = System.Data.Entity.EntityState.Modified;
            //ctx.SaveChanges();

        }

        [HttpPost]
        public ActionResult ajax_add_board_master(BI_BORD_MST add_board_master)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["dbinlineEntities2"].ConnectionString;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_add_board_master";
            cmd.Parameters.AddWithValue("@BI_BOARD_NO", add_board_master.BI_BOARD_NO);
            cmd.Parameters.AddWithValue("@BI_BOARD_MODEL", add_board_master.BI_BOARD_MODEL);
            cmd.Parameters.AddWithValue("@BI_BOARD_REGISTRATION_DATE", add_board_master.BI_BOARD_REGISTRATION_DATE);
            cmd.Parameters.AddWithValue("@BI_BOARD_LAST_PM", add_board_master.BI_BOARD_LAST_PM);
            //cmd.Parameters.AddWithValue("@BI_BOARD_LIFE_TIME", add_board_master.BI_BOARD_LIFE_TIME);
            cmd.Parameters.AddWithValue("@LIFE_TIME", add_board_master.LIFE_TIME);
            cmd.Parameters.AddWithValue("@BI_BOARD_STATUS", add_board_master.BI_BOARD_STATUS);
            cmd.Parameters.AddWithValue("@BI_BOARD_OPARATE", add_board_master.BI_BOARD_OPARATE);
            cnn.Open();
            object o = cmd.ExecuteNonQuery();
            cnn.Close();
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult ajax_edit_board_master(BI_BORD_MST edit_board_master)
        {
            var result = db.BI_BORD_MST.SingleOrDefault(b => b.BI_BOARD_NO_RUN == edit_board_master.BI_BOARD_NO_RUN);
            if (result != null)
            {
                result.BI_BOARD_NO = edit_board_master.BI_BOARD_NO;
                result.BI_BOARD_MODEL = edit_board_master.BI_BOARD_MODEL;
                result.LIFE_TIME = edit_board_master.LIFE_TIME;
                result.BI_BOARD_REGISTRATION_DATE = edit_board_master.BI_BOARD_REGISTRATION_DATE;
                result.BI_BOARD_LAST_PM = edit_board_master.BI_BOARD_LAST_PM;
                result.BI_BOARD_LIFE_TIME = edit_board_master.BI_BOARD_LIFE_TIME;
                result.BI_BOARD_STATUS = edit_board_master.BI_BOARD_STATUS;
                result.BI_BOARD_OPARATE = edit_board_master.BI_BOARD_OPARATE;
                db.SaveChanges();
            }
            return new EmptyResult();
            //db.BI_BOARD_MODEL_PRODUCT_MST.Add(edit_board_model_master);
            //ctx.Entry(myBook).State = System.Data.Entity.EntityState.Modified;
            //ctx.SaveChanges();
        }

        public ActionResult ajax_display_mc_model_master()
        {
            var data = db.BI_MC_MODEL_MST.ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ajax_add_mc_model_master(BI_MC_MODEL_MST add_model_mc_master)
        {

            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["dbinlineEntities2"].ConnectionString;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_add_mc_model_master";
            cmd.Parameters.AddWithValue("@BI_MC_MODEL", add_model_mc_master.BI_MC_MODEL);
            cmd.Parameters.AddWithValue("@BI_MC_CAPACITY_BOARD", add_model_mc_master.BI_MC_CAPACITY_BOARD);
            cnn.Open();
            object o = cmd.ExecuteNonQuery();
            cnn.Close();
            return new EmptyResult();
        }

        public ActionResult ajax_display_mc_master()
        {
            var data = (from s in db.BI_MC_MST
                        join c in db.BI_MC_MODEL_MST on s.BI_MC_MODEL equals c.BI_MC_MODEL
                        select new { s.BI_MC_NO_RUN ,s.BI_MC_NO, s.BI_MC_MODEL, c.BI_MC_CAPACITY_BOARD }).Distinct();
                      return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ajax_add_mc_master(BI_MC_MST add_mc_master)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["dbinlineEntities2"].ConnectionString;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_add_mc_master";
            cmd.Parameters.AddWithValue("@BI_MC_NO", add_mc_master.BI_MC_NO);
            cmd.Parameters.AddWithValue("@BI_MC_MODEL", add_mc_master.BI_MC_MODEL);
            cnn.Open();
            object o = cmd.ExecuteNonQuery();
            cnn.Close();
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult ajax_edit_mc_master(BI_MC_MST edit_mc_master)
        {
            var result = db.BI_MC_MST.SingleOrDefault(b => b.BI_MC_NO_RUN == edit_mc_master.BI_MC_NO_RUN);
            if (result != null)
            {
                result.BI_MC_NO = edit_mc_master.BI_MC_NO;
                result.BI_MC_MODEL = edit_mc_master.BI_MC_MODEL;
                DateTime today = DateTime.Today;
                result.UPDATE_DATE = today;
                db.SaveChanges();
            }
            return new EmptyResult();
        }


        public ActionResult ajax_get_mc_model_master()
        {
            var data = db.BI_MC_MODEL_MST
                       .Select(x => x.BI_MC_MODEL)
                       .Distinct()
                       .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ajax_edit_mc_model_master(BI_MC_MODEL_MST edit_mc_model_master)
        {
            var result = db.BI_MC_MODEL_MST.SingleOrDefault(b => b.BI_MC_MODEL_RUN_NO == edit_mc_model_master.BI_MC_MODEL_RUN_NO);
            if (result != null)
            {
                result.BI_MC_MODEL = edit_mc_model_master.BI_MC_MODEL;
                result.BI_MC_CAPACITY_BOARD = edit_mc_model_master.BI_MC_CAPACITY_BOARD;
                DateTime today = DateTime.Today;
                result.UPDATE_DATE = today;
                db.SaveChanges();
            }
            return new EmptyResult();
         }

        public ActionResult ajax_get_product_type_list()
        {
            var data = db.BI_PRODUCT_TYPE_LIST_MST
                  .Select(x => new {x.PRODUCT_TYPE_AXIS ,x.PRODUCT_TYPE_CONVERT} )
                  .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
            //return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ajax_get_board_dupicate()
        {

            var data = from s in db.BI_BORD_MST
                          select new
                          {
                              BI_BOARD_NO = s.BI_BOARD_NO,
                              //OrderTitle = s.EventType,
                              //OrderDate = s.Title
                          };
            //var data = db.BI_BORD_MST
            //      .Select(x => x.BI_BOARD_NO)
            //      .ToList();
            //return Json(new { data = results }, JsonRequestBehavior.AllowGet);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ajax_board_model_socket_qty()
        {
            var data = from s in db.BI_BOARD_MODEL_PRODUCT_MST
                       select new
                       {
                           BI_BOARD_MODEL = s.BI_BOARD_MODEL,
                           BI_BOARD_COORDINATE_X = s.BI_BOARD_COORDINATE_X,
                           BI_BOARD_COORDINATE_Y = s.BI_BOARD_COORDINATE_Y
                       };
            var datas = data.Where(x => x.BI_BOARD_MODEL == "IMX587-AACK");
            return Json(new { data = datas }, JsonRequestBehavior.AllowGet);
        }
      
    }
}