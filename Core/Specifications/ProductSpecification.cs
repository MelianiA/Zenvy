using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string? type, string? brand, string? sort) : base(p => (string.IsNullOrEmpty(type) || p.Type == type) && (string.IsNullOrEmpty(brand) || p.Brand == brand))
    {
        switch(sort)
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
