using AdvencityWebApi.Dao;
using AdvencityWebApi.Model;

namespace AdvencityWebApi.Services
{
    public class OrderServices
    {
        OrderDao orderDao = new OrderDao();
        public ResponseModel addBasket(AddBasketPostModel addBasket)
        {
            return orderDao.addBasket(addBasket);
        }
        public ResponseModel deleteBasket(DeleteBasketPostModel deleteBasket)
        {
            return orderDao.deleteBasket (deleteBasket);
        }
        public ResponseModel basketToOrder(BasketToOrderPostModel basketToOrderPost)
        {
            return orderDao.basketToOrder(basketToOrderPost);
        }
		public List<OrderHistory> getOrderHistory(OrderHistoryPostModel orderHistoryPost)
		{
			return orderDao.getOrderHistory(orderHistoryPost);
		}

		public List<BasketHistory> getBasketHistory(BasketHistoryPostModel basketHistoryPost)
		{
			return orderDao.getBasketHistory(basketHistoryPost);
		}

		public string getOrderHistoryCSV(OrderHistoryPostModel orderHistoryPost)
        {
            return orderDao.getOrderHistoryCSV(orderHistoryPost);
        }
    }
}
