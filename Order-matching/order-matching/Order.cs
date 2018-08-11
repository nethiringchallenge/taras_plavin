using System;
using System.Collections.Generic;
using System.Text;

namespace OrderMatching
{
    public class Order
    {
        public Order() { }
        public Order(string command, int price, int count) {
            Command = command;
            Count = count;
            Price = price;
        }

        public string Command { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
