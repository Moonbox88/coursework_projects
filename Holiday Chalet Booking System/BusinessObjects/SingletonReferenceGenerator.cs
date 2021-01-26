/*
Author: Sean Mooney | 40283592
Holiday Chalet Booking System: Programme takes user input and creates objects that reflect customers and guests on chalet bookings
SingletonReferenceGenerator.cs: Generates unique customer and booking references
Last modified: 11/12/2017
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    [Serializable]
    public sealed class SingletonReferenceGenerator
    {
        private static SingletonReferenceGenerator instance;

        
        private int _booking_ref = 0;
        private int _customer_ref = 0;

        private SingletonReferenceGenerator() { }

        public static SingletonReferenceGenerator Instance()
        {
            if (instance == null)
            {
                instance = new SingletonReferenceGenerator();
            }
            return instance;
        }


        public int generateBookingReference()
        {
            _booking_ref++;

            return _booking_ref;
        }

        public int generateCustomerReference()
        {
            _customer_ref++;

            return _customer_ref;
        }

        public int getBookingReference()
        {
            return _booking_ref;
        }

        public void setBookingReference(int value)
        {
            _booking_ref = value;
        }

        public int getCustomerReference()
        {
            return _customer_ref;
        }

        public void setCustomerReference(int value)
        {
            _customer_ref = value;
        }


    }
}
