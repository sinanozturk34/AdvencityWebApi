using AdvencityWebApi.Model;
using AdvencityWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvencityWebApi.Controllers
{
    public class OrderController : Controller
    {
        
        private readonly IConfiguration configuration;
        OrderServices orderServices=new OrderServices();
        public OrderController(IConfiguration conf)
        {
            this.configuration = conf;
            DBUtil.DBUtil.conf = conf;
        }
        [Route("addBasket")]
        [HttpPost]
        public ActionResult<ResponseModel> addBasket(AddBasketPostModel addBasket)
        {
            return orderServices.addBasket(addBasket);
        }
        [Route("deleteBasket")]
        [HttpPost]
        public ActionResult<ResponseModel> deleteBasket(DeleteBasketPostModel deleteBasket)
        {
            return orderServices.deleteBasket(deleteBasket);
        }
        [Route("createOrder")]
        [HttpPost]
        public ActionResult<ResponseModel> createOrder(BasketToOrderPostModel orderPost)
        {
            return orderServices.basketToOrder(orderPost);
        }
		[Route("getOrderHistory")]
		[HttpPost]
		public ActionResult<List<OrderHistory>> getOrderHistory(OrderHistoryPostModel orderHistory)
		{
			return orderServices.getOrderHistory(orderHistory);
		}

		[Route("getBasketHistory")]
		[HttpPost]
		public ActionResult<List<BasketHistory>> getBasketHistory(BasketHistoryPostModel basketHistory)
		{
			return orderServices.getBasketHistory(basketHistory);
		}

		[Route("exportCSV")]
        [HttpPost]
        public ActionResult<string> exportCSV(OrderHistoryPostModel orderHistory)
        {
            return orderServices.getOrderHistoryCSV(orderHistory);
        }
    }
}
