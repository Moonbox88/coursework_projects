using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLayer
{
    public class Customer
    {
        private string _name;
        private string _address;
        private string _email;
        private string _bank_num;
        private string _bank_pin;

        private bool[] _details_inputOK = new bool[3];
        private bool[] _bank_inputOK = new bool[2];

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
                            _details_inputOK[0] = false;
                            throw new ArgumentOutOfRangeException("Customer name invalid input!");
                        }
                        else
                        {
                            _details_inputOK[0] = true;
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
                            _details_inputOK[1] = false;
                            throw new ArgumentOutOfRangeException("Customer address invalid input!");
                        }
                        else
                        {
                            _details_inputOK[1] = true;
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

        public string Email
        {//Accesses email of customer
            get { return _email; }
            set
            {
                try
                {
                    foreach (char c in value)
                    {
                        if (value == "") // Check is email address
                        {
                            _details_inputOK[2] = false;
                            throw new ArgumentOutOfRangeException("Customer email invalid input!");
                        }
                        else
                        {
                            _details_inputOK[2] = true;
                            _email = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public Int64 BankNum
        {//Accesses bank number of customer
            get { return Convert.ToInt64(_bank_num); }
            set
            {
                try
                {
                    if (value.GetType() != typeof(Int64) || value.ToString().Length < 16)
                    {
                        _bank_inputOK[0] = false;
                        throw new ArgumentOutOfRangeException("Customer bank number too short or not number value!");
                    }
                    else
                    {
                        _bank_inputOK[0] = true;
                        _bank_num = value.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public Int64 BankPin
        {//Accesses bank pin of customer
            get { return Convert.ToInt64(_bank_pin); }
            set
            {
                try
                {
                    if (value.GetType() != typeof(Int64) || value.ToString().Length < 3)
                    {
                        _bank_inputOK[1] = false;
                        throw new ArgumentOutOfRangeException("Customer bank pin too short or not number value!");
                    }
                    else
                    {
                        _bank_inputOK[1] = true;
                        _bank_pin = value.ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        public bool[] customer_details_inputOK()
        {//Accesses input integrity check bool[]
            return _details_inputOK;
        }

        public bool[] bank_details_inputOK()
        {//Accesses input integrity check bool[]
            return _bank_inputOK;
        }

    }

}
