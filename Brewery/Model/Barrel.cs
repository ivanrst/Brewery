using System;
using System.Collections.Generic;
using System.Text;

namespace Brewery.Model
{
    public class Barrel
    {
        public Customer Customer { get; set; }

        public BeerType BeerType { get; set; }

        public DateTime OrderDate { get; set; } 

        public DateTime DeliveryDate { get; set; }

        public Barrel(Customer customer, BeerType beerType, DateTime orderDate)
        {
            Customer = customer;

            BeerType = beerType;

            OrderDate = orderDate;
        }

    }
}
