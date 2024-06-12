using AdvencityWebApi.Model;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace AdvencityWebApi.Dao
{
    public class ProductDao
    {
        public static IConfiguration conf = DBUtil.DBUtil.conf;
        public List<ProductModel> productList(string filter = "")
        {
            List<ProductModel> productList = new List<ProductModel>();
            string productQuery = @"SELECT MP.MAIN_CATEGORY_NAME,
                                   SP.SUB_CATEGORY_NAME,
                                   P.PRODUCT_ID,
                                   P.PRODUCT_CODE,
                                   P.PRODUCT_NAME,
                                   PR.UNIT_PRICE
                              FROM C##SINANPROJE.MAIN_PRODUCT_CATEGORIES  MP,
                                   C##SINANPROJE.SUB_PRODUCT_CATEGORIES   SP,
                                   C##SINANPROJE.PRODUCTS                 P,
                                   C##SINANPROJE.PRODUCT_PRICES           PR
                             WHERE     MP.MAIN_CATEGORY_ID = SP.MAIN_CATEGORY_ID
                                   AND P.PRODUCT_ID = PR.PRODUCT_ID
                                   AND P.SUB_CATEGORY_ID = SP.SUB_CATEGORY_ID";
            if (!String.IsNullOrWhiteSpace(filter))
            {
                productQuery += "\nORDER BY "+filter;
            }
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    productList = cnn.Query<ProductModel>(productQuery).ToList();
                }
            }
            catch (Exception ex)
            {

                productList = new List<ProductModel>();
            }
            return productList;
        }
        public List<FavoriteModel> getFavoriteList(FavoriteOrderPostModel favoriteOrderPost)
        {
            List<FavoriteModel> favorites = new List<FavoriteModel>();
            string favoriteQuery = @"SELECT FP.PRODUCT_CODE, FP.PRODUCT_NAME, PP.UNIT_PRICE, COUNT(PRODUCT_NAME)
                                        FROM C##SINANPROJE.FAVORITE_PRODUCTS FP, C##SINANPROJE.PRODUCT_PRICES PP
                                       WHERE FP.PRODUCT_ID = PP.PRODUCT_ID
                                    GROUP BY FP.PRODUCT_CODE, FP.PRODUCT_NAME, PP.UNIT_PRICE
                                    ORDER BY COUNT(PRODUCT_NAME) ";
            if (!String.IsNullOrWhiteSpace(favoriteOrderPost.TYPE_))
            {
                favoriteQuery += "\n" + favoriteOrderPost.TYPE_;
            }
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    favorites = cnn.Query<FavoriteModel>(favoriteQuery).ToList();
                }
            }
            catch (Exception ex)
            {

                favorites = new List<FavoriteModel>();
            }
            return favorites;
        }
        public ResponseModel addFavorite(FavoriteAddDeletePostModel favoriteAddDelete)
        {
            ResponseModel response = new ResponseModel();
            string addFavorite = @"DECLARE                                             "
                         +"\n               P_PRODUCT_ID   NUMBER;                            "
                         +"\n               P_USER_ID      NUMBER;                            "
                         +"\n               P_MESSAGE      VARCHAR2 (50);                     "
                         +"\n               P_STATU        VARCHAR2 (50);                     "
                         +"\n           BEGIN                                                 "
                         +"\n               P_PRODUCT_ID := "+favoriteAddDelete.PRODUCT_ID+";                                "
                         + "\n               P_USER_ID := "+favoriteAddDelete.USER_ID+";                                   "
                         + "\n               C##SINANPROJE.FAVORITE_PKG.P_FAVORITE_INSERT (P_PRODUCT_ID,"
						 + "\n                                                       P_USER_ID,"
                         +"\n                                                       P_MESSAGE,"
                         +"\n                                                       P_STATU);"
                         + "\n    SELECT P_MESSAGE,P_STATU INTO :error_message,:error_code  FROM DUAL;"
                         + "\n     END;";
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    cnn.Open();
                    using (OracleCommand command = new OracleCommand(addFavorite, cnn))
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
        public ResponseModel deleteFavorite(FavoriteAddDeletePostModel favoriteAddDelete)
        {
            ResponseModel response = new ResponseModel();
            string deleteFavorite = @"DECLARE                                             "
                         + "\n               P_PRODUCT_ID   NUMBER;                            "
                         + "\n               P_USER_ID      NUMBER;                            "
                         + "\n               P_MESSAGE      VARCHAR2 (50);                     "
                         + "\n               P_STATU        VARCHAR2 (50);                     "
                         + "\n           BEGIN                                                 "
                         + "\n               P_PRODUCT_ID := " + favoriteAddDelete.PRODUCT_ID + ";                                "
                         + "\n               P_USER_ID := " + favoriteAddDelete.USER_ID + ";                                   "
                         + "\n               C##SINANPROJE.FAVORITE_PKG.P_FAVORITE_DELETE (P_PRODUCT_ID,"
						 + "\n                                                       P_USER_ID,"
                         + "\n                                                       P_MESSAGE,"
                         + "\n                                                       P_STATU);"
                         + "\n    SELECT P_MESSAGE,P_STATU INTO :error_message,:error_code  FROM DUAL;"
                         + "\n     END;";
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    cnn.Open();
                    using (OracleCommand command = new OracleCommand(deleteFavorite, cnn))
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
    }
}
