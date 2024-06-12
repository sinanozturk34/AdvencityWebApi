using AdvencityWebApi.Model;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace AdvencityWebApi.Dao
{
    public class OrderDao
    {
        public static IConfiguration conf = DBUtil.DBUtil.conf;
        public ResponseModel addBasket(AddBasketPostModel addBasket)
        {
            ResponseModel response = new ResponseModel();
            string addBasketQuery = @"DECLARE                                         "
                                    +"\nP_PRODUCT_ID   NUMBER;                        "
                                    +"\nP_QUANTITY     NUMBER;                        "
                                    +"\nP_USER_ID      NUMBER;                        "
                                    +"\nP_MESSAGE      VARCHAR2 (500);                 "
                                    +"\nP_STATU        VARCHAR2 (500);                 "
                                    +"\nBEGIN                                         "
                                    +"\nP_PRODUCT_ID := "+addBasket.PRODUCT_ID+";                            "
                                    + "\nP_QUANTITY := "+addBasket.QUANTITY+";                              "
                                    + "\nP_USER_ID := "+addBasket.USER_ID+";                               "
                                    + "\n C##SINANPROJE.BASKET_PKG.P_BASKET_INSERT (P_PRODUCT_ID,"
                                                                     +"\nP_QUANTITY,"
                                                                     +"\nP_USER_ID,"
                                                                     +"\nP_MESSAGE,"
                                                                     +"\nP_STATU);"
                         + "\n    SELECT P_MESSAGE,P_STATU INTO :error_message,:error_code  FROM DUAL;"
                        + "\n     END;";
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    cnn.Open();
                    using (OracleCommand command = new OracleCommand(addBasketQuery, cnn))
                    {
                        OracleParameter resultParameter = new OracleParameter("error_message", OracleDbType.Varchar2, 4000);
                        OracleParameter resultParameter2 = new OracleParameter("error_code", OracleDbType.Varchar2, 4000);
                        resultParameter.Direction = ParameterDirection.Output;
                        resultParameter2.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultParameter);
                        command.Parameters.Add(resultParameter2);
                        command.ExecuteNonQuery();
                        response.MESSAGE = resultParameter.Value.ToString();
                        response.STATU = int.Parse(resultParameter2.Value.ToString());

                    }
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {

                response.STATU = 0;
                response.MESSAGE = ex.Message;
            }
            return response;
        }
        public ResponseModel deleteBasket(DeleteBasketPostModel deleteBasket)
        {
            ResponseModel response = new ResponseModel();
            string deleteBasketQuery = @"DECLARE                                         "
                                    + "\nP_PRODUCT_ID   NUMBER;                        "
                                    + "\nP_USER_ID      NUMBER;                        "
                                    + "\nP_MESSAGE      VARCHAR2 (500);                 "
                                    + "\nP_STATU        VARCHAR2 (500);                 "
                                    + "\nBEGIN                                         "
                                    + "\nP_PRODUCT_ID := " + deleteBasket.PRODUCT_ID + ";                            "
                                    + "\nP_USER_ID := " + deleteBasket.USER_ID + ";                               "
                                    + "\n C##SINANPROJE.BASKET_PKG.P_BASKET_DELETE (P_PRODUCT_ID,"
																	 + "\nP_USER_ID,"
                                                                     + "\nP_MESSAGE,"
                                                                     + "\nP_STATU);"
                         + "\n    SELECT P_MESSAGE,P_STATU INTO :error_message,:error_code  FROM DUAL;"
                        + "\n     END;";
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    cnn.Open();
                    using (OracleCommand command = new OracleCommand(deleteBasketQuery, cnn))
                    {
                        OracleParameter resultParameter = new OracleParameter("error_message", OracleDbType.Varchar2, 4000);
                        OracleParameter resultParameter2 = new OracleParameter("error_code", OracleDbType.Varchar2, 4000);
                        resultParameter.Direction = ParameterDirection.Output;
                        resultParameter2.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultParameter);
                        command.Parameters.Add(resultParameter2);
                        command.ExecuteNonQuery();
                        response.MESSAGE = resultParameter.Value.ToString();
                        response.STATU = int.Parse(resultParameter2.Value.ToString());

                    }
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {

                response.STATU = 0;
                response.MESSAGE = ex.Message;
            }
            return response;
        }
        public ResponseModel basketToOrder(BasketToOrderPostModel basketToOrderPost)
        {
            ResponseModel response = new ResponseModel();
            string basketToOrderQuery = @"DECLARE"
                        + "\n               P_ORDER_ID      NUMBER;                        "
                        + "\n               P_USER_ID      NUMBER;                        "
                         +"\n               P_MESSAGE      VARCHAR2 (500);                 "
                         +"\n               P_STATU        VARCHAR2 (500);                 "
                         +"\n           BEGIN                                             "
                         + "\n               P_ORDER_ID := " + basketToOrderPost.ORDER_ID + "; "
						 + "\n               P_USER_ID := " + basketToOrderPost.USER_ID + "; "
						 + "\n               C##SINANPROJE.PURCHASE_PKG.P_PURCHASE_INSERT (P_ORDER_ID,"
						 + "\n                                                     P_USER_ID,"
                         + "\n                                                    P_MESSAGE,"
                         +"\n                                                     P_STATU);"
                         + "\n    SELECT P_MESSAGE,P_STATU INTO :error_message,:error_code  FROM DUAL;"
                         + "\n     END;";
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    cnn.Open();
                    using (OracleCommand command = new OracleCommand(basketToOrderQuery, cnn))
                    {
                        OracleParameter resultParameter = new OracleParameter("error_message", OracleDbType.Varchar2, 4000);
                        OracleParameter resultParameter2 = new OracleParameter("error_code", OracleDbType.Varchar2, 4000);
                        resultParameter.Direction = ParameterDirection.Output;
                        resultParameter2.Direction = ParameterDirection.Output;
                        command.Parameters.Add(resultParameter);
                        command.Parameters.Add(resultParameter2);
                        command.ExecuteNonQuery();
                        response.MESSAGE = resultParameter.Value.ToString();
                        response.STATU = int.Parse(resultParameter2.Value.ToString());

                    }
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {

                response.STATU = 0;
                response.MESSAGE = ex.Message;
            }
            return response;
        }
		public List<BasketHistory> getBasketHistory(BasketHistoryPostModel basketHistoryPost)
		{
			List<BasketHistory> basketList = new List<BasketHistory>();
			string basketHistoryQuery = @"SELECT PRODUCT_CODE,
                                               PRODUCT_NAME,
                                               QUANTITY,
                                               UNIT_PRICE,
                                               TOTAL_LINE_PRICE
                                          FROM BASKET_ORDERS
                                         WHERE USER_ID =" + basketHistoryPost.USER_ID + "";

			try
			{
				using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
				{
					basketList = cnn.Query<BasketHistory>(basketHistoryQuery).ToList();
				}
			}
			catch (Exception ex)
			{

				basketList = new List<BasketHistory>();
			}
			return basketList;
		}

		public List<OrderHistory> getOrderHistory(OrderHistoryPostModel orderHistoryPost)
		{
			List<OrderHistory> orderList = new List<OrderHistory>();
			string orderHistoryQuery = @"SELECT P.ORDER_NUMBER,
                                       PD.PRODUCT_CODE,
                                       PD.PRODUCT_NAME,
                                       PD.QUANTITY,
                                       PD.UNIT_PRICE,
                                       PD.QUANTITY * PD.UNIT_PRICE     TOTAL_LINE_PRICE,
                                       PD.CREATION_DATE
                                       FROM C##SINANPROJE.PURCHASES P, C##SINANPROJE.PURCHASE_DETAILS PD
                                       WHERE P.ORDER_ID = PD.ORDER_ID AND P.USER_ID=" + orderHistoryPost.USER_ID + "";

			try
			{
				using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
				{
					orderList = cnn.Query<OrderHistory>(orderHistoryQuery).ToList();
				}
			}
			catch (Exception ex)
			{

				orderList = new List<OrderHistory>();
			}
			return orderList;
		}
		public string getOrderHistoryCSV(OrderHistoryPostModel orderHistoryPost)
        {
            List<OrderHistory> orderList = new List<OrderHistory>();
            string file = "";
            string orderHistoryQuery = @"SELECT P.ORDER_NUMBER,
                                       PD.PRODUCT_CODE,
                                       PD.PRODUCT_NAME,
                                       PD.QUANTITY,
                                       PD.UNIT_PRICE,
                                       PD.QUANTITY * PD.UNIT_PRICE     TOTAL_LINE_PRICE,
                                       PD.CREATION_DATE
                                       FROM C##SINANPROJE.PURCHASES P, C##SINANPROJE.PURCHASE_DETAILS PD
                                       WHERE P.ORDER_ID = PD.ORDER_ID AND P.USER_ID=" + orderHistoryPost.USER_ID + "";

            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    orderList = cnn.Query<OrderHistory>(orderHistoryQuery).ToList();
                }
                if (orderList.Count>0)
                {
                    file = WriteCsv(orderList, "CSVFiles/" + orderHistoryPost.USER_ID + ".csv");
                }
                
            }
            catch (Exception ex)
            {

                orderList = new List<OrderHistory>();
            }
            return System.IO.Directory.GetCurrentDirectory()+"/"+file;
        }
        public  string WriteCsv<T>(List<T> list, string filePath)
        {
            
            using (var writer = new StreamWriter(filePath))
            {
                // Başlık satırını yazma
                var properties = typeof(T).GetProperties();
                writer.WriteLine(string.Join(";", properties.Select(p => p.Name)));

                // Veri satırlarını yazma
                foreach (var item in list)
                {
                    writer.WriteLine(string.Join(";", properties.Select(p => p.GetValue(item, null))));
                }
            }
            return filePath;
        }
    }
}
