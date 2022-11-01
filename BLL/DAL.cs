using RestraurantADMIN.GlobalClass;
using RestraurantADMIN.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RestraurantADMIN.BLL
{
    public class DAL
    {
        #region Sql Connection
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["RestraurantDBCon"].ConnectionString;
        }
        #endregion
        #region Common
        public List<T> GetGlobalSelect<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            arrParams.Add(new SqlParameter("@PId", PId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "GlobalSelect_SP", arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]);
        }
        public T GetGlobalSelectOne<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            arrParams.Add(new SqlParameter("@PId", PId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "GlobalSelect_SP", arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]).FirstOrDefault();
        }
        public Int64 InsertAnyMasters(List<SqlParameter> arrParams, string SP_Name, SqlParameter OutPutId)
        {

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            Int64 ID = Convert.ToInt64(OutPutId.Value);
            return Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
        }
        public List<T> GetAnyList<T>(List<SqlParameter> arrParams, string SP_Name) where T : class, new()
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]);
        }
        public long GlobalDelete(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null)
        {

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@PId", PId));
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            if (TransTableName != null) arrParams.Add(new SqlParameter("@TransTableName", TransTableName));
            if (TransFieldName != null) arrParams.Add(new SqlParameter("@TransFieldName", TransFieldName));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_GlobalDelete", arrParams.ToArray());

            long ReturnValue = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return Convert.ToInt64(ReturnValue);

        }
        #endregion
        #region Login
        public Login Login(Login obj)
        {
            //List<Login> ListObject = new List<Login>();
            Login DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@strEmail", obj.strEmail));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Login", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new Login();
                    DataObject.strEmail = Convert.ToString(rdr["strEmail"]);
                    DataObject.strUserPwd = Convert.ToString(rdr["strUserPwd"]);
                    DataObject.strUserType = Convert.ToString(rdr["strUserType"]);
                    DataObject.strUserName = Convert.ToString(rdr["strUserName"]);
                    DataObject.strUserId = Convert.ToString(rdr["strUserId"]);
                    DataObject.pk_intUserId = Convert.ToInt64(rdr["pk_intUserId"]);
                    DataObject.intViewOrder = Convert.ToInt16(rdr["intViewOrder"]);
                    DataObject.dtAddDate = Convert.ToString(rdr["dtAddDate"]);
                    DataObject.strDesignation = Convert.ToString(rdr["strDesignation"]);
                    //ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return DataObject;
        }
        #endregion
        #region Food Item Master List
        public List<FoodItemMasters> FooditemMasterList(FoodItemMasters FooditemMaster)
        {
            List<FoodItemMasters> FooditemMasterList = new List<FoodItemMasters>();
            FoodItemMasters ListObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "Getdata"));
            //if (AdminModel.UM_Id != 0)
            //{
            //    arrParams.Add(new SqlParameter("@BM_Id", BranchMaster.BM_Id));

            //}
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_InUpFooditemMaster", arrParams.ToArray());

            if (rdr != null)
            {
                while (rdr.Read())
                {
                    ListObj = new FoodItemMasters();
                    ListObj.pk_intFoodItemId = Convert.ToInt64(rdr["pk_intFoodItemId"]);
                    ListObj.strFoodItemName = Convert.ToString(rdr["strFoodItemName"]);
                    ListObj.strFoodItemCode = Convert.ToString(rdr["strFoodItemCode"]);
                    ListObj.strCategoryName = Convert.ToString(rdr["strCategoryName"]);
                    ListObj.bitIsAlcohol = Convert.ToBoolean(rdr["bitIsAlcohol"]);
                    ListObj.bitIsJain = Convert.ToBoolean(rdr["bitIsJain"]);
                    ListObj.bitIsSpicy = Convert.ToBoolean(rdr["bitIsSpicy"]);
                    ListObj.bitIsVeg = Convert.ToBoolean(rdr["bitIsVeg"]);
                    ListObj.decBarCharges = Convert.ToInt16(rdr["decBarCharges"]);
                    ListObj.decDeliveryRate = Convert.ToInt16(rdr["decDeliveryRate"]);
                    ListObj.decDiningRate = Convert.ToInt16(rdr["decDiningRate"]);
                    ListObj.decGSTTaxRate = Convert.ToInt16(rdr["decGSTTaxRate"]);
                    ListObj.decOptionalRate = Convert.ToInt16(rdr["decOptionalRate"]);
                    ListObj.decTakeAwayRate = Convert.ToInt16(rdr["decTakeAwayRate"]);
                    ListObj.Imagesone = Convert.ToString(rdr["Imagesone"]);
                    ListObj.Imagestwo = Convert.ToString(rdr["Imagestwo"]);



                    FooditemMasterList.Add(ListObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return FooditemMasterList;
        }
        #endregion
        #region StockinMaster
        public List<StockinMaster> ListStockinFooditem(StockinMaster StockinMaster)
        {
            List<StockinMaster> ListStockinFooditem = new List<StockinMaster>();
            StockinMaster ListObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "StockinGatedata"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_InUpStockInMaster", arrParams.ToArray());

            if (rdr != null)
            {
                while (rdr.Read())
                {
                    ListObj = new StockinMaster();
                    ListObj.pk_intFoodItemId = Convert.ToInt64(rdr["pk_intFoodItemId"]);
                    ListObj.strFoodItemName = Convert.ToString(rdr["strFoodItemName"]);
                    ListObj.decTakeAwayRate = Convert.ToInt64(rdr["decTakeAwayRate"]);
                    ListObj.decDeliveryRate = Convert.ToInt64(rdr["decDeliveryRate"]);
                    ListObj.TotalFoodCount = Convert.ToInt64(rdr["TotalFoodCount"]);
                    ListStockinFooditem.Add(ListObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListStockinFooditem;
        }
        #endregion
        #region Restraurant Service
        public List<FoodItemMasters> ResturantService(FoodItemMasters FooditemMaster)
        {
            List<FoodItemMasters> FooditemMasterList = new List<FoodItemMasters>();
            FoodItemMasters ListObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "Getdata"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_InUpFooditemMaster", arrParams.ToArray());

            if (rdr != null)
            {
                while (rdr.Read())
                {
                    ListObj = new FoodItemMasters();
                    ListObj.pk_intFoodItemId = Convert.ToInt64(rdr["pk_intFoodItemId"]);
                    ListObj.strFoodItemName = Convert.ToString(rdr["strFoodItemName"]);
                    ListObj.strFoodItemCode = Convert.ToString(rdr["strFoodItemCode"]);
                    ListObj.strCategoryName = Convert.ToString(rdr["strCategoryName"]);
                    ListObj.decDeliveryRate = Convert.ToInt16(rdr["decDeliveryRate"]);
                    ListObj.decDiningRate = Convert.ToInt16(rdr["decDiningRate"]);
                    ListObj.decGSTTaxRate = Convert.ToInt16(rdr["decGSTTaxRate"]);
                    ListObj.decOptionalRate = Convert.ToInt16(rdr["decOptionalRate"]);
                    ListObj.decTakeAwayRate = Convert.ToInt16(rdr["decTakeAwayRate"]);



                    FooditemMasterList.Add(ListObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return FooditemMasterList;
        }
        public List<RserviceBill> ListRserviceBill(RserviceBill RserviceBill)
        {
            List<RserviceBill> ListRserviceBill = new List<RserviceBill>();
            RserviceBill ListObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "GETALL"));
            //if (AdminModel.UM_Id != 0)
            //{
            //    arrParams.Add(new SqlParameter("@BM_Id", BranchMaster.BM_Id));

            //}
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_insRServiceBillDetails", arrParams.ToArray());

            if (rdr != null)
            {
                while (rdr.Read())
                {
                    //pk_intRestrurentServiceId, dtServiceBillDate, fk_intWaiterId, strKOTNo, strBillNo, fk_intTableBookingId, fk_intRoomId, strGuestName, decTotalAmount, decTotalTaxCGSTAmount, decTotalTaxSGSTAmount, decDiscountAmount, decTotalNetAmount, intCreatedBy, dtCreatedDate, intEditedBy, dtEditedDate, bitIsActive, SNo, fk_intCheckInOrResID, IsComplementary

                    ListObj = new RserviceBill();
                    ListObj.pk_intRestrurentServiceId = Convert.ToInt64(rdr["pk_intRestrurentServiceId"]);
                    ListObj.dtServiceBillDate = Convert.ToString(rdr["dtServiceBillDate"]);
                    ListObj.strKOTNo = Convert.ToString(rdr["strKOTNo"]);
                    ListObj.strBillNo = Convert.ToString(rdr["strBillNo"]);
                    ListObj.fk_intTableBookingId = Convert.ToInt64(rdr["fk_intTableBookingId"]);
                    ListObj.strGuestName = Convert.ToString(rdr["strGuestName"]);
                    ListObj.decTotalAmount = Convert.ToDecimal(rdr["decTotalAmount"]);


                    ListRserviceBill.Add(ListObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListRserviceBill;
        }
        public List<RserviceBill> GetFoodItemDetlsById(Int64 pk_intRestrurentServiceId)
        {
            List<RserviceBill> ListRserviceBill = new List<RserviceBill>();
            RserviceBill ListObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@OppType", "GetFoodItemDetlsById"));
            arrParams.Add(new SqlParameter("@fk_intRestrurentServiceId", pk_intRestrurentServiceId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_insRServiceBillDetails", arrParams.ToArray());

            if (rdr != null)
            {
                while (rdr.Read())
                {

                    ListObj = new RserviceBill();
                    ListObj.fk_intFoodItemId = Convert.ToInt64(rdr["fk_intFoodItemId"]);
                    ListObj.strFoodItemName = Convert.ToString(rdr["strFoodItemName"]);
                    ListObj.intQuantity = Convert.ToInt64(rdr["intQuantity"]);
                    ListObj.decRate = Convert.ToDecimal(rdr["decRate"]);
                    ListObj.decDiscount = Convert.ToDecimal(rdr["decDiscount"]);
                    ListObj.decTaxCGSTPer = Convert.ToDecimal(rdr["decTaxCGSTPer"]);
                    ListObj.decTaxSGSTPer = Convert.ToDecimal(rdr["decTaxSGSTPer"]);
                    ListObj.decNetAmount = Convert.ToDecimal(rdr["decNetAmount"]);
                    ListObj.pk_intRestrurentServiceDetailsID = Convert.ToInt64(rdr["pk_intRestrurentServiceDetailsID"]);
                    ListObj.fk_intRestrurentServiceId = Convert.ToInt64(rdr["fk_intRestrurentServiceId"]);
                    ListObj.decTaxCGSTAmount = Convert.ToDecimal(rdr["decTaxCGSTAmount"]);
                    ListObj.pk_intRestrurentServiceId = Convert.ToInt64(rdr["pk_intRestrurentServiceId"]);
                    ListObj.dtServiceBillDate = Convert.ToString(rdr["dtServiceBillDate"]);
                    ListObj.fk_intWaiterId = Convert.ToInt64(rdr["fk_intWaiterId"]);
                    ListObj.strKOTNo = Convert.ToString(rdr["strKOTNo"]);
                    ListObj.strBillNo = Convert.ToString(rdr["strBillNo"]);
                    ListObj.fk_intTableBookingId = Convert.ToInt64(rdr["fk_intTableBookingId"]);
                    ListObj.fk_intRoomId = Convert.ToInt64(rdr["fk_intRoomId"]);
                    ListObj.strGuestName = Convert.ToString(rdr["strGuestName"]);
                    ListObj.decTotalAmount = Convert.ToDecimal(rdr["decTotalAmount"]);
                    ListObj.decTotalTaxCGSTAmount = Convert.ToDecimal(rdr["decTotalTaxCGSTAmount"]);
                    ListObj.decTotalTaxSGSTAmount = Convert.ToDecimal(rdr["decTotalTaxSGSTAmount"]);
                    ListObj.decTotalTaxSGSTAmount = Convert.ToDecimal(rdr["decTotalTaxSGSTAmount"]);
                    ListObj.decDiscountAmount = Convert.ToDecimal(rdr["decDiscountAmount"]);
                    ListObj.decTotalNetAmount = Convert.ToDecimal(rdr["decTotalNetAmount"]);
                    ListObj.IsComplementary = Convert.ToString(rdr["IsComplementary"]);
                    ListRserviceBill.Add(ListObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListRserviceBill;
        }

        #endregion
        #region Recent OrderList
        public List<OrderListModel> GetCustomerOrderList(OrderListModel OrderListModel)
        {
            OrderListModel obj = null;
            List<OrderListModel> OrderListModelList = new List<OrderListModel>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //arrParams.Add(new SqlParameter("@strEmail", OrderListModel.strEmail));
            arrParams.Add(new SqlParameter("@OppType", "GetInstantOrderList"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_OrderList", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    //[FoodItemId],[Qty],[TotalRate],[StockOutDate],[strFoodItemName], GuestId, FirstName, LastName, Company, Street, LandMark, Zip, 
                    //StateId, CountryId, Telephone, Email,[strStateName],[strCountryName]OutOfDelivery,RedyToProcess
                    obj = new OrderListModel();
                    obj.FoodItemId = Convert.ToInt64(rdr["FoodItemId"]);
                    obj.StockOutId = Convert.ToInt64(rdr["StockOutId"]);
                    obj.Qty = Convert.ToInt16(rdr["Qty"]);
                    obj.TotalRate = Convert.ToInt16(rdr["TotalRate"]);
                    obj.StockOutDate = Convert.ToString(rdr["StockOutDate"]);
                    obj.strFoodItemName = Convert.ToString(rdr["strFoodItemName"]);
                    obj.GuestId = Convert.ToInt64(rdr["GuestId"]);
                    obj.FirstName = Convert.ToString(rdr["FirstName"]);
                    obj.LastName = Convert.ToString(rdr["LastName"]);
                    obj.Company = Convert.ToString(rdr["Company"]);
                    obj.Street = Convert.ToString(rdr["Street"]);
                    obj.LandMark = Convert.ToString(rdr["LandMark"]);
                    obj.Zip = Convert.ToString(rdr["Zip"]);
                    obj.StateId = Convert.ToInt16(rdr["StateId"]);
                    obj.CountryId = Convert.ToInt16(rdr["CountryId"]);
                    obj.Telephone = Convert.ToString(rdr["Telephone"]);
                    obj.Email = Convert.ToString(rdr["Email"]);
                    obj.strStateName = Convert.ToString(rdr["strStateName"]);
                    obj.strCountryName = Convert.ToString(rdr["strCountryName"]);
                    obj.RedyToProcess = Convert.ToInt16(rdr["RedyToProcess"]);
                    obj.OutOfDelivery = Convert.ToInt16(rdr["OutOfDelivery"]);
                    OrderListModelList.Add(obj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return OrderListModelList;
        }
        #endregion

        #region Order History List
        public List<OrderListModel> GetCustomerOrderHistoryList(OrderListModel OrderListModel)
        {
            OrderListModel obj = null;
            List<OrderListModel> OrderListModelList = new List<OrderListModel>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //arrParams.Add(new SqlParameter("@strEmail", OrderListModel.strEmail));
            arrParams.Add(new SqlParameter("@OppType", "GetOrderHistoryList"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_OrderList", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    //[FoodItemId],[Qty],[TotalRate],[StockOutDate],[strFoodItemName], GuestId, FirstName, LastName, Company, Street, LandMark, Zip, 
                    //StateId, CountryId, Telephone, Email,[strStateName],[strCountryName]OutOfDelivery,RedyToProcess
                    obj = new OrderListModel();
                    obj.FoodItemId = Convert.ToInt64(rdr["FoodItemId"]);
                    obj.StockOutId = Convert.ToInt64(rdr["StockOutId"]);
                    obj.Qty = Convert.ToInt16(rdr["Qty"]);
                    obj.TotalRate = Convert.ToInt16(rdr["TotalRate"]);
                    obj.StockOutDate = Convert.ToString(rdr["StockOutDate"]);
                    obj.strFoodItemName = Convert.ToString(rdr["strFoodItemName"]);
                    obj.GuestId = Convert.ToInt64(rdr["GuestId"]);
                    obj.FirstName = Convert.ToString(rdr["FirstName"]);
                    obj.LastName = Convert.ToString(rdr["LastName"]);
                    obj.Company = Convert.ToString(rdr["Company"]);
                    obj.Street = Convert.ToString(rdr["Street"]);
                    obj.LandMark = Convert.ToString(rdr["LandMark"]);
                    obj.Zip = Convert.ToString(rdr["Zip"]);
                    obj.StateId = Convert.ToInt16(rdr["StateId"]);
                    obj.CountryId = Convert.ToInt16(rdr["CountryId"]);
                    obj.Telephone = Convert.ToString(rdr["Telephone"]);
                    obj.Email = Convert.ToString(rdr["Email"]);
                    obj.strStateName = Convert.ToString(rdr["strStateName"]);
                    obj.strCountryName = Convert.ToString(rdr["strCountryName"]);
                    obj.RedyToProcess = Convert.ToInt16(rdr["RedyToProcess"]);
                    obj.OutOfDelivery = Convert.ToInt16(rdr["OutOfDelivery"]);
                    OrderListModelList.Add(obj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return OrderListModelList;
        }
        #endregion

        #region Redy To Process
        public Int64 RedyToProcess(OrderListModel OrderListModel)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (OrderListModel.FoodItemId >0)
            {
                arrParams.Add(new SqlParameter("@OppType", "OutOfDelivery"));
            }
            else
            {
                if (OrderListModel.OppType!=null)
                {
                    arrParams.Add(new SqlParameter("@OppType", OrderListModel.OppType));
                }
                else
                {
                    arrParams.Add(new SqlParameter("@OppType", "RedyToProcess"));
                }
            }
            arrParams.Add(new SqlParameter("@StockOutId", OrderListModel.StockOutId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_OrderList", arrParams.ToArray());

            long ReturnValue = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return Convert.ToInt64(ReturnValue);
        }
        #endregion

        #region DELETE TABLE
        public Int64 DeleteTable(TableBookingMasters OBJ)
        {
            Int64 Status = 0;
            SqlConnection Con = new SqlConnection(GetConnectionString());
            Con.Open();
            SqlCommand Commend = new SqlCommand("Sp_TableBooking", Con);
            Commend.CommandType = CommandType.StoredProcedure;
            Commend.Parameters.Add(new SqlParameter("@pk_intTableBookingId", OBJ.pk_intTableBookingId));
            Commend.Parameters.Add(new SqlParameter("@OppType", "DELETETABLEBYID"));
            SqlParameter OutPutId=new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            Commend.Parameters.Add(OutPutId);
            Commend.ExecuteNonQuery();
            Status = Convert.ToInt64(OutPutId.Value);
            return Status;
        }
        #endregion
    }
}