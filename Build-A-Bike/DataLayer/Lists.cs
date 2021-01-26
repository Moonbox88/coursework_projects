using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Lists
    {
        //Lists hold bookings and customer data
        private List<Customer> _customer_list = new List<Customer>();
        private List<Order> _order_list = new List<Order>();
        private List<Bike> _bike_list = new List<Bike>();

        //Lists act as stock
        private List<string> _frames = new List<string>();
        private List<string> _gears = new List<string>();
        private List<string> _brakes = new List<string>();
        private List<string> _wheels = new List<string>();
        private List<string> _handlebars = new List<string>();
        private List<string> _saddle = new List<string>();
        int framePrice;
        int gearsPrice;
        int brakesPrice;
        int wheelsPrice;
        int handlebarsPrice;
        int saddlePrice;

        public string findFrame(string frame)
        {//Finds customer in the list
            foreach (string p in _frames)
            {
                if (frame == p)
                {
                    return p;
                }
            }
            return null;
        }

        public void deleteFrame(string frame)
        {//Uses find to delete a customer
            string p = this.findFrame(frame);
            if (p != null)
            {
                _frames.Remove(p);
            }
        }

        public string findGears(string gears)
        {//Finds customer in the list
            foreach (string p in _gears)
            {
                if (gears == p)
                {
                    return p;
                }
            }
            return null;
        }

        public void deleteGears(string gears)
        {//Uses find to delete a customer
            string p = this.findGears(gears);
            if (p != null)
            {
                _gears.Remove(p);
            }
        }

        public string findBrakes(string brakes)
        {//Finds customer in the list
            foreach (string p in _brakes)
            {
                if (brakes == p)
                {
                    return p;
                }
            }
            return null;
        }

        public void deleteBrakes(string brakes)
        {//Uses find to delete a customer
            string p = this.findBrakes(brakes);
            if (p != null)
            {
                _brakes.Remove(p);
            }
        }

        public string findWheels(string wheels)
        {//Finds customer in the list
            foreach (string p in _wheels)
            {
                if (wheels == p)
                {
                    return p;
                }
            }
            return null;
        }

        public void deleteWheels(string wheels)
        {//Uses find to delete a customer
            string p = this.findWheels(wheels);
            if (p != null)
            {
                _wheels.Remove(p);
            }
        }

        public string findHandlebars(string handlebars)
        {//Finds customer in the list
            foreach (string p in _handlebars)
            {
                if (handlebars == p)
                {
                    return p;
                }
            }
            return null;
        }

        public void deleteHandlebars(string handlebars)
        {//Uses find to delete a customer
            string p = this.findHandlebars(handlebars);
            if (p != null)
            {
                _handlebars.Remove(p);
            }
        }

        public string findSaddle(string saddle)
        {//Finds customer in the list
            foreach (string p in _saddle)
            {
                if (saddle == p)
                {
                    return p;
                }
            }
            return null;
        }

        public void deleteSaddle(string saddle)
        {//Uses find to delete a customer
            string p = this.findSaddle(saddle);
            if (p != null)
            {
                _saddle.Remove(p);
            }
        }

        public void addCustomer(Customer newCustomer)
        {//Adds a customer to the list
            _customer_list.Add(newCustomer);
        }

        public void addOrder(Order newOrder)
        {//Adds a booking to the list
            _order_list.Add(newOrder);
        }

        public void addBike(Bike newBike)
        {//Adds a booking to the list
            _bike_list.Add(newBike);
        }

        //Loads items into stock lists
        public void addFrame(string frame)
        {
            _frames.Add(frame);
        }

        public void addGears(string gears)
        {
            _gears.Add(gears);
        }

        public void addBrakes(string brakes)
        {
            _brakes.Add(brakes);
        }

        public void addWheels(string wheels)
        {
            _wheels.Add(wheels);
        }

        public void addHandlebars(string handlesbars)
        {
            _handlebars.Add(handlesbars);
        }

        public void addSaddle(string saddle)
        {
            _saddle.Add(saddle);
        }

        //Returns prices
        public int getFramePrice(string frame)
        {
            if (frame == "Specialized")
                framePrice = 1000;
            if (frame == "Trek")
                framePrice = 750;
            if (frame == "Boardman")
                framePrice = 500;
            if (frame == "Planet X")
                framePrice = 850;

            return framePrice;
        }

        public int getGearsPrice(string gears)
        {
            if (gears == "Shimano")
                gearsPrice = 600;
            if (gears == "Sram")
                gearsPrice = 350;
            if (gears == "Campagnolo")
                gearsPrice = 700;

            return gearsPrice;
        }

        public int getBrakesPrice(string brakes)
        {
            if (brakes == "Shimano")
                brakesPrice = 350;
            if (brakes == "Sram")
                brakesPrice = 250;
            if (brakes == "Hope")
                brakesPrice = 400;

            return brakesPrice;
        }

        public int getWheelsPrice(string wheels)
        {
            if (wheels == "Shimano")
                wheelsPrice = 400;
            if (wheels == "Mavic")
                wheelsPrice = 800;
            if (wheels == "Hope")
                wheelsPrice = 600;

            return wheelsPrice;
        }

        public int gethandlebarsPrice(string handlebars)
        {
            if (handlebars == "Specialized")
                handlebarsPrice = 200;
            if (handlebars == "Trek")
                handlebarsPrice = 150;
            if (handlebars == "Boardman")
                handlebarsPrice = 100;
            if (handlebars == "Planet X")
                handlebarsPrice = 180;

            return handlebarsPrice;
        }

        public int getSaddlePrice(string saddle)
        {
            if (saddle == "Selle Italia")
                saddlePrice = 200;
            if (saddle == "Brooks")
                saddlePrice = 250;
            if (saddle == "Prologo")
                saddlePrice = 150;

            return saddlePrice;
        }
    }
}
