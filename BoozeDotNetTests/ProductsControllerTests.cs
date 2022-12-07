using BoozeDotNet.Controllers;
using BoozeDotNet.Data;
using BoozeDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoozeDotNetTests
{
    [TestClass]
    public class ProductsControllerTests
    {
        // db var at class level for use in all tests
        private ApplicationDbContext context;
        ProductsController controller;

        // set up code that runs automatically before each unit test
        [TestInitialize]
        public void TestInitialize()
        {
            // must instantiate in memory db to pass as a dependency when creating an instance of ProductsController
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);

            // seed the db before passing it to controller
            var category = new Category { CategoryId = 74, Name = "Some Category " };
            context.Add(category);

            for (var i = 30; i < 40; i++)
            {
                var product = new Product { ProductId = i, Name = "Product " + i.ToString(), CategoryId = 74, Category = category, Price = i + 10 };
                context.Add(product);
            }

            var extraProduct = new Product { ProductId = 65, Name = "ABC Product", CategoryId = 74, Category = category, Price = 38 };
            context.Add(extraProduct);
            context.SaveChanges();

            controller = new ProductsController(context);
        }
    }
}
