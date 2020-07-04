using Brewery.Controller;
using Brewery.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BreweryTest
{
    public class ControllerTest
    {
        public Controller testController = new Controller();

        [SetUp]
        public void Setup()
        {
            // Test data

            // customers
            testController.customers.AddLast(new Customer("Adam", "Sandler"));
            testController.customers.AddLast(new Customer("Aubrey", "Plaza"));
            testController.customers.AddLast(new Customer("Ken", "Jeong"));
            testController.customers.AddLast(new Customer("Zoey", "Deutch"));
            testController.customers.AddLast(new Customer("Adam", "Devine"));

            // ordered barrels
            testController.customers.ElementAt(0).OrderedBarrels.AddLast(new Barrel(testController.customers.ElementAt(0), BeerStyle.IPA, new DateTime(2012, 3, 31)));
            testController.customers.ElementAt(0).OrderedBarrels.AddLast(new Barrel(testController.customers.ElementAt(0), BeerStyle.IPA, new DateTime(2013, 11, 3)));
            testController.customers.ElementAt(0).OrderedBarrels.AddLast(new Barrel(testController.customers.ElementAt(0), BeerStyle.IPA, new DateTime(2014, 7, 20)));

            // delivered barrels
            testController.customers.ElementAt(0).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(0), BeerStyle.IPA, new DateTime(2011, 6, 10)));
            testController.customers.ElementAt(0).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2011, 6, 11);
            testController.customers.ElementAt(1).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(1), BeerStyle.IPA, new DateTime(2011, 3, 31)));
            testController.customers.ElementAt(1).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2011, 4, 2);
            testController.customers.ElementAt(1).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(1), BeerStyle.Lager, new DateTime(2011, 4, 2)));
            testController.customers.ElementAt(1).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2011, 4, 3);
            testController.customers.ElementAt(1).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(1), BeerStyle.Pilsner, new DateTime(2013, 1, 6)));
            testController.customers.ElementAt(1).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2013, 6, 5);
            testController.customers.ElementAt(1).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(1), BeerStyle.Stout, new DateTime(2015, 7, 10)));
            testController.customers.ElementAt(1).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2015, 7, 12);
            testController.customers.ElementAt(2).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(2), BeerStyle.IPA, new DateTime(2010, 2, 5)));
            testController.customers.ElementAt(2).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2010, 2, 6);
            testController.customers.ElementAt(2).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(2), BeerStyle.Stout, new DateTime(2014, 10, 31)));
            testController.customers.ElementAt(2).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2014, 11, 2);
            testController.customers.ElementAt(3).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(3), BeerStyle.Pilsner, new DateTime(2015, 3, 17)));
            testController.customers.ElementAt(3).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2015, 3, 18);
            testController.customers.ElementAt(4).DeliveredBarrels.AddLast(new Barrel(testController.customers.ElementAt(4), BeerStyle.Lager, new DateTime(2011, 1, 2)));
            testController.customers.ElementAt(4).DeliveredBarrels.Last.Value.DeliveryDate = new DateTime(2011, 1, 4);
        }

        [TearDown]
        public void Cleanup()
        {
            testController.customers.Clear();
        }

        [Test]
        public void CustomerIdIsGuid()
        {
            // Arrange
            string firstName = "Ivan";
            string lastName = "Rust";
            Customer tempCustomer;

            // Act
            testController.CreateCustomer(firstName,lastName);
            tempCustomer = testController.customers.Last.Value;

            // Assert
            Assert.IsTrue(tempCustomer.LastName == lastName && tempCustomer.FirstName == firstName && Guid.TryParse(tempCustomer.Id.ToString(),out Guid result));
        }

        [Test]
        public void CustomerOrderedBarrelsIsEmpty()
        {
            // Arrange
            string firstName = "Ivan";
            string lastName = "Rust";
            Customer tempCustomer;

            // Act
            testController.CreateCustomer(firstName, lastName);
            tempCustomer = testController.customers.Last.Value;

            // Assert
            Assert.IsTrue(!tempCustomer.OrderedBarrels.Any());
        }

        [Test]
        public void CustomerDeliveredBarrelsIsEmpty()
        {
            // Arrange
            string firstName = "Ivan";
            string lastName = "Rust";
            Customer tempCustomer;

            // Act
            testController.CreateCustomer(firstName, lastName);
            tempCustomer = testController.customers.Last.Value;

            // Assert
            Assert.IsTrue(!tempCustomer.DeliveredBarrels.Any());
        }

        [Test]
        public void CreateOrderBarrelCheck()
        {
            // Arrange
            var rand = new Random();
            int i = rand.Next(testController.customers.Count);
            Customer tempCustomer = testController.customers.ElementAt(i);
            int orderedBarrelsBefore = tempCustomer.OrderedBarrels.Count;
            int deliveredBarrelsBefore = tempCustomer.DeliveredBarrels.Count;
            BeerStyle style = BeerStyle.Pilsner;
            DateTime orderDate = DateTime.Today;

            // Act
            testController.CreateOrderBarrel(tempCustomer.Id, style, orderDate);

            // Assert
            Assert.IsTrue((tempCustomer.OrderedBarrels.Count - orderedBarrelsBefore) == 1);
            Assert.IsTrue((tempCustomer.DeliveredBarrels.Count - deliveredBarrelsBefore) == 0);
        }

        [Test]
        public void CreateDeliveryBarrelCheck()
        {
            // Arrange
            Customer tempCustomer = testController.customers.ElementAt(0);
            int deliveredBarrelsBefore = tempCustomer.DeliveredBarrels.Count;
            int orderedBarrelsBefore = tempCustomer.OrderedBarrels.Count;
            BeerStyle style = BeerStyle.IPA;
            DateTime deliveryDate = new DateTime(2014, 9, 1);

            // Act
            testController.CreateDeliveryBarrel(tempCustomer.Id, style, deliveryDate);

            // Assert
            Assert.IsTrue((tempCustomer.DeliveredBarrels.Count - deliveredBarrelsBefore) == 1);
            Assert.IsTrue((tempCustomer.OrderedBarrels.Count - orderedBarrelsBefore) == -1);
        }

        [Test]
        public void GetCustomerCheck()
        {
            // Arrange
            string firstName = "Ivan";
            string lastName = "Rust";
            testController.customers.AddLast(new Customer(firstName, lastName));

            // Act
            var tempCustomer = testController.GetCustomer(testController.customers.Last.Value.Id);

            // Assert
            Assert.IsInstanceOf<Customer>(tempCustomer);
        }

        [Test]
        public void GetBestCustomerCheck()
        {
            // Act
            List<Customer> bestCustomers = testController.GetBestCustomers();

            // Assert
            foreach (var item in bestCustomers)
            {
                Assert.IsTrue(bestCustomers[0].OrderedBarrels.Count + bestCustomers[0].DeliveredBarrels.Count >= item.OrderedBarrels.Count + item.DeliveredBarrels.Count);
            }

        }

        [Test]
        public void GetTimeDifferenceCheck()
        {
            // Act
            int diff = testController.GetTimeDifference();

            //Assert
            foreach (var c in testController.customers)
            {
                foreach (var item in c.DeliveredBarrels)
                {
                    Assert.IsTrue(diff >= item.DeliveryDate.Subtract(item.OrderDate).Days);
                }
                
            }

        }

    }
}