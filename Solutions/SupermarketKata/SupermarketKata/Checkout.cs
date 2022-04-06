using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SupermarketKata
{
    public class Checkout : ICheckout
    {
        public readonly List<Item> _inventoryList;
        public List<CartItems> cartItems { get; set; }

        public Checkout(List<Item> inventoryList)
        {
            _inventoryList = inventoryList;           

            cartItems = new List<CartItems>();
        }
        

        
        public int GetTotalPrice()
        {            
            return cartItems.Sum(obj => obj.TotalItemPrice);
        }

        public void Scan(string item)
        {
            var currCartItem = cartItems.Where(obj => obj.CartItem.Sku.Equals(item, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if(currCartItem != null)
            {
                currCartItem.Quantity++;
            }
            else
            {
                AddItemToCart(item);
            }
        }

        private void AddItemToCart(string item)
        {
            var itemToAdd = _inventoryList.Where(obj => obj.Sku.Equals(item, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
            if (itemToAdd == null)
            {
                Console.WriteLine("Requested Item does not exist in our inventory");
            }
            else
            {
                cartItems.Add(new CartItems { CartItem = itemToAdd, Quantity = 1 });
            }
        }
    }
}
