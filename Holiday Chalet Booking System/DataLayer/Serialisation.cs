using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Documents;
using System.Windows;
using BusinessObjects;

namespace DataLayer
{
    public class Serialisation
    {
        private Lists store = new Lists();
        string path = Directory.GetCurrentDirectory();

        public void Save(List<Booking> bookings, List<Customer> customers)
        {
            XmlSerializer serializerBooking = new XmlSerializer(typeof(List<Booking>));
            XmlSerializer serializerCustomer = new XmlSerializer(typeof(List<Customer>));

            TextWriter WriteFileStreamBooking = new StreamWriter(path + "//PersistenceBookings.xml");
            TextWriter WriteFileStreamCustomer = new StreamWriter(path + "//PersistenceCustomers.xml");

            serializerBooking.Serialize(WriteFileStreamBooking, bookings);
            serializerCustomer.Serialize(WriteFileStreamCustomer, customers);

            WriteFileStreamBooking.Close();
            WriteFileStreamCustomer.Close();

        }

        public void Read()
        {


            XmlSerializer serializerBooking = new XmlSerializer(typeof(List<Booking>));
            XmlSerializer serializerCustomer = new XmlSerializer(typeof(List<Customer>));

            if (File.Exists(path + "//PersistenceBookings.xml") && File.Exists(path + "//PersistenceCustomers.xml"))
            {
                FileStream ReadFileStreamBooking = new FileStream(path + "//PersistenceBookings.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                FileStream ReadFileStreamCustomer = new FileStream(path + "//PersistenceCustomers.xml", FileMode.Open, FileAccess.Read, FileShare.Read);

                store.setBookingList((List<Booking>)serializerBooking.Deserialize(ReadFileStreamBooking));
                store.setCustomerList((List<Customer>)serializerCustomer.Deserialize(ReadFileStreamCustomer));

                ReadFileStreamBooking.Close();
                ReadFileStreamCustomer.Close();
            }
            else
            {
                return;
            }
        }
    }
}
