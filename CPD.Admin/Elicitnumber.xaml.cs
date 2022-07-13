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

namespace CPD.Admin
{
    /// <summary>
    /// Interaction logic for ElicitDecimal.xaml
    /// </summary>
    public partial class ElicitNumber : Window
    {
        public int IntegerAnswer = 0; 
        public decimal DecimalAnswer = 0;

        public ElicitNumber(string pQuestion)
        {
            InitializeComponent();
            gQuestion.Content = pQuestion;
            gAnswer.Focus();

            this.Title = "Request for information";

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }


        private void ButtonAccept_Click(object sender, RoutedEventArgs e)
        {
 
            if (Int32.TryParse(gAnswer.Text, out IntegerAnswer))
            {
                this.Close();
            }

            if (!Decimal.TryParse(gAnswer.Text, out DecimalAnswer))
            {
                MessageBox.Show("This is not a proper integer or decimal number. Please try again");
                return;
            }
        }


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

    }
}
