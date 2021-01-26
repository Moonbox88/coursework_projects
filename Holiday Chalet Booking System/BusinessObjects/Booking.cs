/*
Author: Sean Mooney | 40283592
Holiday Chalet Booking System: Programme takes user input and creates objects that reflect customers and guests on chalet bookings
Booking.cs: Has Booking class and constructors for creating Booking objects
Last modified: 11/12/2017
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BusinessObjects
{
    public class Booking
    {

        private string _arrival;
        private string _departure;
        private int _booking_ref;
        private int _id;
        private Customer _customer;
        private List<Guest> _guest_list = new List<Guest>();
        private bool _meals;
        private bool _breakfast;

        
        private bool[] _inputOK = new bool[3];

        public DateTime ArrivalDate
        {//GetsSets arrival date for a booking
            get { return Convert.ToDateTime(_arrival); }
            set
            {
                try
                {

                    if (value.Date >= DateTime.Now.Date)
                    {
                        _inputOK[0] = true;
                        _arrival = value.ToString();
                        
                    }
                    else
                    {
                        _inputOK[0] = false;
                        throw new ArgumentOutOfRangeException("Booking date cannot be in the past");
                    }
                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public DateTime DepartureDate
        {//GetsSets departure date for a booking
            get { return Convert.ToDateTime(_departure); }
            set
            {
                try
                {
                    
                    if (value.Date == DateTime.Now.Date || value.Date <= Convert.ToDateTime(_arrival).Date)
                    {
                        _inputOK[1] = false;
                        throw new ArgumentOutOfRangeException("Booking date cannot be in the past and must be after arrival date");
                    }
                    else
                    {
                        _inputOK[1] = true;
                        _departure = value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public int ChaletID
        {//GetSets chaletID for a booking
            get { return _id; }
            set
            {
                try
                {
                    if (value < 1 || value > 10)
                    {
                        _inputOK[2] = false;
                        throw new ArgumentOutOfRangeException("Please select a chalet to create booking for");
                    }
                    else
                    {
                        _inputOK[2] = true;
                        _id = value;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public int BookingReference
        {//GetSets booking reference for a booking
            get { return _booking_ref; }
            set
            {
                _booking_ref = value;
            }
        }

        public void addGuest(Guest newGuest)
        {//Adds a new guest to the guest list
            try
            {
                if (GuestList.Count < 6)
                {
                    GuestList.Add(newGuest);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Can only have 6 guests per booking");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public Guest findGuest(string passport, string name)
        {//Searches for a guest in the guest list
            foreach (Guest p in GuestList)
            {
                if (passport == p.PassportNumber && name == p.Name)
                {

                    return p;
                }
            }
            return null;
        }

        public List<Guest> GuestList
        {//GetsSets instances of _guestList
            get { return _guest_list; }
            set
            {
                _guest_list = value;
            }
        }

        public void deleteGuest(string passport, string name)
        {//Uses find to delete a guest from guestlist
            Guest p = this.findGuest(passport, name);
            if (p != null)
            {
                GuestList.Remove(p);
            }
        }

        public Customer Customer
        {//GetsSets customer details for a booking
            get { return _customer; }
            set
            {
                try
                {
                    if (_customer != null)
                    {
                        throw new ArgumentOutOfRangeException("Bookings may only hold one customer");
                    }
                    else
                    {
                        _customer = value;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public bool EveningMeals
        {//Accesses switch for meals extra
            get { return _meals; }
            set
            {
                _meals = value;
            }
        }
        public bool Breakfast
        {//Accesses switch for breakfast extra
            get { return _breakfast; }
            set
            {
                _breakfast = value;
            }
        }
     
        public bool[] inputOK()
        {//Accesses input integrity check bool[]
            return _inputOK;
        }
        
    }
}

