using NewYearGift.App.Constants;
using NewYearGift.DAL.Models.Sweets;
using NewYearGift.DAL.Models.Sweets.Parameters;
using NewYearGift.Models.Gifts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewYearGift.App.Data
{
    public class DataSeeder
    {
        public DataModel GetData()
        {
            var FillingTypes = new List<FillingType>
                {
                    new FillingType { Name = "Fudge" },
                    new FillingType { Name = "Chocolate" },
                    new FillingType { Name = "Nuts" },
                    new FillingType { Name = "Lollipop caramel" },
                    new FillingType { Name = "Jelly" },
                    new FillingType { Name = "Liquor" },
                    new FillingType { Name = "Whiskey" }
                };
            var ShapeTypes = new List<ShapeType>
                {
                    new ShapeType { Name = "Square" },
                    new ShapeType { Name = "Circle" },
                    new ShapeType { Name = "Oval" },
                    new ShapeType { Name = "Bottle" },
                    new ShapeType { Name = "Pyramid" }
                };
            var Sweets = new List<Sweet>
                {
                    new SugarFreeSweet(1, "Korivka", 10, 15, new Filling { Type = new FillingType { Name = "Fudge" } }, new Shape { Type = new ShapeType { Name = "Square" } }),
                    new SugarFreeSweet(1, "Romashka", 8, 9, new Filling { Type = new FillingType { Name = "Chocolate" } }, new Shape { Type = new ShapeType { Name = "Square" } }),
                    new SugarFreeSweet(1, "Brittle", 14, 18, new Filling { Type = new FillingType { Name = "Nuts" } }, new Shape { Type = new ShapeType { Name = "Square" } }),
                    new SugarFreeSweet(1, "Lollipop", 6, 9, new Filling { Type = new FillingType { Name = "Lollipop caramel" } }, new Shape { Type = new ShapeType { Name = "Circle" } }),
                    new SugarFreeSweet(1, "Milky", 11, 22, new Filling { Type = new FillingType { Name = "Fudge" } }, new Shape { Type = new ShapeType { Name = "Oval" } }),
                    new SugarFreeSweet(1, "JellyBelly", 11, 18, new Filling { Type = new FillingType { Name = "Jelly" } }, new Shape { Type = new ShapeType { Name = "Square" } }),
                    new SugarSweet(2, "Korivka Sugar", 10, 15, new Filling { Type = new FillingType { Name = "Fudge" } }, new Shape { Type = new ShapeType { Name = "Square" } }, 3),
                    new SugarSweet(2, "Romashka Sugar", 8, 9, new Filling { Type = new FillingType { Name = "Chocolate" } }, new Shape { Type = new ShapeType { Name = "Square" } }, 4),
                    new SugarSweet(2, "Brittle sugar", 14, 18, new Filling { Type = new FillingType { Name = "Nuts" } }, new Shape { Type = new ShapeType { Name = "Square" } }, 2),
                    new SugarSweet(2, "Lollipop sugar", 6, 9, new Filling { Type = new FillingType { Name = "Lollipop caramel" } }, new Shape { Type = new ShapeType { Name = "Circle" } }, 8),
                    new SugarSweet(2, "Milky sugar", 11, 22, new Filling { Type = new FillingType { Name = "Fudge" } }, new Shape { Type = new ShapeType { Name = "Oval" } }, 6),
                    new AlcoholicSweet(3, "Liqueur Alcohol", 10, 15, new Filling { Type = new FillingType { Name = "Liquor" } }, new Shape { Type = new ShapeType { Name = "Square" } }, 20),
                    new AlcoholicSweet(3, "Whiskey Bottle Alcohole", 8, 9, new Filling { Type = new FillingType { Name = "Whiskey" } }, new Shape { Type = new ShapeType { Name = "Bottle" } }, 40),
                    new AlcoholicSugarSweet(4, "Pyramid Alcohol Sugar", 10, 15, new Filling { Type = new FillingType { Name = "Liquor" } }, new Shape { Type = new ShapeType { Name = "Pyramid" } }, 14, 20)
                };

            var resultModel = new DataModel
            {
                FillingTypes = FillingTypes,
                ShapeTypes = ShapeTypes,
                Sweets = Sweets,
                Gift = new Gift
                {
                    BelongToPresentee = Presentee.Children,
                    Weight = 35,
                    Kkal = 55,
                    CountOfSweets = 14,
                    Sweets = Sweets
                }
            };
            return resultModel;
        }
    }
}
