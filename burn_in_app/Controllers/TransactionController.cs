using burn_in_app.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace burn_in_app.Controllers
{
    public class TransactionController : Controller
    {
        BURN_INEntities db = new BURN_INEntities(); // declare goalbal

        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult transaction_operation()
        {
            return View();
        }

        public ActionResult ajax_get_display_operation_transaction()
        {
            var data = db.sp_display_transaction_operation()
                 .Select(x => new { x.MC_NUMBER,x.LOT_EDP,x.TYPE_NAME,x.BI_TRANSATUS_NAME,x.CREATE_DATE,x.START_BI_DATE,x.END_BI_DATE,x.CREATE_BY})
                 .Distinct()
                 .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ajax_get_display_operation_transaction_by_board(string param_mc_name,string param_lot_edp)
        {
            var data = db.sp_display_transaction_operation()
                .Where(x => x.MC_NUMBER == param_mc_name && x.LOT_EDP == param_lot_edp)
                .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ajax_get_mc_condition_db(string type_name)
        {
            var data = db.BI_MC_CONDITION_DB_MST
                    .Where(x=> x.TYPE_NAME == type_name)
                    //.Select(x => new { x.BI_MC_NO })
                    .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult update_operation_start_bi(BI_OPERATION_TRANSACTION confirm_start_bi_data)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["dbinlineEntities2"].ConnectionString;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_update_operation_bi";
            cmd.Parameters.AddWithValue("@LOT_EDP", confirm_start_bi_data.LOT_EDP);
            cmd.Parameters.AddWithValue("@MC_NUMBER", confirm_start_bi_data.MC_NUMBER);
            cmd.Parameters.AddWithValue("@TYPE_NAME", confirm_start_bi_data.TYPE_NAME);
            cmd.Parameters.AddWithValue("@FLAG_TRAN", "START_BI");

            cnn.Open();
            object o = cmd.ExecuteNonQuery();
            cnn.Close();
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult update_operation_end_bi(BI_OPERATION_TRANSACTION end_bi_data)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["dbinlineEntities2"].ConnectionString;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_update_operation_bi";
            cmd.Parameters.AddWithValue("@LOT_EDP", end_bi_data.LOT_EDP);
            cmd.Parameters.AddWithValue("@MC_NUMBER", end_bi_data.MC_NUMBER);
            cmd.Parameters.AddWithValue("@TYPE_NAME", "");
            cmd.Parameters.AddWithValue("@FLAG_TRAN", "END_BI");

            cnn.Open();
            object o = cmd.ExecuteNonQuery();
            cnn.Close();
            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult update_life_time_board_usage_end_bi(BI_OPERATION_TRANSACTION end_bi_data)
        {
            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["dbinlineEntities2"].ConnectionString;
            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "sp_update_lifetime_board_usage";
            cmd.Parameters.AddWithValue("@LOT_EDP", end_bi_data.LOT_EDP);
            cmd.Parameters.AddWithValue("@MC_NUMBER", end_bi_data.MC_NUMBER);
            cmd.Parameters.AddWithValue("@TYPE_NAME", "");
            cmd.Parameters.AddWithValue("@FLAG_TRAN", "END_BI");

            cnn.Open();
            object o = cmd.ExecuteNonQuery();
            cnn.Close();
            return new EmptyResult();
        }

        public ActionResult ajax_get_mc_mst()
        {
            var data = db.BI_MC_MST
                    .Select(x => new { x.BI_MC_NO })
                    .Distinct()
                    .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ajax_get_bi_operation_status()
        {
            var data = db.BI_TRANSACTION_STATUS_MST
                    .Select(x => new { x.BI_TRAN_STATUS_ID, x.BI_TRANSATUS_NAME })
                    .Distinct()
                    .ToList();
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);

        }

    }
}