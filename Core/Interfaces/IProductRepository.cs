using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(int id);
    Task<IReadOnlyList<Product?>> GetProductsAsync();
    Task<IReadOnlyList<string?>> GetTypesAsync();
    Task<IReadOnlyList<string?>> GetBrandsAsync();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}
