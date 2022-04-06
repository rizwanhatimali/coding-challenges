using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupermarketKata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SupermarketKataTest
{
    [TestClass]
    public class TestCheckout
    {
        public Checkout checkout;
        public Item itemA = new Item { Sku = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 };
        public Item itemB = new Item { Sku = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 };
        public Item itemC = new Item { Sku = "C", UnitPrice = 20 };
        public Item itemD = new Item { Sku = "D", UnitPrice = 15 };

        public readonly List<Item> _inventoryList;
        public TestCheckout()
        {
            _inventoryList = new List<Item>();
            _inventoryList.Add(itemA);
            _inventoryList.Add(itemB);
            _inventoryList.Add(itemC);
            _inventoryList.Add(itemD);

            checkout = new Checkout(_inventoryList);

            checkout.cartItems = new List<CartItems> 
            { 
                new CartItems
                {
                    CartItem = itemA,
                    Quantity = 2
                },
                new CartItems
                {
                    CartItem = itemB,
                    Quantity = 2
                }
            };
        }

        [TestMethod]
        public void TestAddNewItemToCart()
        {
            checkout.Scan("C");
            Assert.AreEqual(checkout.cartItems.Where(obj => obj.CartItem.Sku.Equals("C")).First().Quantity, 1);
            Assert.AreEqual(checkout.cartItems.Where(obj => obj.CartItem.Sku.Equals("C")).First().TotalItemPrice, 20);
            Assert.AreEqual(checkout.GetTotalPrice(), 195);
        }

        [TestMethod]
        public void TestAddExistingItemToCart()
        {
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.AreEqual(checkout.cartItems.Where(obj => obj.CartItem.Sku.Equals("A")).First().Quantity, 3);
            Assert.AreEqual(checkout.cartItems.Where(obj => obj.CartItem.Sku.Equals("A")).First().TotalItemPrice, 130);
            Assert.AreEqual(checkout.cartItems.Where(obj => obj.CartItem.Sku.Equals("B")).First().Quantity, 3);
            Assert.AreEqual(checkout.cartItems.Where(obj => obj.CartItem.Sku.Equals("B")).First().TotalItemPrice, 75);
            Assert.AreEqual(checkout.GetTotalPrice(), 205);
        }

        [TestMethod]
        public void TestAddUnavailableItemToCart()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            checkout.Scan("N");
            Assert.AreEqual("Requested Item does not exist in our inventory\r\n", sw.ToString());
        }
    }
}
