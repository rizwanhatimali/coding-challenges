using System;
using System.Collections.Generic;
using System.Text;

namespace SupermarketKata
{
    public class Item
    {
        public string Sku { get; set; }
        public short UnitPrice { get; set; }
        public short SpecialQuantity { get; set; } = default(short);
        public short SpecialPrice { get; set; } = default(short);
    }
}