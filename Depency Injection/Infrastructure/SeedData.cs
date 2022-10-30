using Depency_Injection.Models;
using Microsoft.EntityFrameworkCore;

namespace Depency_Injection.Infrastructure;

public class SeedData
{
    public static void SeedDatabase(DataContext context)
    {
        context.Database.Migrate();

        if(context.Products.Count() == 0 && context.Categories.Count() == 0)
        {
            Category fruits = new Category { Name = "fruits" };
            Category shirts = new Category { Name = "shirts" };

            context.Products.AddRange(
                new Product
                {
                    Name = "Blue shirt",
                    Price = 2.5M,
                    Category = shirts
                });

        }
    }
}
