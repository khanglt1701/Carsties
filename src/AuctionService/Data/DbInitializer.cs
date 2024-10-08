using System;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data;

public class DbInitializer
{
    public static void InitDb(WebApplication app) {
        using var scope = app.Services.CreateScope();
        
        SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
    }

    private static void SeedData(AuctionDbContext context)
    {
        context.Database.Migrate();

        if(context.Auctions.Any()){
            Console.WriteLine("Already have data - no need to seed");
            return;
        }

        var auctions = new List<Auction> {
            // 1. Ford GR
            new Auction
            {
                Id = Guid.Parse("6bbf0f05-4de2-4580-9af3-4f0673a5d96c"),
                Status = Status.Live,
                SoldAmount = 19000,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(10),
                Item = new Item
                {
                    Make = "Ford",
                    Model = "GT",
                    Color = "White",
                    Mileage = 50000,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2021/12/21/07/51/car-6884738_1280.jpg"
                }
            },
            // 2. Tesla Model S
            new Auction
            {
                Id = Guid.Parse("d2b68f15-5834-43e0-b017-64e26f8c5c3a"),
                Status = Status.Finished,
                SoldAmount = 75000,
                ReservePrice = 75000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(15),
                Item = new Item
                {
                    Make = "Tesla",
                    Model = "Model S",
                    Color = "Red",
                    Mileage = 30000,
                    Year = 2021,
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/11/16/14/24/milky-way-1829019_1280.jpg"
                }
            },

            // 3. BMW M3
            new Auction
            {
                Id = Guid.Parse("cdd3f26d-d5d1-4719-bd62-15d780f3de8a"),
                Status = Status.ReserveNotMet,
                SoldAmount = 63000,
                ReservePrice = 65000,
                Seller = "charlie",
                AuctionEnd = DateTime.UtcNow.AddDays(12),
                Item = new Item
                {
                    Make = "BMW",
                    Model = "M3",
                    Color = "Blue",
                    Mileage = 22000,
                    Year = 2022,
                    ImageUrl = "https://cdn.pixabay.com/photo/2024/08/07/13/43/modern-car-8952164_1280.png"
                }
            },

            // 4. Audi Q5
            new Auction
            {
                Id = Guid.Parse("e49e3f7b-7e5f-447e-a66b-5c816fd9e169"),
                Status = Status.Live,
                SoldAmount = 30000,
                ReservePrice = 40000,
                Seller = "dave",
                AuctionEnd = DateTime.UtcNow.AddDays(8),
                Item = new Item
                {
                    Make = "Audi",
                    Model = "Q5",
                    Color = "Black",
                    Mileage = 15000,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2024/08/07/13/43/modern-car-8952162_1280.png"
                }
            },

            // 5. Mercedes-Benz C-Class
            new Auction
            {
                Id = Guid.Parse("b1a77e3e-799c-4c5e-a07e-4f0eb7f4c7b4"),
                Status = Status.Live,
                SoldAmount = 30000,
                ReservePrice = 55000,
                Seller = "eve",
                AuctionEnd = DateTime.UtcNow.AddDays(14),
                Item = new Item
                {
                    Make = "Mercedes-Benz",
                    Model = "C-Class",
                    Color = "Silver",
                    Mileage = 18000,
                    Year = 2021,
                    ImageUrl = "https://cdn.pixabay.com/photo/2024/08/07/13/43/modern-car-8952161_1280.png"
                }
            },

            // 6. Chevrolet Corvette
            new Auction
            {
                Id = Guid.Parse("d4c3f5c6-6f6e-4be0-80d1-e9f7852c1cb6"),
                Status = Status.Live,
                SoldAmount = 60000,
                ReservePrice = 80000,
                Seller = "frank",
                AuctionEnd = DateTime.UtcNow.AddDays(20),
                Item = new Item
                {
                    Make = "Chevrolet",
                    Model = "Corvette",
                    Color = "Yellow",
                    Mileage = 10000,
                    Year = 2022,
                    ImageUrl = "https://cdn.pixabay.com/photo/2021/12/28/23/57/car-6900370_1280.jpg"
                }
            },

            // 7. Honda Accord
            new Auction
            {
                Id = Guid.Parse("a8b16427-320f-4b92-bc58-1e8d2d16d9b8"),
                Status = Status.Live,
                SoldAmount = 23000,
                ReservePrice = 25000,
                Seller = "grace",
                AuctionEnd = DateTime.UtcNow.AddDays(7),
                Item = new Item
                {
                    Make = "Honda",
                    Model = "Accord",
                    Color = "Gray",
                    Mileage = 30000,
                    Year = 2019,
                    ImageUrl = "https://cdn.pixabay.com/photo/2024/01/17/12/06/car-8514314_1280.png"
                }
            },

            // 8. Nissan Leaf
            new Auction
            {
                Id = Guid.Parse("f1c4c5e7-4a8f-40a0-b27d-94c6e872f889"),
                Status = Status.Live,
                SoldAmount = 28000,
                ReservePrice = 30000,
                Seller = "hannah",
                AuctionEnd = DateTime.UtcNow.AddDays(11),
                Item = new Item
                {
                    Make = "Nissan",
                    Model = "Leaf",
                    Color = "Green",
                    Mileage = 12000,
                    Year = 2021,
                    ImageUrl = "https://cdn.pixabay.com/photo/2017/03/05/15/29/aston-martin-2118857_1280.jpg"
                }
            },

            // 9. Subaru Outback
            new Auction
            {
                Id = Guid.Parse("b2f0c81a-0a92-4147-b0c7-8eac8e4902a5"),
                Status = Status.Live,
                SoldAmount = 31000,
                ReservePrice = 35000,
                Seller = "ian",
                AuctionEnd = DateTime.UtcNow.AddDays(9),
                Item = new Item
                {
                    Make = "Subaru",
                    Model = "Outback",
                    Color = "Brown",
                    Mileage = 20000,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2023/07/19/12/16/car-8136751_1280.jpg"
                }
            },

            // 10. Toyota Camry
            new Auction
            {
                Id = Guid.Parse("8c29a95c-d163-4b65-9e77-2209f5e160e1"),
                Status = Status.Live,
                SoldAmount = 20000,
                ReservePrice = 22000,
                Seller = "james",
                AuctionEnd = DateTime.UtcNow.AddDays(13),
                Item = new Item
                {
                    Make = "Toyota",
                    Model = "Camry",
                    Color = "Blue",
                    Mileage = 25000,
                    Year = 2019,
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/09/06/13/37/maserati-gran-turismo-1649119_1280.jpg"
                }
            }
        };

        context.AddRange(auctions);

        context.SaveChanges();
    }
}
