using RestraurantADMIN.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestraurantADMIN.BLL
{
    public class Service
    {
        DAL dal = new DAL();
        #region Common
        public List<T> GetGlobalSelect<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            return dal.GetGlobalSelect<T>(MainTableName, MainFieldName, PId);
        }
        public T GetGlobalSelectOne<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {

            return dal.GetGlobalSelectOne<T>(MainTableName, MainFieldName, PId);
        }
        public long GlobalDelete(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null)
        {
            return dal.GlobalDelete(MainTableName, MainFieldName, PId, TransTableName, TransFieldName);
        }
        #endregion
        #region GetLogin
        public Login Login(Login ob)
        {
            return dal.Login(ob);
        }
        #endregion
        #region Table Booking Master
        public Int64 InsUpTablerMaster(TableBookingMasters TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //SqlParameter OutPutId=new SqlParameter();
            if (TEntity.pk_intTableBookingId == 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@pk_intTableBookingId", TEntity.pk_intTableBookingId));
            }
            arrParams.Add(new SqlParameter("@strTableNo", TEntity.strTableNo));
           
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return dal.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Waiter Master
        public Int64 InsUpWaiterMaster(WaiterMaster TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //SqlParameter OutPutId=new SqlParameter();
            if (TEntity.pk_intWaiterId == 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "InsertWaiter"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@pk_intWaiterId", TEntity.pk_intWaiterId));
            }
            arrParams.Add(new SqlParameter("@strWaiterName", TEntity.strWaiterName));
            arrParams.Add(new SqlParameter("@strWaiterPhone", TEntity.strWaiterPhone));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return dal.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Food Item Category Masters
        public Int64 InsUpFoodItemCategoryMasters(FoodItemCategoryMasters TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //SqlParameter OutPutId=new SqlParameter();
            if (TEntity.pk_intFoodItemCategoryId == 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@pk_intFoodItemCategoryId", TEntity.pk_intFoodItemCategoryId));
            }
            arrParams.Add(new SqlParameter("@strCategoryName", TEntity.strCategoryName));
            arrParams.Add(new SqlParameter("@strCategoryCode", TEntity.strCategoryCode));
            arrParams.Add(new SqlParameter("@intCreatedBy", TEntity.intCreatedBy));
           
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return dal.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Food Item Master
         public Int64 InsUpFooditemMaster(FoodItemMasters TEntity, string SP_Name)
         {
             List<SqlParameter> arrParams = new List<SqlParameter>();
             //SqlParameter OutPutId=new SqlParameter();
             if (TEntity.pk_intFoodItemId == 0)
             {
                 arrParams.Add(new SqlParameter("@OppType", "InsertFoodItemMaster"));
             }
             else
             {
                 arrParams.Add(new SqlParameter("@OppType", "Update"));
                 arrParams.Add(new SqlParameter("@pk_intFoodItemId", TEntity.pk_intFoodItemId));
             }
             arrParams.Add(new SqlParameter("@bitIsAlcohol", TEntity.bitIsAlcohol));
             arrParams.Add(new SqlParameter("@bitIsJain", TEntity.bitIsJain));
             arrParams.Add(new SqlParameter("@bitIsSpicy", TEntity.bitIsSpicy));
             arrParams.Add(new SqlParameter("@bitIsVeg", TEntity.bitIsVeg));
             arrParams.Add(new SqlParameter("@decBarCharges", TEntity.decBarCharges));
             arrParams.Add(new SqlParameter("@decDeliveryRate", TEntity.decDeliveryRate));
             arrParams.Add(new SqlParameter("@decDiningRate", TEntity.decDiningRate));
             arrParams.Add(new SqlParameter("@decGSTTaxRate", TEntity.decGSTTaxRate));
             arrParams.Add(new SqlParameter("@decOptionalRate", TEntity.decOptionalRate));
             arrParams.Add(new SqlParameter("@decTakeAwayRate", TEntity.decTakeAwayRate));
             arrParams.Add(new SqlParameter("@fk_intFoodItemCategoryId", TEntity.fk_intFoodItemCategoryId));
             arrParams.Add(new SqlParameter("@strFoodItemCode", TEntity.strFoodItemCode));
             arrParams.Add(new SqlParameter("@strFoodItemName", TEntity.strFoodItemName));
             arrParams.Add(new SqlParameter("@Imagesone", TEntity.Imagesone));
             arrParams.Add(new SqlParameter("@Imagestwo", TEntity.Imagestwo));

             SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
             OutPutId.Direction = ParameterDirection.Output;
             arrParams.Add(OutPutId);
             return dal.InsertAnyMasters(arrParams, SP_Name, OutPutId);
         }

         public List<FoodItemMasters> FooditemMasterList(FoodItemMasters FooditemMaster)
         {
             return dal.FooditemMasterList(FooditemMaster);
         }
         #endregion
        #region Stock IN
         public Int64 InUpStockinMaster(StockinMaster TEntity, string SP_Name)
         {
             List<SqlParameter> arrParams = new List<SqlParameter>();
             //SqlParameter OutPutId=new SqlParameter();
             if (TEntity.StockInId != 0)
             {
                 arrParams.Add(new SqlParameter("@OppType", "InsertStockInMaster"));
             }
             else
             {
                 arrParams.Add(new SqlParameter("@OppType", "StockinUpdate"));
                 arrParams.Add(new SqlParameter("@pk_intFoodItemId", TEntity.pk_intFoodItemId));
             }
             arrParams.Add(new SqlParameter("@decTakeAwayRate", TEntity.decTakeAwayRate));
             arrParams.Add(new SqlParameter("@decDeliveryRate", TEntity.decDeliveryRate));
             arrParams.Add(new SqlParameter("@TotalFoodCount", TEntity.TotalFoodCount));


             SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
             OutPutId.Direction = ParameterDirection.Output;
             arrParams.Add(OutPutId);
             return dal.InsertAnyMasters(arrParams, SP_Name, OutPutId);
         }



         public List<StockinMaster> ListStockinFooditem(StockinMaster StockinMaster)
         {
             return dal.ListStockinFooditem(StockinMaster);
         }
         #endregion
        #region Shedule Master
         public Int64 InsertUpdateShedule(SheduleWiseFood TEntity, string SP_Name)
         {
             List<SqlParameter> arrParams = new List<SqlParameter>();
             SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
             OutPutId.Direction = ParameterDirection.Output;
             arrParams.Add(OutPutId);
             return dal.InsertAnyMasters(arrParams, SP_Name, OutPutId);
         }
         #endregion
        #region Restraurant Service
        public List<FoodItemMasters> ResturantService(FoodItemMasters FooditemMaster)
        {
            return dal.ResturantService(FooditemMaster);
        }
        public Int64 InsertUpdateRservice(RserviceBill TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            if (TEntity.pk_intRestrurentServiceId == 0)
            {
                arrParams.Add(new SqlParameter("@OppType", "INSERT"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@OppType", "UPDATE"));
                arrParams.Add(new SqlParameter("@pk_intRestrurentServiceId", TEntity.pk_intRestrurentServiceId));
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("fk_intFoodItemId", typeof(Int64));
            dt.Columns.Add("intQuantity", typeof(Int64));
            dt.Columns.Add("decRate", typeof(decimal));
            dt.Columns.Add("decDiscount", typeof(decimal));
            dt.Columns.Add("decTaxCGSTPer", typeof(decimal));
            dt.Columns.Add("decTaxCGSTAmount", typeof(decimal));
            dt.Columns.Add("decTaxSGSTPer", typeof(decimal));
            dt.Columns.Add("decTaxSGSTAmount", typeof(decimal));
            dt.Columns.Add("decNetAmount", typeof(decimal));
            //List<SqlParameter> arrParams = new List<SqlParameter>();
           // arrParams.Add(new SqlParameter("@strBillNo", TEntity.strBillNo));
            arrParams.Add(new SqlParameter("@dtServiceBillDate", TEntity.dtServiceBillDate));
            arrParams.Add(new SqlParameter("@fk_intWaiterId", TEntity.fk_intWaiterId));
            arrParams.Add(new SqlParameter("@fk_intTableBookingId", TEntity.fk_intTableBookingId));
            arrParams.Add(new SqlParameter("@strGuestName", TEntity.strGuestName));
            arrParams.Add(new SqlParameter("@decTotalAmount", TEntity.decTotalAmount));
            arrParams.Add(new SqlParameter("@decTotalTaxCGSTAmount", TEntity.decTotalTaxCGSTAmount));
            arrParams.Add(new SqlParameter("@decTotalTaxSGSTAmount", TEntity.decTotalTaxSGSTAmount));
            arrParams.Add(new SqlParameter("@decDiscountAmount", TEntity.decDiscountAmount));
            arrParams.Add(new SqlParameter("@decTotalNetAmount", TEntity.decTotalNetAmount));
            arrParams.Add(new SqlParameter("@decNetAmount", TEntity.decNetAmount));
            arrParams.Add(new SqlParameter("@intCreatedBy", TEntity.intCreatedBy));
            foreach (var item in TEntity.ListRserviceBill)
            {
                DataRow dr = dt.NewRow();
                dr["fk_intFoodItemId"] = item.fk_intFoodItemId;
                dr["intQuantity"] = item.intQuantity;
                dr["decRate"] = item.decRate;
                dr["decDiscount"] = item.decDiscount;
                dr["decTaxCGSTPer"] = item.decTaxCGSTPer;
                dr["decTaxCGSTAmount"] = item.decTaxCGSTAmount;
                dr["decTaxSGSTPer"] = item.decTaxSGSTPer;
                dr["decTaxSGSTAmount"] = item.decTaxSGSTAmount;
                dr["decNetAmount"] = item.decNetAmount; ;
                dt.Rows.Add(dr);
            }
            arrParams.Add(new SqlParameter("@FoodServiceList", dt));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return dal.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        public List<RserviceBill> ListRserviceBill(RserviceBill RserviceBill)
        {
            return dal.ListRserviceBill(RserviceBill);
        }
        public List<RserviceBill> GetFoodItemDetlsById(Int64 pk_intRestrurentServiceId)
        {
            return dal.GetFoodItemDetlsById(pk_intRestrurentServiceId);
        }
        #endregion
        #region OrderList
        public List<OrderListModel>GetCustomerOrderList(OrderListModel OrderListModel)
        {
            return dal.GetCustomerOrderList(OrderListModel);
        }
        #endregion

        #region Order History List
        public List<OrderListModel> GetCustomerOrderHistoryList(OrderListModel OrderListModel)
        {
            return dal.GetCustomerOrderHistoryList(OrderListModel);
        }
        #endregion

        #region Redy To Process
        public Int64 RedyToProcess(OrderListModel OrderListModel)
        {
            return dal.RedyToProcess(OrderListModel);
        }
        #endregion
    }
}