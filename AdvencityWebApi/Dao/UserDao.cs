using System.Data;
using AdvencityWebApi.Model;
using Oracle.ManagedDataAccess.Client;

namespace AdvencityWebApi.Dao
{
    public class UserDao
    {
        public static IConfiguration conf = DBUtil.DBUtil.conf;
        public ResponseModel userLogin(UserModel user)
        {
            ResponseModel response = new ResponseModel();
            string loginUserQuery = @"DECLARE"
                        + "\n     P_USER_NAME   VARCHAR2 (50);"
                        + "\n     P_PASSWORD    VARCHAR2 (50);"
                        + "\n     P_MESSAGE     VARCHAR2 (50);"
                        + "\n     P_STATU       VARCHAR2 (50);"
                        + "\n     BEGIN"
                        + "\n     P_USER_NAME:='"+user.USERCODE+"';"
                        + "\n     P_PASSWORD :='"+user.PASSWORD+"';"
                        + "\n     C##SINANPROJE.USERS_PKG.P_USER_CONTROL (P_USER_NAME,"
						+ "\n     P_PASSWORD,"
                        + "\n     P_MESSAGE,"
                        + "\n     P_STATU);"
                         + "\n    SELECT P_MESSAGE,P_STATU INTO :error_message,:error_code  FROM DUAL;"
                        + "\n     END;";
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    cnn.Open();
                    using (OracleCommand command = new OracleCommand(loginUserQuery, cnn))
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
        public ResponseModel resetPassword(UserModel user)
        {
            ResponseModel response = new ResponseModel();
            string resetPasswordQuery = @"DECLARE"
                        + "\n     P_USER_NAME   VARCHAR2 (50);"
                        + "\n     P_PASSWORD    VARCHAR2 (50);"
                        + "\n     P_MESSAGE     VARCHAR2 (50);"
                        + "\n     P_STATU       VARCHAR2 (50);"
                        + "\n     BEGIN"
                        + "\n     P_USER_NAME:='" + user.USERCODE + "';"
                        + "\n     P_PASSWORD :='" + user.PASSWORD + "';"
                        + "\n     C##SINANPROJE.USERS_PKG.P_User_Update (P_USER_NAME,"
						+ "\n     P_PASSWORD,"
                        + "\n     P_MESSAGE,"
                        + "\n     P_STATU);"
                         + "\n    SELECT P_MESSAGE,P_STATU INTO :error_message,:error_code  FROM DUAL;"
                        + "\n     END;";
            try
            {
                using (var cnn = new OracleConnection(conf.GetConnectionString("OracleConStr")))
                {
                    cnn.Open();
                    using (OracleCommand command = new OracleCommand(resetPasswordQuery, cnn))
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
