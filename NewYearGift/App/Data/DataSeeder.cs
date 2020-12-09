using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.DAL.Models.Sweets.Parameters;
using NewYearGift.Models.Gifts;
using System.Collections.Generic;

namespace NewYearGift.App.Data
{
    public class DataSeeder
    {
        public static JsonDataModel GetData()
        {
            /*
            var Fillings = new List<Filling>
            {
                new Filling { Name = "Fudge" },
                new Filling { Name = "Chocolate" },
                new Filling { Name = "Nuts" },
                new Filling { Name = "Lollipop caramel" },
                new Filling { Name = "Jelly" },
                new Filling { Name = "Liquor" },
                new Filling { Name = "Whiskey" }
            };
            var Shapes = new List<Shape>
            {
                new Shape { Name = "Square" },
                new Shape { Name = "Circle" },
                new Shape { Name = "Oval" },
                new Shape { Name = "Bottle" },
                new Shape { Name = "Pyramid" }
            };*/
            var Sweets = new List<Sweet>
            {
                new SugarFreeSweet("Korivka", 10, 15, new Filling {  Name = "Fudge" }, new Shape { Name = "Square" }),
                new SugarFreeSweet("Romashka", 8, 9, new Filling {  Name = "Chocolate" }, new Shape { Name = "Square" }),
                new SugarFreeSweet("Brittle", 14, 18, new Filling {  Name = "Nuts" }, new Shape { Name = "Square" }),
                new SugarFreeSweet("Lollipop", 6, 9, new Filling {  Name = "Lollipop caramel" }, new Shape { Name = "Circle" }),
                new SugarFreeSweet("Milky", 11, 22, new Filling {  Name = "Fudge" }, new Shape { Name = "Oval" }),
                new SugarFreeSweet("JellyBelly", 11, 18, new Filling {  Name = "Jelly" }, new Shape { Name = "Square" }),
                new SugarSweet("Korivka Sugar", 10, 15, new Filling {  Name = "Fudge" }, new Shape { Name = "Square" }, 3),
                new SugarSweet("Romashka Sugar", 8, 9, new Filling {  Name = "Chocolate" }, new Shape { Name = "Square" }, 4),
                new SugarSweet("Brittle sugar", 14, 18, new Filling {  Name = "Nuts" }, new Shape { Name = "Square" }, 2),
                new SugarSweet("Lollipop sugar", 6, 9, new Filling {  Name = "Lollipop caramel" }, new Shape { Name = "Circle" }, 8),
                new SugarSweet("Milky sugar", 11, 22, new Filling {  Name = "Fudge" }, new Shape { Name = "Oval" }, 6),
                new AlcoholicSweet("Liqueur Alcohol", 10, 15, new Filling {  Name = "Liquor" }, new Shape { Name = "Square" }, 20),
                new AlcoholicSweet("Whiskey Bottle Alcohole", 8, 9, new Filling {  Name = "Whiskey" }, new Shape { Name = "Bottle" }, 40),
                new AlcoholicSugarSweet("Pyramid Alcohol Sugar", 10, 15, new Filling {  Name = "Liquor" }, new Shape { Name = "Pyramid" }, 14, 20)
            };

            var resultModel = new JsonDataModel
            {
                AllSweets = Sweets,
                Gift = new Gift
                {
                    BelongToPresentee = Presentee.Adult,
                    Weight = 42,
                    Kkal = 55,
                    CountOfSweets = 4,
                    Sweets = new List<Sweet> { Sweets[1], Sweets[8], Sweets[11], Sweets[13]}
                }
            };
            return resultModel;
        }
    }
}
