using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedData(ApplicationDbContext context){

        if(!context.Products.Any()){
            var productsData = System.IO.File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products == null) return;
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }

}
