using System;
using System.Collections.Generic;

namespace SupermarketKata
{
    public class Program
    {
        private static bool endProgram = false;
        public static readonly List<Item> _inventoryList = new List<Item>
        {
            new Item { Sku = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
            new Item { Sku = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
            new Item { Sku = "C", UnitPrice = 20 },
            new Item { Sku = "D", UnitPrice = 15 }
        };        

        public static void Main(string[] args)
        {
            var checkout = new Checkout(_inventoryList);

            Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                endProgram = true;

            };

            Console.WriteLine("Add the Skus to add items to cart. Press CTRL + C to Exit");
            
            while(!endProgram)
            {
                string sku = Console.ReadLine();
                if(string.IsNullOrEmpty(sku))
                    break;

                checkout.Scan(sku);
            }

            Console.WriteLine($"Total Price = {checkout.GetTotalPrice()}");
        }
    }
}
