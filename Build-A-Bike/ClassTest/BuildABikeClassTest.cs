using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using System.Collections.Generic;

namespace ClassTest
{
    [TestClass]
    public class BuildABikeClassTest
    {
        [TestMethod]
        public void CreateBikeClassTest()
        {
            Bike newBike = new Bike();
            newBike.Frame = "frame";
            newBike.Gears = "gears";
            newBike.Brakes = "brakes";
            newBike.Wheels = "wheels";
            newBike.Handlebars = "handlebars";
            newBike.Saddle = "saddle";

            Bike otherNewBike = new Bike();
            newBike.Frame = "frame";
            newBike.Gears = "gears";
            newBike.Brakes = "brakes";
            newBike.Wheels = "wheels";
            newBike.Handlebars = "handlebars";
            newBike.Saddle = "saddle";

            Assert.AreEqual(newBike, otherNewBike);
        }

        [TestMethod]
        public void CreateCustomerClassTest()
        {
            Customer newCustomer = new Customer();
            newCustomer.Name = "frame";
            newCustomer.Address = "gears";
            newCustomer.Email = "brakes";
            newCustomer.BankNum = 12345678912345667;
            newCustomer.BankPin = 22;

            Customer otherNewCustomer = new Customer();
            newCustomer.Name = "8888888";
            newCustomer.Address = "6666666";
            newCustomer.Email = "444444";
            newCustomer.BankNum = Int64.Parse("bankNum");
            newCustomer.BankPin = Int64.Parse("bankPin");


            Assert.AreNotEqual(newCustomer, otherNewCustomer);
        }

        [TestMethod]
        public void CreateOrderClassTest()
        {
            Order newOrder = new Order();
            newOrder.Customer = new Customer();
            newOrder.BikeList = new List<Bike>();

            Order otherNewOrder = new Order();
            otherNewOrder.Customer = new Customer();
            otherNewOrder.BikeList = new List<Bike>();

            Assert.AreEqual(newOrder, otherNewOrder);
        }
    }
}
