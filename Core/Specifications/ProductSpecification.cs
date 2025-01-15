using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams productParams) :
        base(p => (productParams.Brands.Count == 0 || productParams.Brands.Contains(p.Brand)) &&
                  (productParams.Types.Count == 0 || productParams.Types.Contains(p.Type)))
    {
        switch (productParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }

}
