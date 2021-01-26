using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class Order
    {
        private int _order_num;
        private string _order_date;
        private Customer _customer;
        private List<Bike> _bikes = new List<Bike>();
        private bool _extended_warranty;

        public DateTime OrderDate
        {//GetsSets arrival date for a booking
            get { return Convert.ToDateTime(_order_date); }
            set { _order_date = value.ToString(); }
        }

        public int OrderNumber
        {//GetSets booking reference for a booking
            get { return _order_num; }
            set { _order_num = value; }
        }

        public void addBike(Bike newBike)
        {//Adds a new guest to the guest list
            BikeList.Add(newBike);
        }

        public List<Bike> BikeList
        {//GetsSets instances of _bikeList
            get { return _bikes; }
            set {_bikes = value; }
        }

        public Customer Customer
        {//GetsSets customer details for a booking
            get { return _customer; }
            set {_customer = value; }
        }

        public bool Warranty
        {//GetsSets customer details for a booking
            get { return _extended_warranty; }
            set {_extended_warranty = value; }
        }
    }
}
