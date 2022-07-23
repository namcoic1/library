using LMS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_Project.Models
{
    public class Cart
    {
        Book book;
        int quantity;
        double price;
        string totalat;

        public Cart()
        {
        }

        public Cart(Book p, int quantity)
        {
            this.book = p;
            this.quantity = quantity;
        }

        public Cart(Book p, int quantity, double price)
        {
            this.book = p;
            this.quantity = quantity;
            this.price = price;
        }

        public Book Book
        {
            get { return book; }
            set { book = value; }
        }

        public int Quantities
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public string TotalAt
        {
            get { return totalat; }
            set { totalat = value; }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
