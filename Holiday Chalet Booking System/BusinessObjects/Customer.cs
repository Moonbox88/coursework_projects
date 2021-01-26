/*
Author: Sean Mooney | 40283592
Holiday Chalet Booking System: Programme takes user input and creates objects that reflect customers and guests on chalet bookings
Customer.cs: Has Customer class and constructors for creating Customer objects
Last modified: 11/12/2017
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace BusinessObjects
{
    public class Customer
    {
        
        private string _name;
        private string _address;
        private int _cust_ref;

        private bool[] _inputOK = new bool[2];

        public string Name
        {//GetsSets customer name
            get { return _name; }
            set
            {
                try
                {
                    foreach (char c in value)
                    {
                        if (value == "" || !Regex.IsMatch(value, @"^[a-zA-Z0-9# ]+$"))
                        {
                            _inputOK[0] = false;
                            throw new ArgumentOutOfRangeException("Customer name invalid input!");
                        }
                        else
                        {
                            _inputOK[0] = true;
                            _name = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public string Address
        {//Accesses address of customer
            get { return _address; }
            set
            {
                try
                {
                    foreach (char c in value)
                    {
                        if (value == "" || !Regex.IsMatch(value, @"^[a-zA-Z0-9# ]+$"))
                        {
                            _inputOK[1] = false;
                            throw new ArgumentOutOfRangeException("Customer address invalid input!");
                        }
                        else
                        {
                            _inputOK[1] = true;
                            _address = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public int CustomerReference
        {//Accesses customer reference
            get { return _cust_ref; }
            set { _cust_ref = value; }
        }

        public bool[] inputOK()
        {//Accesses input integrity check bool[]
            return _inputOK;
        }

    }
}
