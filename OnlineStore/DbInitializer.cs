using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Data;
using OnlineStore.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OnlineStore
{
    public static class DbInitializer
    {
        public static async Task Initialize(DataContext context)
        {
            var dairy = new ProductType("dairy", 0);
            var meat = new ProductType("meat", 0);
            var drinks = new ProductType("harmful drinks", 12);
            var fruit = new ProductType("fruit", 0);
            var vegetables = new ProductType("vegetables", 0);
            var bakery = new ProductType("bakery products", 0);
            var alcohol = new ProductType("vegetables", 21);
            var tobacco = new ProductType("vegetables", 21);
            //____________________________________________________________________________

            var milk = new Product() {
                    Name = "Milk", Description = "Cow milk",
                    Price = 2.69M, Discount  = 0,
                    Quantity = 20, ProductType = dairy
            };
            var beef = new Product() {
                    Name = "Beef", Description = "Cow meat",
                    Price = 5.92M, Discount  = 0,
                    Quantity = 20, ProductType = meat
            };
            var coke = new Product() {
                    Name = "Coca-Cola", Description = "Dissolves rust",
                    Price = 2.26M, Discount  = 0,
                    Quantity = 20, ProductType = drinks
            };
            var orange = new Product() {
                    Name = "Orange", Description = "Juicy fruit",
                    Price = 1.58M, Discount  = 0,
                    Quantity = 20, ProductType = fruit
            };
            var tomato = new Product() {
                    Name = "Tomato", Description = "Almost like a potato but a tomato",
                    Price = 4.50M, Discount  = 0,
                    Quantity = 20, ProductType = vegetables
            };
            var bread = new Product() {
                    Name = "Bread", Description = "A loaf of bread",
                    Price = 0.84M, Discount  = 0,
                    Quantity = 20, ProductType = bakery
            };
            var whisky = new Product() {
                    Name = "Jack Daniel's", Description = "Whisky",
                    Price = 22, Discount  = 0,
                    Quantity = 20, ProductType = alcohol
            };
            var marlboro = new Product() {
                    Name = "Marlboro", Description = "Cigarettes",
                    Price = 2.26M, Discount  = 0,
                    Quantity = 20, ProductType = tobacco
            };
            //____________________________________________________________________________

            var adminUser = new User()
            {
                Email = "admin@gmail.com",
                Password = "admin1234",
                Name = "Evgeny",
                DateOfBirth = new DateTime(2002, 11, 14),
                Role = Role.Admin,
            };

            var Ivan = new User()
            {
                Email = "ivan@gmail.com",
                Password = "ivan1234",
                Name = "Ivan",
                DateOfBirth = new DateTime(1990, 1, 20),
                Money = 1000,
                Role = Role.Customer,
                Orders = null,
            };

            var Max = new User()
            {
                Email = "max@gmail.com",
                Password = "max1234",
                Name = "Max",
                DateOfBirth = new DateTime(2008, 7, 20),
                Money = 1000,
                Role = Role.Customer,
                Orders = null,
            };
            //____________________________________________________________________________

            var IvanOrder1 = new Order()
            {
                User = Ivan,
                //Products = new List<Product>() { whisky, coke },
                DeliveryAddress = "Moskow",
                TotalPrice = whisky.Price + coke.Price
            };

            var IvanOrder2 = new Order()
            {
                User = Ivan,
                //Products = new List<Product>() { marlboro, milk },
                DeliveryAddress = "Saint Petersburg",
                TotalPrice = marlboro.Price + milk.Price
            };

            var MaxOrder1 = new Order()
            {
                User = Max,
                //Products = new List<Product>() { bread, tomato, orange },
                DeliveryAddress = "New York",
                TotalPrice = bread.Price + tomato.Price + orange.Price
            };

            var MaxOrder2 = new Order()
            {
                User = Max,
                //Products = new List<Product>() { tomato, coke, milk },
                DeliveryAddress = "Washington, D.C.",
                TotalPrice = tomato.Price + coke.Price + milk.Price
            };
            //____________________________________________________________________________

            if (!context.ProductTypes.Any()) {
                context.ProductTypes.AddRange(new ProductType[]
                    { dairy, meat, drinks, fruit, vegetables, bakery, alcohol, tobacco }
                );
            }

            if (!context.Products.Any()) {
                context.Products.AddRange(new Product[]
                    { marlboro, whisky, bread, tomato, orange, coke, beef, milk }
                );
            }

            if (!context.Users.Any()) {
                context.Users.AddRange(new User[] { adminUser, Ivan, Max });
            }

            if (!context.Orders.Any()) {
                context.Orders.AddRange(new Order[] { IvanOrder1, IvanOrder2, MaxOrder1, MaxOrder2 });
            }

            if (!context.OrderProducts.Any()) {
                context.OrderProducts.AddRange(
                    new OrderProduct() { Order = IvanOrder1 , Product = whisky},
                    new OrderProduct() { Order = IvanOrder1 , Product = coke},
                    new OrderProduct() { Order = IvanOrder2 , Product = marlboro },
                    new OrderProduct() { Order = IvanOrder2 , Product = milk },
                    new OrderProduct() { Order = MaxOrder1, Product = bread },
                    new OrderProduct() { Order = MaxOrder1, Product = tomato },
                    new OrderProduct() { Order = MaxOrder1, Product = orange },
                    new OrderProduct() { Order = MaxOrder2, Product = tomato },
                    new OrderProduct() { Order = MaxOrder2, Product = coke},
                    new OrderProduct() { Order = MaxOrder2, Product = milk }
                );
            }
            
            await context.SaveChangesAsync();
        }
    }
}
