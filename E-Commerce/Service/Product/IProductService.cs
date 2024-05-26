using E_Commerce.ViewModel;

namespace E_Commerce.Service.Product
{
    public interface IProductService
    {
        Task<List<ProductCreateViewModel>> GetAllProductAsync();
    }
}
