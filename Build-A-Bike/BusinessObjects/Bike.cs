using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Bike
    {
        private int _bike_ref;
        private string _frame;
        private string _gears;
        private string _brakes;
        private string _wheels;
        private string _handlebars;
        private string _saddle;

        public int Reference
        {//GetsSets Guest name
            get { return _bike_ref; }
            set { _bike_ref = value; }
        }

        public string Frame
        {//GetsSets Guest name
            get { return _frame; }
            set { _frame = value; }
        }

        public string Gears
        {//GetsSets guest passport number
            get { return _gears; }
            set { _gears = value; }
        }

        public string Brakes
        {//GetsSets guest age
            get { return _brakes; }
            set { _brakes = value; }
        }

        public string Wheels
        {//GetsSets guest age
            get { return _wheels; }
            set { _wheels = value; }
        }

        public string Handlebars
        {//GetsSets guest age
            get { return _handlebars; }
            set { _handlebars = value; }
        }

        public string Saddle
        {//GetsSets guest age
            get { return _saddle; }
            set { _saddle = value; }
        }
    }
}
