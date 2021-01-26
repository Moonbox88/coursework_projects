/*
Author: Sean Mooney | 40283592
Holiday Chalet Booking System: Programme takes user input and creates objects that reflect customers and guests on chalet bookings
Guest.cs: Has Guest class and constructors for creating Guest objects
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
    public class Guest
    {
        private string _name;
        private string _passport;
        private int _age;

        private bool[] _inputOK = new bool[3];

        public string Name
        {//GetsSets Guest name
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
                            throw new ArgumentOutOfRangeException("Guest name invalid input!");
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

        public string PassportNumber
        {//GetsSets guest passport number
            get { return _passport; }
            set
            {
                try
                {
                    foreach (char c in value)
                    {
                        if (value == "" || !Regex.IsMatch(value, @"^[a-zA-Z0-9#]+$"))
                        {
                            _inputOK[1] = false;
                            throw new ArgumentOutOfRangeException("Passport no. must be comprised of letter or numbers and no spaces!");
                        }
                        else
                        {
                            _inputOK[1] = true;
                            _passport = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public int Age
        {//GetsSets guest age
            get { return _age; }
            set
            {
                try
                {

                    if (value == 0)
                    {
                            _inputOK[2] = false;
                            throw new ArgumentOutOfRangeException("Age must be a number and above 0");
                    }
                    else
                    {
                        _inputOK[2] = true;
                        _age = value;
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public bool[] inputOK()
        {//Accesses input integrity check bool[]
            return _inputOK;
        }
    }
}
