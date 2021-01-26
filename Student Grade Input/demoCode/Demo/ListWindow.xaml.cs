/*
Author: Sean Mooney | 40283592
Student record input programme: takes input data and creates student objects
ListWindow.xaml.cs: Has code for the list window that will display all student records from the list
Last modified: 24/10/2017
*/

using BusinessObjects;
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
using System.Windows.Shapes;

namespace Demo
{
    /// <summary>
    /// Interaction logic for ListWindow.xaml
    /// </summary>
    public partial class ListWindow : Window
    {
        public ListWindow(ModuleList store)
        {//Initialise list window
            InitializeComponent();
            //Loop through each matric number stored
            foreach(int matric in store.matrics)
            {//For each number add all student data to list box
                lstDisplayAll.Items.Add(store.find(matric).FirstName + " " + store.find(matric).Surname + "\n" +
                    store.find(matric).DOB + "\nCoursework mark- " + store.find(matric).Coursework + ", Exam mark- " +
                    store.find(matric).Exam + "\nFinal grade - " + store.find(matric).getMark() + "%\n-----");
            }
        }
        
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {//Method closes list window when Back button clicked
            this.Close();
        }
    }
}
