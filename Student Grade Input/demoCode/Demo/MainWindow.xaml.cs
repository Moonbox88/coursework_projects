/*
Author: Sean Mooney | 40283592
Student record input programme: takes input data and creates student objects
MainWindow.xaml.cs: Has code for the main window in the programme for processing the input data
Last modified: 24/10/2017
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModuleList store = new ModuleList();
        //Int for keeping count of entered students in order to increment 1 to each allocated matriculation no.
        private int studentCount = 0;

        public MainWindow()
        {//Initialises main window and controls size of input font
            InitializeComponent();
            ((TextBox)txtMatric).FontSize = 11;
            ((TextBox)txtFirstName).FontSize = 11;
            ((TextBox)txtSurname).FontSize = 11;
            ((TextBox)txtCW).FontSize = 11;
            ((TextBox)txtExam).FontSize = 11;
            ((TextBox)txtDOB).FontSize = 11;
        }

        public void clearData()
        {//Method clears all textbox data when called
            txtMatric.Text = null;
            txtFirstName.Text = null;
            txtSurname.Text = null;
            txtCW.Text = null;
            txtExam.Text = null;
            txtDOB.Text = null;
        }

        public int generateMatric()
        {//Method will generate matriculation numbers starting at 10001 and incrementing by one each time
            //a new record is created
            int matric = 10001;
            matric += studentCount;
            studentCount++;
            return matric;
        }

        public void displayStudent(Student tempRecord)
        {//Method will display selected student data in the display window when called
            lblDisplay.Content = tempRecord.Matric + ", " + tempRecord.FirstName + " " + tempRecord.Surname +
            ", " + tempRecord.DOB + ",\ncw result- " + tempRecord.Coursework + ", exam result- " +
            tempRecord.Exam;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {//Add method adds new student records to the list
         //Activated by clicking Add button
         //*********************************************
         //Check for existence of input data
            if (txtFirstName.Text == "" || txtSurname.Text == "" || txtCW.Text == "" || txtExam.Text == "" || txtDOB.Text == "")
            {//If any descrepancies messagebox displays error message and exit method
                MessageBox.Show("One or more neccesary fields is empty!");
                return;
            }
            else if (txtMatric.Text != "")
            {
                MessageBox.Show("Matriculation number is automatically generated\n" +
                    "for new students. Please leave blank!");
                txtMatric.Text = null;
                return;
            }
            //Create new student record
            Student tempRecord = new Student();
            //Populate record with variables
            tempRecord.FirstName = txtFirstName.Text;
            tempRecord.Surname = txtSurname.Text;
            tempRecord.Coursework = Double.Parse(txtCW.Text);
            tempRecord.Exam = Double.Parse(txtExam.Text);
            tempRecord.DOB = txtDOB.Text;
            //Bool value to control if student record is created 
            bool inputsOK = true;

            foreach (bool check in tempRecord.InputOK)
            {//Checks all elements of bool[] in student class for input failures
                if (!check)
                {//If any found sets check bool to false
                    inputsOK = false;
                }
            }
            if (inputsOK)
            {//If no inputs fail then student record is created
                int tempMatric = generateMatric();
                tempRecord.Matric = tempMatric;
                //Message box displays input data and requests yes or no to add new record to list
                if (MessageBox.Show(tempRecord.Matric + ", " + tempRecord.FirstName + " " + tempRecord.Surname + ", " + tempRecord.DOB + ",\ncw mark- " +
                    tempRecord.Coursework + ", exam mark- " + tempRecord.Exam, "Add", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {//If no clear text box inputs
                    clearData();
                }
                else
                {//If yes store new record in list and matric in listbox
                    store.add(tempRecord);
                    store.matrics.Add(tempRecord.Matric);
                    lstBoxMatrics.Items.Add(tempRecord.Matric);
                    MessageBox.Show("Student record added.");
                    clearData();
                }
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {//Find method to find and display student records from the list
         //Activated by clicking find button
         //**********************************************************
         //Clears any record previously displayed in display label
            lblDisplay.Content = null;
            //Create variable for input search number
            int tempMatric = int.Parse(txtMatric.Text);

            if (txtMatric.Text == "")
            {//Check for existence and integrity of input search number
                MessageBox.Show("No matriculation number provided!");
            }
            else if (txtFirstName.Text != "" || txtSurname.Text != "" || txtCW.Text != "" || txtExam.Text != "" || txtDOB.Text != "")
            {
                MessageBox.Show("Please only enter matriculation number for Find function!");
                clearData();
            }
            else if (tempMatric < 10001 || tempMatric > 50000)
            {
                MessageBox.Show("Entered matriculation number invalid!");
                clearData();
            }
            else
            {//If checks okay attempt to populate temp record with student data matching search input
                Student tempRecord = store.find(tempMatric);
                
                if (tempRecord != null)
                {//If record found display on display label
                    displayStudent(tempRecord);
                }
                else
                {//If no student data matching input is found display error message
                    MessageBox.Show("Record does not exist!");
                    clearData();
                }
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {//Del method deletes student records from the list
         //Activated by clicking Delete button
         //**********************************************
         //Clear any record previously displayed in display label
            lblDisplay.Content = null;
            int tempMatric = int.Parse(txtMatric.Text);
            //Check itegrity of input matric number
            if (txtFirstName.Text != "" || txtSurname.Text != "" || txtCW.Text != "" || txtExam.Text != "" || txtDOB.Text != "")
            {
                MessageBox.Show("Please only enter matriculation number for Delete function!");
                clearData();
            }
            else if (txtMatric.Text == "")
            {
                MessageBox.Show("No matriculation number provided!");
            }
            else if (tempMatric < 10001 || tempMatric > 50000)
            {
                MessageBox.Show("Entered matriculation number invalid!");
                clearData();
            }
            else
            {//Create temp record and populate with record found in list
                Student tempRecord = store.find(tempMatric);
                
                if (tempRecord != null)
                {//If check okay display message box with found student record
                    //and yes no buttons to delete student record or cancel
                    if (MessageBox.Show(tempRecord.Matric + ", " + tempRecord.FirstName + " " + tempRecord.Surname + ", " + tempRecord.DOB + ",\ncw result- " +
                    tempRecord.Coursework + ", exam result- " + tempRecord.Exam, "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {//If no clear text box inputs
                        clearData();
                    }
                    else
                    {//If yes delete student record from list
                        lstBoxMatrics.UnselectAll();
                        lstBoxMatrics.Items.Remove(tempRecord.Matric);
                        store.delete(tempMatric);
                        MessageBox.Show("Record deleted!");
                        clearData();
                    }
                }
                else
                {//If no matching student record is found display error message
                    MessageBox.Show("Record does not exist!");
                    clearData();
                }
            }
        }

        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {//Method displays a new window where all stored student records are displayed
         //Activated by clicking List All button
            ListWindow listMatrics = new ListWindow(store);
            listMatrics.Show();
        }

        private void lstBoxMatrics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//Method displays student records in display window when matric numbers in listbox are selected 
            if (lstBoxMatrics.SelectedItem != null)
            {
                int tempMatric = int.Parse(lstBoxMatrics.SelectedItem.ToString());
                Student tempRecord = store.find(tempMatric);
                double result = tempRecord.getMark();
                txtMatric.Text = tempRecord.Matric.ToString();
                displayStudent(tempRecord);
            }
        }
    }
}
