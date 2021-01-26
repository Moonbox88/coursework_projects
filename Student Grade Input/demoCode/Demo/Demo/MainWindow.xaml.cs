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
        {//Function clears all textbox and display data when called
            txtMatric.Text = null;
            txtFirstName.Text = null;
            txtSurname.Text = null;
            txtCW.Text = null;
            txtExam.Text = null;
            txtDOB.Text = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {    
            if (txtMatric.Text == "" || txtFirstName.Text == "" || txtSurname.Text == "" || txtCW.Text == "" || txtExam.Text == "" || txtDOB.Text == "")
            {
                MessageBox.Show("One or more fields is empty!");
            }
            else
            {
                string matric = txtMatric.Text;
                int tempMatric = int.Parse(matric);
                string tempFirst = txtFirstName.Text;
                string tempSur = txtSurname.Text;
                string CW = txtCW.Text;
                double tempCW = Double.Parse(CW);
                string Exam = txtExam.Text;
                double tempExam = Double.Parse(Exam);
                string tempDOB = txtDOB.Text;
                //CALCULATE PERCENTAGE FROM RESULTS AND ADD THIS FOR DISPLAY !!!

                if (MessageBox.Show("Create new student record?", "Add", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                    clearData();
                }
                else
                {
                    //do yes stuff
                    Student tempRecord = new Student();

                    tempRecord.Matric = tempMatric;
                    tempRecord.FirstName = tempFirst;
                    tempRecord.Surname = txtSurname.Text;
                    tempRecord.Coursework = tempCW;
                    tempRecord.Exam = tempExam;
                    tempRecord.DOB = tempDOB;

                    if (MessageBox.Show(tempRecord.Matric + ", " + tempRecord.FirstName + ", " + tempRecord.Surname + ",\n" +
                        tempRecord.Coursework + ", " + tempRecord.Exam + ", " + tempRecord.DOB, "Add", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff
                        clearData();
                    }
                    else
                    {
                        //do yes stuff
                        store.add(tempRecord);
                        //ALSO ADD MATRICS TO LIST MATRICS AND LISTBOX ON FORM
                        MessageBox.Show("Student record added.");
                        clearData();
                    }
                }
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            Student tempRecord = new Student();
            int tempMatric = int.Parse(txtMatric.Text);
            tempRecord = store.find(tempMatric);
            if (tempRecord != null)
            {
                //SHOW THIS DATA ON THE FORM NOT IN MESSAGE BOX!!!
                MessageBox.Show(tempRecord.Matric + ", " + tempRecord.FirstName + ", " + tempRecord.Surname + ",\n" +  
                tempRecord.Coursework + ", " + tempRecord.Exam + ", " + tempRecord.DOB);
                clearData();
            }
            else
            {
                MessageBox.Show("Record does not exist!");
                clearData();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Student tempRecord = new Student();
            int tempMatric = int.Parse(txtMatric.Text);
            tempRecord = store.find(tempMatric);

            if (tempRecord != null)
            {

                if (MessageBox.Show(tempRecord.Matric + ", " + tempRecord.FirstName + ", " + tempRecord.Surname + ",\n" +
                tempRecord.Coursework + ", " + tempRecord.Exam + ", " + tempRecord.DOB, "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                    clearData();
                }
                else
                {
                    //do yes stuff
                    store.delete(tempMatric);
                    MessageBox.Show("Record deleted!");
                    clearData();
                }

            }
            else
            {
                clearData();
            }
        }

        private void btnListAll_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
