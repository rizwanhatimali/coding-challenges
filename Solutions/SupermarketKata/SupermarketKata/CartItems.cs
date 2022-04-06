using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketKata
{
    public class CartItems
    {
        public Item CartItem { get; set; }
        public short Quantity { get; set; }
        public int TotalItemPrice
        {
            get
            {
                int specialSet = CartItem.SpecialQuantity == default(short) ? default(int) : Quantity / CartItem.SpecialQuantity;
                int individualSet = CartItem.SpecialQuantity == default(short) ? Quantity : Quantity % CartItem.SpecialQuantity;
                return (specialSet * CartItem.SpecialPrice) + (individualSet * CartItem.UnitPrice);
            }
        }
    }
}
