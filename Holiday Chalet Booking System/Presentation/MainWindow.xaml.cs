/*
Author: Sean Mooney | 40283592
Holiday Chalet Booking System: Programme takes user input and creates objects that reflect customers and guests on chalet bookings
MainWindow.xaml.cs: Has code for the main window in the programme where input data is taken from
Last modified: 11/12/2017
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using BusinessObjects;
using DataLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Lists store = new Lists();
        private Serialisation save = new Serialisation();
        private SingletonReferenceGenerator reference = SingletonReferenceGenerator.Instance();

        public MainWindow()
        {//Initialises main window and set input text size
            InitializeComponent();
           
            ((TextBox)txtAge).FontSize = 11;
            ((TextBox)txtBookingAddress).FontSize = 11;
            ((TextBox)txtBookingName).FontSize = 11;
            ((TextBox)txtGuestName).FontSize = 11;
            ((TextBox)txtPassportNo).FontSize = 11;

            save.Read();
            updateLists();
        }

        private void clearFields()
        {//Clears input fields
            txtBookingName.Text = null;
            txtBookingAddress.Text = null;
            dateArrival.SelectedDate = null;
            dateDeparture.SelectedDate = null;
            txtGuestName.Text = null;
            txtAge.Text = null;
            txtPassportNo.Text = null;
        }

        private bool integrityCheck(bool[] inputOK)
        {//Confirms if all neccesary inputs are OK format
            bool inputsOK = true;

            foreach (bool check in inputOK)
            {
                if (!check)
                {
                    inputsOK = false;
                }
            }
            return inputsOK;
        }

        private void updateLists()
        {//Updates listboxes with entries in lists
            lstChalets.Items.Clear();
            lstCustomers.Items.Clear();
            lstBookings.Items.Clear();
            lstInvoice.Items.Clear();

            for (int i = 1; i < 11; i++)
            {
                bool chaletOpen = true;
                if (store.getBookingList().Count == 0)
                {
                    lstChalets.Items.Add("Chalet #" + (i) + " available");
                }
                else
                { 
                    foreach (Booking b in store.getBookingList())
                    {
                        if (b.ChaletID == i && b.ArrivalDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                        {
                            chaletOpen = false;
                        }
                    }
                    if (chaletOpen == false)
                    {
                        lstChalets.Items.Add("Chalet #" + (i) + " occupied");

                    }
                    else
                    {
                        lstChalets.Items.Add("Chalet #" + (i) + " available");
                    }
                } 
            }

            foreach (Booking b in store.getBookingList())
            {
                lstBookings.Items.Add("#" + b.BookingReference + ",[" + b.ArrivalDate.ToShortDateString() + "-" + b.DepartureDate.Date.ToShortDateString() + "]," + "Chalet " + b.ChaletID);
            }

            foreach (Customer c in store.getCustomerList())
            {
                lstCustomers.Items.Add(c.Name + "," + c.Address + "," + "ref#" + c.CustomerReference);
            }
        }

        public void extrasSelection(Booking booking)
        {//Handles adding optional extras to a new booking
            if (MessageBox.Show("Add evening meals? £10 per person per night.", "Extras - Meals", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {}
            else { booking.EveningMeals = true; }
            if (MessageBox.Show("Add breakfast? £5 per person per day.", "Extras - Breakfast", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {}
            else { booking.Breakfast = true; }
            
        }

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {//Creates an invoice for a selected booking

            double baseCost = 0;
            double meals = 0;
            double breakfast = 0;
         
            string[] guestKey = lstBookings.SelectedItem.ToString().Split('#', ',');
            Booking booking = store.findBooking(int.Parse(guestKey[1]));
       
            lstInvoice.Items.Add("Booking #" + guestKey[1] + " Invoice\n");

            //60 per night plus 25 per guest
            baseCost = (60 * (booking.ArrivalDate - booking.DepartureDate).TotalDays) + ((25 * (booking.ArrivalDate - booking.DepartureDate).TotalDays) * booking.GuestList.Count);
            lstInvoice.Items.Add("Basecost £" + baseCost);

            if (booking.EveningMeals == true)
            {
                meals = (10 * (booking.ArrivalDate - booking.DepartureDate).TotalDays + (10 * (booking.ArrivalDate - booking.DepartureDate).TotalDays) * booking.GuestList.Count);
                lstInvoice.Items.Add("Evening meals £" + meals);
            }
            else
            {
                lstInvoice.Items.Add("no evening meals");
            }

            if (booking.Breakfast == true)
            {
                breakfast = (5 * (booking.ArrivalDate - booking.DepartureDate).TotalDays + (5 * (booking.ArrivalDate - booking.DepartureDate).TotalDays) * booking.GuestList.Count);
                lstInvoice.Items.Add("Breakfast £" + breakfast + "\n");
            }
            else
            {
                lstInvoice.Items.Add("no breaksfast");
            }

          
        }

        public bool checkAvailable(Booking booking)
        {//Checks new booking for overlap with any current bookings
            bool control = false;
           
            foreach (Booking b in store.getBookingList())
            {
                if (booking.ChaletID == b.ChaletID)
                {
                    if (booking.DepartureDate >= b.ArrivalDate && booking.ArrivalDate <= b.DepartureDate)
                    {
                        MessageBox.Show("Booking overlap");
                        control = true;
                    }
                }
            }
            return control;
        }

        private void btnCreateBooking_Click(object sender, RoutedEventArgs e)
        {//Adds a new booking with customer to customer list
            //Create new objects and capture variables
            Booking booking = new Booking();
            Customer customer = new Customer();
            try
            {
                booking.ArrivalDate = dateArrival.SelectedDate.Value;
                booking.DepartureDate = dateDeparture.SelectedDate.Value;
            }
            catch
            {
                MessageBox.Show("Arrival and departure dates must be selected");
            }
            
            booking.ChaletID = lstChalets.SelectedIndex + 1;

            if (checkAvailable(booking) == true)
                return;

            customer.Name = txtBookingName.Text;
            customer.Address = txtBookingAddress.Text;
            bool custControl = false;

            if (integrityCheck(booking.inputOK()) == false || (integrityCheck(customer.inputOK()) == false))
            {//If inputs are incorrect throw error and cancel process
                if (booking.ArrivalDate != null)
                    return;
                else
                MessageBox.Show("Name and address must be provided for customer");
                return;
            }
            else
            {//If all inputs are OK then proceed to create booking
                if (booking.Customer != null)
                {
                    return;
                }
                lstGuests.Items.Clear();
                booking.BookingReference = reference.generateBookingReference();
                MessageBox.Show("New booking ref#" + booking.BookingReference);
                extrasSelection(booking);
                
                if (store.getCustomerList().Count >= 0)
                {
                    foreach (Customer p in store.getCustomerList())
                    {//When adding a customer check the customer list for existing record
                        if (customer.Name == p.Name && customer.Address == p.Address)
                        {
                            customer = store.findCustomer(customer.Name, customer.Address);
                            booking.Customer = customer;
                            custControl = true;
                            MessageBox.Show("Existing customer " + customer.Name + " added to booking #" + booking.BookingReference);
                            store.addBooking(booking);
                            save.Save(store.getBookingList(), store.getCustomerList());
                            updateLists();
                            lstGuests.Items.Add("Guest List booking ref#" + booking.BookingReference);
                            lstGuests.Items.Add("Booking customer - " + customer.Name);
                            clearFields();
                            txtBookRef.Text = booking.BookingReference.ToString();
                            return;
                        }
                    }
                    if (custControl == false)
                    {//if new this will generate new ref for customers first booking
                        customer.CustomerReference = reference.generateCustomerReference();
                        store.addCustomer(customer);
                        booking.Customer = customer;
                        store.addBooking(booking);
                        MessageBox.Show("New customer added to booking ref#" + booking.BookingReference +
                            "\nCustomer ref#" + customer.CustomerReference);
                        lstCustomers.Items.Add(customer.Name);
                        save.Save(store.getBookingList(), store.getCustomerList());
                        updateLists();
                        lstGuests.Items.Add("Guest List booking ref#" + booking.BookingReference);
                        lstGuests.Items.Add("Booking customer - " + customer.Name);
                        clearFields();
                        txtBookRef.Text = booking.BookingReference.ToString();
                    }
                }
            }
        }

        private void btnDelBooking_Click(object sender, RoutedEventArgs e)
        {//Retreives booking reference and deletes corresponding booking
            try
            {
                int bookingRef = int.Parse(lstBookings.SelectedItem.ToString().Substring(lstBookings.SelectedItem.ToString().Length - 1, 1));
                store.deleteBooking(bookingRef);
                save.Save(store.getBookingList(), store.getCustomerList());
                updateLists();
                lstGuests.Items.Clear();
            }
            catch
            {
                MessageBox.Show("Invalid selection for delete");
            }
        }

        private void btnDelCustomer_Click(object sender, RoutedEventArgs e)
        {//Retreives customer name and deletes corresponding customer
            try
            {
                string[] custKey = lstCustomers.SelectedItem.ToString().Split(',', '#');
                store.deleteCustomer(custKey[0], custKey[1]);
                save.Save(store.getBookingList(), store.getCustomerList());
                updateLists();
            }
            catch
            {
                MessageBox.Show("Invalid selection for delete");
            }
        }

        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {//Handles adding a guest to a selected booking
            Guest guest = new Guest();
            bool guestControl = false;

            guest.Name = txtGuestName.Text;
            guest.PassportNumber = txtPassportNo.Text;
            if (txtAge.Text != "")
            {
                try
                {
                    guest.Age = int.Parse(txtAge.Text);
                }
                catch
                {
                    MessageBox.Show("Guest age is invalid format");
                }
                
            }
            
            if (integrityCheck(guest.inputOK()) == false)
            {
                return;
            }
            else
            {
                Booking addGuest = store.findBooking(int.Parse(txtBookRef.Text));

                if (addGuest.GuestList.Count == 6)
                    return;
                foreach (Guest g in addGuest.GuestList)
                {//When adding a customer check the customer list for existing record
                    if (guest.PassportNumber == g.PassportNumber)
                    {
                        MessageBox.Show("Guest already exists on booking");
                        guestControl = true;
                        clearFields();
                        return;
                    }
                }
                if (guestControl == false)
                {//if new this will generate new ref for customers first booking
                    
                    addGuest.addGuest(guest);
                    lstGuests.Items.Add(guest.PassportNumber + "," + guest.Name);
                }
            }
        }

        private void btnDelGuest_Click(object sender, RoutedEventArgs e)
        {//Deletes selected guest from booking
            try
            {
                Booking booking = store.findBooking(int.Parse(txtBookRef.Text));
                string[] guestKey = lstGuests.SelectedItem.ToString().Split(',');

                booking.deleteGuest(guestKey[0], guestKey[1]);

                lstGuests.Items.Clear();
                lstGuests.Items.Add("Guest List booking ref#" + booking.BookingReference);
                lstGuests.Items.Add("Booking customer - " + booking.Customer.Name);
                foreach (Guest g in booking.GuestList)
                {
                    lstGuests.Items.Add(g.PassportNumber + "," + g.Name);
                }
            }
            catch
            {
                MessageBox.Show("Invalid selection for delete");
            }
            lstGuests.SelectedItem = null;
        }
        
        private void lstBookings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//When booking selection changed this displays the guest list for thast booking
            if (lstBookings.Items.Count == 0)
            {
                return;
            }
            else
            {
                lstGuests.Items.Clear();
                string[] guestKey = lstBookings.SelectedItem.ToString().Split('#',',');
                Booking booking = store.findBooking(int.Parse(guestKey[1]));

                lstGuests.Items.Add("Guest List booking ref#" + booking.BookingReference);
                lstGuests.Items.Add("Booking customer - " + booking.Customer.Name);
                txtBookRef.Text = guestKey[1];

                foreach (Guest g in booking.GuestList)
                {
                    lstGuests.Items.Add(g.PassportNumber + ", " + g.Name);
                }
            }
        }
    }
}
