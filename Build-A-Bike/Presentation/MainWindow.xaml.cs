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
using BusinessLayer;
using DataLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PresentationLayer
{
    public partial class MainWindow : Window
    {
        private Lists store = new Lists();
        private SingletonOrderNumberGenerator reference = SingletonOrderNumberGenerator.Instance();

        public MainWindow()
        {
            InitializeComponent();

            ((TextBox)txtName).FontSize = 11;
            ((TextBox)txtAddress).FontSize = 11;
            ((TextBox)txtEmail).FontSize = 11;
            ((TextBox)txtBankNum).FontSize = 11;
            ((TextBox)txtBankPin).FontSize = 11;

            // Get cmboxs to display default messages!!!!!
            cmboxFrame.Text = "Select a frame";
            cmboxGears.Text = "Select gears";
            cmboxBrakes.Text = "Select brakes";
            cmboxWheels.Text = "Select wheels";
            cmboxHandlebars.Text = "Select handlebars";
            cmboxSaddle.Text = "Select a saddle";

            // Held strings symbolise stock sold by company
            string[] frames = { "Specialized", "Trek", "Boardman", "Planet X" };
            string[] gears = { "Shimano", "Sram", "Campagnolo" };
            string[] brakes = { "Shimano", "Sram", "Hope" };
            string[] wheels = { "Shimano", "Mavic", "Hope" };
            string[] handlebars = { "Specialized", "Trek", "Boardman", "Planet X" };
            string[] saddle = { "Selle Italia", "Brooks", "Prologo" };

            for (int i = 0; i < frames.Length; i++)
            {
                cmboxFrame.Items.Add(frames[i]);
            }
            for (int i = 0; i < gears.Length; i++)
            {
                cmboxGears.Items.Add(gears[i]);
            }
            for (int i = 0; i < brakes.Length; i++)
            {
                cmboxBrakes.Items.Add(brakes[i]);
            }
            for (int i = 0; i < wheels.Length; i++)
            {
                cmboxWheels.Items.Add(wheels[i]);
            }
            for (int i = 0; i < handlebars.Length; i++)
            {
                cmboxHandlebars.Items.Add(handlebars[i]);
            }
            for (int i = 0; i < saddle.Length; i++)
            {
                cmboxSaddle.Items.Add(saddle[i]);
            }

            //Load mock stock items
            store.addFrame("Specialized");
            store.addFrame("Specialized");
            store.addFrame("Trek");
            store.addFrame("Boardman");
            store.addFrame("Boardman");
            store.addFrame("Boardman");
            store.addGears("Shimano");
            store.addGears("Shimano");
            store.addGears("Shimano");
            store.addGears("Shimano");
            store.addGears("Sram");
            store.addGears("Sram");
            store.addBrakes("Shimano");
            store.addBrakes("Shimano");
            store.addBrakes("Shimano");
            store.addBrakes("Sram");
            store.addBrakes("Hope");
            store.addWheels("Shimano");
            store.addWheels("Shimano");
            store.addWheels("Hope");
            store.addHandlebars("Specialized");
            store.addHandlebars("Trek");
            store.addHandlebars("Trek");
            store.addHandlebars("Boardman");
            store.addSaddle("Selle Italia");
            store.addSaddle("Brooks");
            store.addSaddle("Prologo");
            store.addSaddle("Prologo");
            store.addSaddle("Prologo");
            
            refresh_bike_display();
            restrict_inputs();
        }
        
        /* GLOBALS */
        bool temp_warranty;
        int input_control = 0;
        string[] bike_temp = new string[6];
        bool[] inStock = new bool[6];
        Customer tempCustomer;
        string completeTemp;
        int total = 0;
        

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

        private void clearBikeFields()
        {//Clears bike input fields
            for (int i = 0; i < bike_temp.Length; i++)
            {
                bike_temp[i] = "";
            }
            refresh_bike_display();
            cmboxFrame.SelectedIndex = -1;
            cmboxGears.SelectedIndex = -1;
            cmboxBrakes.SelectedIndex = -1;
            cmboxWheels.SelectedIndex = -1;
            cmboxHandlebars.SelectedIndex = -1;
            cmboxSaddle.SelectedIndex = -1;
        }

        void restrict_inputs()
        {//Disables confirm button for sections not in use
            switch (input_control)
            {
                case 0:
                    //Customer and bank details inputs inactive
                    btnConfirmBike.IsEnabled = true;
                    btnConfirmCust.IsEnabled = false;
                    btnConfirmBank.IsEnabled = false;
                    break;
                case 1:
                    //Bike and bank inputs inactive
                    btnConfirmBike.IsEnabled = false;
                    btnConfirmBank.IsEnabled = false;
                    btnConfirmCust.IsEnabled = true;
                    break;
                case 2:
                    //Bike and Customer inputs inactive
                    btnConfirmCust.IsEnabled = false;
                    btnConfirmBike.IsEnabled = false;
                    btnConfirmBank.IsEnabled = true;
                    break;
            }
        }

        void refresh_bike_display()
        {
            lstBike.Items.Clear();
            string listItem = "";
            for (int i = 0; i < bike_temp.Length; i++)
            {
                if (i == 0)
                    listItem = "Frame: " + bike_temp[i];
                if (i == 1)
                    listItem = "Gears: " + bike_temp[i];
                if (i == 2)
                    listItem = "Brakes: " + bike_temp[i];
                if (i == 3)
                    listItem = "Wheels: " + bike_temp[i];
                if (i == 4)
                    listItem = "Handlebars: " + bike_temp[i];
                if (i == 5)
                    listItem = "Saddle: " + bike_temp[i];
                
                lstBike.Items.Add(listItem);
            }
        }

        void checkStock(string[] bike)
        {//Checks stock class for existing items
            if (store.findFrame(bike[0]) == null)
            {
                MessageBox.Show(bike[0] + " frame out of stock");
                inStock[0] = false;
            }
            else
            {
                MessageBox.Show(bike[0] + " frame in stock");
                inStock[0] = true;
            }

            if (store.findGears(bike[1]) == null)
            {
                MessageBox.Show(bike[1] + " gears out of stock");
                inStock[1] = false;
            }
            else
            {
                MessageBox.Show(bike[1] + " gears in stock");
                inStock[1] = true;
            }

            if (store.findBrakes(bike[2]) == null)
            {
                MessageBox.Show(bike[2] + " brakes out of stock");
                inStock[2] = false;
            }
            else
            {
                MessageBox.Show(bike[2] + " brakes in stock");
                inStock[2] = true;
            }

            if (store.findWheels(bike[3]) == null)
            {
                MessageBox.Show(bike[3] + " wheels out of stock");
                inStock[3] = false;
            }
            else
            {
                MessageBox.Show(bike[3] + " wheels in stock");
                inStock[3] = true;
            }

            if (store.findHandlebars(bike[4]) == null)
            {
                MessageBox.Show(bike[4] + " handlebars out of stock");
                inStock[4] = false;
            }
            else
            {
                MessageBox.Show(bike[4] + " handlebars in stock");
                inStock[4] = true;
            }

            if (store.findSaddle(bike[5]) == null)
            {
                MessageBox.Show(bike[5] + " saddle out of stock");
                inStock[5] = false;
            }
            else
            {
                MessageBox.Show(bike[5] + " saddle in stock");
                inStock[5] = true;
            }
        }

        void balanceStock()
        {
            store.deleteFrame(bike_temp[0]);
            store.deleteGears(bike_temp[1]);
            store.deleteBrakes(bike_temp[2]);
            store.deleteWheels(bike_temp[3]);
            store.deleteHandlebars(bike_temp[4]);
            store.deleteSaddle(bike_temp[5]);
        }

        private void btnAddFrame_Click(object sender, RoutedEventArgs e)
        {
            //catch error if no item selected
            if (cmboxFrame.SelectedItem == null)
            {
                MessageBox.Show("Please select a frame!");
            }
            else
            {
                bike_temp[0] = cmboxFrame.SelectedItem.ToString();
            }
            refresh_bike_display();
        }

        private void btnRemoveFrame_Click(object sender, RoutedEventArgs e)
        {
            bike_temp[0] = "";
            refresh_bike_display();
        }

        private void btnAddGears_Click(object sender, RoutedEventArgs e)
        {
            if (cmboxGears.SelectedItem == null)
            {
                MessageBox.Show("Please select gears!");
            }
            else
            {
                bike_temp[1] = cmboxGears.SelectedItem.ToString();
            }
            refresh_bike_display();
        }

        private void btnRemoveGears_Click(object sender, RoutedEventArgs e)
        {
            bike_temp[1] = "";
            refresh_bike_display();
        }

        private void btnAddBrakes_Click(object sender, RoutedEventArgs e)
        {
            if (cmboxBrakes.SelectedItem == null)
            {
                MessageBox.Show("Please select brakes!");
            }
            else
            {
                bike_temp[2] = cmboxBrakes.SelectedItem.ToString();
            }
            refresh_bike_display();
        }

        private void btnRemoveBrakes_Click(object sender, RoutedEventArgs e)
        {
            bike_temp[2] = "";
            refresh_bike_display();
        }

        private void btnAddWheels_Click(object sender, RoutedEventArgs e)
        {
            if (cmboxWheels.SelectedItem == null)
            {
                MessageBox.Show("Please select wheels!");
            }
            else
            {
                bike_temp[3] = cmboxWheels.SelectedItem.ToString();
            }
            refresh_bike_display();
        }

        private void btnRemoveWheels_Click(object sender, RoutedEventArgs e)
        {
            bike_temp[3] = "";
            refresh_bike_display();
        }

        private void btnAddHandlebars_Click(object sender, RoutedEventArgs e)
        {
            if (cmboxHandlebars.SelectedItem == null)
            {
                MessageBox.Show("Please select handlebars!");
            }
            else
            {
                bike_temp[4] = cmboxHandlebars.SelectedItem.ToString();
            }
            refresh_bike_display();
        }

        private void btnRemoveHandlebars_Click(object sender, RoutedEventArgs e)
        {
            bike_temp[4] = "";
            refresh_bike_display();
        }

        private void btnAddSaddle_Click(object sender, RoutedEventArgs e)
        {
            if (cmboxBrakes.SelectedItem == null)
            {
                MessageBox.Show("Please select a saddle!");
            }
            else
            {
                bike_temp[5] = cmboxSaddle.SelectedItem.ToString();
            }
            refresh_bike_display();
        }

        private void btnRemoveSaddle_Click(object sender, RoutedEventArgs e)
        {
            bike_temp[5] = "";
            refresh_bike_display();
        }

        private void btnConfirmBike_Click(object sender, RoutedEventArgs e)
        {
            int flag = 1;
            int bikePrice = 0;
            int warranty = 0;
            
            List<Bike> bike_list_temp = new List<Bike>();

            foreach (string p in bike_temp)
            {
                if (p == null)
                {
                    flag = 0;
                    break;
                }
            }

            if (flag == 0)
            {
                MessageBox.Show("Please select all components");
            }
            else
            {
                Bike newBike = new Bike();
                //Confirm box
                if (MessageBox.Show("Frame: " + bike_temp[0] + "\nGears: " + bike_temp[1] + "\nBrakes: " + bike_temp[2] + "\nWheels: " +
                    bike_temp[3] + "\nHandlebars" + bike_temp[4] + "\nSaddle" + bike_temp[5], "Is this correct?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {//If no clear selection
                    clearBikeFields();
                    flag = 0;
                }
                else
                {//If yes populate new bike object with selected items
                    newBike.Frame = bike_temp[0];
                    newBike.Gears = bike_temp[1];
                    newBike.Brakes = bike_temp[2];
                    newBike.Wheels = bike_temp[3];
                    newBike.Handlebars = bike_temp[4];
                    newBike.Saddle = bike_temp[5];

                    if (MessageBox.Show("Would you like extended 3 year warranty for an extra £50?", "Confirm",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {//No 
                        temp_warranty = false;
                    }
                    else
                    {//Yes
                        temp_warranty = true;
                        warranty = 50;
                    }
                }
                // CONFIRM PRICE
                int frame = store.getFramePrice(bike_temp[0]);
                int gears = store.getGearsPrice(bike_temp[1]);
                int brakes = store.getBrakesPrice(bike_temp[2]);
                int wheels = store.getWheelsPrice(bike_temp[3]);
                int handlebars = store.gethandlebarsPrice(bike_temp[4]);
                int saddle = store.getSaddlePrice(bike_temp[5]);
                bikePrice = frame + gears + brakes + wheels + handlebars + saddle + 100 + warranty;
                total = bikePrice;

                MessageBox.Show("Frame: £" + frame + "\nGears: £" + gears + "\nBrakes: £" + brakes + "\nWheels: £" + wheels +
                    "\nHandlebars: £" + handlebars + "\nSaddle: £" + saddle + "\nTesting & Building: £" + 100 + "\nExtended Warranty: £" + warranty + "\n\nTotal: £" + bikePrice);

                // CHECK FOR AVAILABILITY OF STOCK
                checkStock(bike_temp);


                // DISPLAY ESTIMATED TIME FOR COMPLETION
                if (integrityCheck(inStock) == false)
                {
                    DateTime today = DateTime.Now;
                    DateTime complete = today.AddDays(7.5);
                    MessageBox.Show("ESTIMATED TIME FOR COMPLETION\n" + complete.ToString());
                    completeTemp = complete.ToString();
                }
                else
                {
                    DateTime today = DateTime.Now;
                    DateTime complete = today.AddHours(12);
                    MessageBox.Show("ESTIMATED TIME FOR COMPLETION\n" + complete.ToString());
                    completeTemp = complete.ToString();
                }

                if (flag == 0)
                {
                    MessageBox.Show("Cancelling process");
                }
                else
                {
                    if (MessageBox.Show("Another bike, sir?", "Yeeee",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {//If no store bike info and ready for customer details input
                        newBike.Reference = reference.generateBikeReference();
                        bike_list_temp.Add(newBike);
                        store.addBike(newBike);
                        balanceStock();
                        input_control = 1;
                        restrict_inputs();
                        clearBikeFields();
                        MessageBox.Show("Enter customer details");
                    }
                    else
                    {//If yes clear input fields and allow for new bike selection
                        newBike.Reference = reference.generateBikeReference();
                        bike_list_temp.Add(newBike);
                        store.addBike(newBike);
                        balanceStock();
                        clearBikeFields();
                    }
                }
            }
        }

        private void btnConfirmCust_Click(object sender, RoutedEventArgs e)
        {
            Customer newCustomer = new Customer();
            newCustomer.Name = txtName.Text;
            newCustomer.Address = txtAddress.Text;
            newCustomer.Email = txtEmail.Text;

            //if inputs are okay, create new customer object
            if (integrityCheck(newCustomer.customer_details_inputOK()) == false )
            {//If inputs are incorrect do nothing
                
            }
            else
            {//store new object in order
                tempCustomer = newCustomer;
                input_control = 2;
                txtAddress.Text = "";
                txtName.Text = "";
                txtEmail.Text = "";
                restrict_inputs();
                MessageBox.Show("Enter customer bank details");
            } 
        }

        private void btnConfirmBank_Click(object sender, RoutedEventArgs e)
        {
            tempCustomer.BankNum = Int64.Parse(txtBankNum.Text);
            tempCustomer.BankPin = Int64.Parse(txtBankPin.Text);

            //if inputs are okay, add to customer
            if (integrityCheck(tempCustomer.bank_details_inputOK()) == false)
            {//If inputs are incorrect do nothing
                
            }
            else
            {//store all temp details in new order object
                Order newOrder = new Order();
                newOrder.OrderNumber = reference.generateOrderReference();
                newOrder.OrderDate = DateTime.Now;
                newOrder.Customer = tempCustomer;
                newOrder.Warranty = temp_warranty;

                store.addOrder(newOrder);

                txtBankNum.Text = "";
                txtBankPin.Text = "";

                //GENERATE RECEIPT
                MessageBox.Show("Order successful\nEstimated completion: " + completeTemp + "\nTotal: £" + total);

                input_control = 0;
                restrict_inputs();
            }
        }
    }
}
