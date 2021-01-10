using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sasasaa.Models
{
    public static class Pricelist
    {

        public static Dictionary<string, double> pricelist;

        static Pricelist()
        {
            pricelist = new Dictionary<string, double>();
            pricelist.Add("Apple", 1);
            pricelist.Add("Banana", 1.3);
            pricelist.Add("Bread", 2);
            pricelist.Add("Oatmeal", 4.7);
            pricelist.Add("Croissants", 7);
            pricelist.Add("Bun", 4);
            pricelist.Add("Peanut Butter", 5);
            pricelist.Add("Rice Cake", 4.9);
            pricelist.Add("Jam", 3);
            pricelist.Add("Milk - 1 Litre", 14);
            pricelist.Add("Egg - 10 pack", 7);
            pricelist.Add("Yogurt", 3);
            pricelist.Add("Cheese", 5);
            pricelist.Add("Pasta", 11);
            pricelist.Add("Tomato - 1kg", 4);
            pricelist.Add("Onion - 1kg", 5);
            pricelist.Add("Tea", 6);
            pricelist.Add("Coffee", 7);
            pricelist.Add("Coconut milk", 5);
            pricelist.Add("Chicken 1kg", 14);
            pricelist.Add("Mutton 1kg", 14.9);
            pricelist.Add("Beef 1kg", 16.9);
            pricelist.Add("Pork 1kg", 16.5);
            pricelist.Add("Salmon 1kg", 15);
            pricelist.Add("Carrots", 5);
            pricelist.Add("Frozen Vegetable", 6);
            pricelist.Add("Pizza", 10);
            pricelist.Add("Washing liquid", 4);
            pricelist.Add("Toothpaste", 3);
            pricelist.Add("Tissues", 2);

        }
    }
}