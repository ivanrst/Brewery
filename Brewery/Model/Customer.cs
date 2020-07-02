using System;
using System.Collections.Generic;
using System.Text;

namespace Brewery.Model
{
    public class Customer
    {

        public Guid Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public LinkedList<Barrel> OrderedBarrels { get; set; }

        public LinkedList<Barrel> DeliveredBarrels { get; set; }

        public Customer(string firstName, string lastName)
        {
            Id = Guid.NewGuid();

            FirstName = firstName;

            LastName = lastName;

            OrderedBarrels = new LinkedList<Barrel>();

            DeliveredBarrels = new LinkedList<Barrel>();
        }

    }
}
