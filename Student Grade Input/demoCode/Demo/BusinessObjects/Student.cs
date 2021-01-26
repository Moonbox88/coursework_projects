using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class Student
    {
        private int _matricNo;
        private string _firstName;
        private string _surname;
        private double _courseworkMark;
        private double _examMark;
        private string _dOb;


        //At limitations to the get/set methods
        
        public int Matric
        {//Have Matric generate from 10001...
            get { return _matricNo; }
            set {_matricNo = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set {_firstName = value; }
        }
        public string Surname
        {
            get { return _surname; }
            set {_surname = value; }
        }
        public double Coursework
        {
            get { return _courseworkMark; }
            set {_courseworkMark = value; }
        }
        public double Exam
        {
            get { return _examMark; }
            set {_examMark = value; }
        }
        public string DOB
        {//DOB only allows dd/mm/yyyy format
            get { return _dOb; }
            set {_dOb = value; }
        }
        //ADD GETMARK() METHOD
    }
}
