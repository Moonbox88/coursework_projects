/*
Author: Sean Mooney | 40283592
Student record input programme: takes input data and creates student objects
Student.cs: Has Student class and constructors for building student object
Last modified: 24/10/2017
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BusinessObjects
{
    public class Student
    {
        //Student attributes
        private int _matricNo;
        private string _firstName;
        private string _surname;
        private double _courseworkMark;
        private double _examMark;
        private string _dOb;
        //Bool array holds values for checking input integrity
        private bool[] _inputOK = new bool[5];


        public int Matric
        {//Accesses matric number of student
            get { return _matricNo; }
            set {_matricNo = value; }
        }

        public string FirstName
        {//Accesses first name of student
            get { return _firstName; }
            set
            {
                try
                {
                    foreach (char c in value)
                    {//Check if each character of string is a letter character
                        if (!char.IsLetter(c))
                        {//If non letter chars found then throw exception
                            _inputOK[0] = false;
                            throw new ArgumentOutOfRangeException("First name must be comprised of letter characters!");
                        }
                        else
                        {//If none found then set attribute as input
                            _inputOK[0] = true;
                            _firstName = value;
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public string Surname
        {//Accesses surname of student
            get { return _surname; }
            set
            {
                try
                {
                    foreach (char c in value)
                    {//Check if each character of string is a letter character
                        if (!char.IsLetter(c))
                        {//If non letter chars found then throw exception. Set bool[] index to false
                            _inputOK[1] = false;
                            throw new ArgumentOutOfRangeException("Surname must be comprised of letter characters!");
                        }
                        else
                        {//If none found then set attribute as input. Set bool[] index to true
                            _inputOK[1] = true;
                            _surname = value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public double Coursework
        {//Accesses coursework mark of student
            get { return _courseworkMark; }
            set
            {
                try
                {
                    if (value >= 0 && value <= 20)
                    {//If value is within correct range return value. Set bool[] index to true.
                        _inputOK[2] = true;
                        _courseworkMark = value;
                    }
                    else
                    {//If not then throw exception. Set bool[] index to false
                        _inputOK[2] = false;
                        throw new ArgumentOutOfRangeException("Coursework must be between 0 - 20!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public double Exam
        {//Accesses exam result of student
            get { return _examMark; }
            set
            {
                try
                {
                    if(value >= 0 && value <= 40)
                    {//If value is within correct range return value. Set bool[] index to true.
                        _inputOK[3] = true;
                        _examMark = value;
                    }
                    else
                    {//If not then throw exception. Set bool[] index to false
                        _inputOK[3] = false;
                        throw new ArgumentOutOfRangeException("Exam mark must be between 0 - 40!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public string DOB
        {//Accesses DOB of student
            get { return _dOb; }
            set
            {
                try
                {//Attempt to convert input string to DateTime
                    DateTime dt = DateTime.Parse(value);
                    //If successful the return value. Set bool[] index to true  
                    _inputOK[4] = true;
                    _dOb = value;
                }
                catch(Exception ex)
                {//If unsuccessful then throw exception. Set bool[] index to false
                    _inputOK[4] = false;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public bool[] InputOK
        {//Accesses input integrity check bool[]
            get { return _inputOK; }
            set {_inputOK = value; }
        }

        public double getMark()
        {//Method returns final grade as a percentage when called
            double mark = (((_courseworkMark / 20) * 100) * 0.5) + (((_examMark / 40) * 100) * 0.5);
            return mark;
        }
    }
}
