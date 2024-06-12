using AdvencityWebApi.Model;
using AdvencityWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvencityWebApi.Controllers
{
    public class ProductController : Controller
    {
        ProductServices productServices = new ProductServices();
        private readonly IConfiguration configuration;
        public ProductController(IConfiguration conf)
        {
            this.configuration = conf;
            DBUtil.DBUtil.conf = conf;
        }
        [Route("getProductList")]
        [HttpGet]
        public ActionResult<List<ProductModel>> getProductList()
        {
            return productServices.getProductList();
        }
        [Route("getProductList")]
        [HttpPost]
        public ActionResult<List<ProductModel>> getProductList(ProductFilterPostModel productFilterPost)
        {
            return productServices.getProductList(productFilterPost.PARAMS + " "+ productFilterPost.ORDER);
        }
        [Route("addFavorite")]
        [HttpPost]
        public ActionResult<ResponseModel> addFavorite(FavoriteAddDeletePostModel favoriteAddDelete)
        {
            return productServices.addFavorite(favoriteAddDelete);
        }
        [Route("deleteFavorite")]
        [HttpPost]
        public ActionResult<ResponseModel> deleteFavorite(FavoriteAddDeletePostModel favoriteAddDelete)
        {
            return productServices.deleteFavorite(favoriteAddDelete);
        }
        [Route("getFavorite")]
        [HttpPost]
        public ActionResult<List<FavoriteModel>> getFavorite(FavoriteOrderPostModel favoriteOrderPost)
        {
            return productServices.getFavoriteList(favoriteOrderPost);
        }
    }
}
