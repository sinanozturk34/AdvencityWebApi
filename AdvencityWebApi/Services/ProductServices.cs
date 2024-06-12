using AdvencityWebApi.Dao;
using AdvencityWebApi.Model;

namespace AdvencityWebApi.Services
{
    public class ProductServices
    {
        ProductDao productDao=new ProductDao();
        public List<ProductModel> getProductList(string filter="")
        {
            return productDao.productList(filter);
        }
        public ResponseModel addFavorite(FavoriteAddDeletePostModel favoriteAddDelete)
        {
            return  productDao.addFavorite(favoriteAddDelete);
        }
        public ResponseModel deleteFavorite(FavoriteAddDeletePostModel favoriteAddDelete)
        {
            return productDao.deleteFavorite(favoriteAddDelete);
        }
        public List<FavoriteModel> getFavoriteList(FavoriteOrderPostModel favoriteOrderPost)
        {
           return productDao.getFavoriteList(favoriteOrderPost);
        }
    }
}
