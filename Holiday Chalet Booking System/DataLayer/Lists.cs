/*
Author: Sean Mooney | 40283592
Holiday Chalet Booking System: Programme takes user input and creates objects that reflect customers and guests on chalet bookings
Lists.cs: Has methods for accessing and updating Lists 
Last modified: 11/12/2017
*/

using System;
using System.Collections.Generic;
using BusinessObjects;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataLayer
{
    [Serializable]
    public class Lists
    {
        //List hold bookings and customer data
        private List<Customer> _customer_list = new List<Customer>();
        private List<Booking> _booking_list = new List<Booking>();

    
        public void addCustomer(Customer newCustomer)
        {//Adds customer to list
            _customer_list.Add(newCustomer);
        }

        public Customer findCustomer(string name, string address)
        {//Finds customer in the list
            foreach (Customer p in _customer_list)
            {
                if (name == p.Name && address == p.Address)
                {
                    return p;
                }
            }
            return null;
        }

        public void deleteCustomer(string name, string address)
        {//Uses find to delete a customer
            Customer p = this.findCustomer(name, address);
            if (p != null)
            {
                _customer_list.Remove(p);
            }
        }

        public List<Customer> getCustomerList()
        {//Returns customer list
            return _customer_list;
        }

        public void addBooking(Booking newBooking)
        {//Adds a booking to the list
            _booking_list.Add(newBooking);
        }

        public Booking findBooking(int reference)
        {//Finds a booking in the list
            foreach (Booking p in _booking_list)
            {
                if (reference == p.BookingReference)
                {

                    return p;
                }
            }
            return null;
        }

        public void deleteBooking(int reference)
        {//Uses find to delete a booking
            Booking p = this.findBooking(reference);
            if (p != null)
            {
                _booking_list.Remove(p);
            }
        }

        public List<Booking> getBookingList()
        {//Retirns bookings list
            return _booking_list;
        }

        public void setBookingList(List<Booking> bookingList)
        {
            _booking_list = bookingList;
        }

        public void setCustomerList(List<Customer> customerList)
        {
            _customer_list = customerList;
        }
    }
}
