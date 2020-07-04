using System;
using System.Collections.Generic;
using System.Text;

namespace Brewery.Model
{
    public class Barrel
    {
        public Customer Customer { get; set; }

        public BeerStyle BeerStyle { get; set; }

        public DateTime OrderDate { get; set; } 

        public DateTime DeliveryDate { get; set; }

        public Barrel(Customer customer, BeerStyle beerStyle, DateTime orderDate)
        {
            Customer = customer;

            BeerStyle = beerStyle;

            OrderDate = orderDate;
        }

    }
}
