using Brewery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brewery.Controller
{
    public class Controller
    {
        public LinkedList<Customer> customers = new LinkedList<Customer>();

        public void CreateCustomer(string firstName, string lastName)
        {
            customers.AddLast(new Customer(firstName, lastName));
        }

        // Customer orders a barrel of beer. 
        public void CreateOrderBarrel(Guid id, BeerStyle beerStyle, DateTime orderDate)
        {
            var tempCustomer = customers.Where(o => o.Id == id).FirstOrDefault();
            tempCustomer.OrderedBarrels.AddLast(new Barrel(tempCustomer, beerStyle, orderDate));
        }

        // Ordered barrels should be produced and delivered. 
        // If the customer has ordered several barrels of this beer style and those are not delivered yet, the brewery must deliver the earliest order (FIFO principle - First In First Out)
        public void CreateDeliveryBarrel(Guid id, BeerStyle beerStyle, DateTime deliveryDate)
        {
            var tempCustomer = customers.Where(o => o.Id == id).FirstOrDefault();

            // We select ordered barrels of this beer style and sort these by ascending
            LinkedList<Barrel> orderBarrels  = new LinkedList<Barrel>(tempCustomer.OrderedBarrels.Where(o => o.BeerStyle.Equals(beerStyle)).OrderBy(o => o.OrderDate));
            
            // Get the barrel with the earliest order date and deliver it
            Barrel tempBarrel = orderBarrels.First.Value;
            tempBarrel.DeliveryDate = deliveryDate;
            tempCustomer.DeliveredBarrels.AddLast(tempBarrel);
            tempCustomer.OrderedBarrels.Remove(tempBarrel);
        }

        public Customer GetCustomer(Guid id)
        {
            return customers.Where(c => c.Id == id).FirstOrDefault();
        }

        public List<Customer> GetBestCustomers()
        {
            // returns a list of customers sorted by ordered barrels + delivered barrels
            return customers.OrderByDescending(o => o.OrderedBarrels.Count + o.DeliveredBarrels.Count).ToList();
        }

        // the longest time period in days between an order being placed and its delivery
        public int GetTimeDifference()
        {
            TimeSpan span = new TimeSpan();

            foreach(Customer c in customers)
            {
                foreach(Barrel b in c.DeliveredBarrels)
                {
                    if(b.DeliveryDate.Subtract(b.OrderDate) > span)
                    {
                        span = b.DeliveryDate.Subtract(b.OrderDate);
                    }
                    
                }
            }

            return span.Days;
        }

    }
}
