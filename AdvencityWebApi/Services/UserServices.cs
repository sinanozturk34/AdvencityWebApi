using AdvencityWebApi.Dao;
using AdvencityWebApi.Model;

namespace AdvencityWebApi.Services
{
    public class UserServices
    {
        UserDao userDao=new UserDao();
        public ResponseModel userLogin(UserModel user)
        {
            return userDao.userLogin(user);
        }
        public ResponseModel resetPassword(UserModel user)
        {
            return userDao.resetPassword(user);

        }
    }
}
