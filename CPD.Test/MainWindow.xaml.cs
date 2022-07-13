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
using CPD.Business;
using CPD.Data;
using CPD.Test.ServiceReference1;

namespace CPD.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public CPD.Test.ServiceReference1.CustomerInfo GetCustomerInfo(int pCustomerId)
        {
            CPD.Test.ServiceReference1.ActivatorClient lClient = new ActivatorClient();
            CustomerInfo lCustomerInfo = new CustomerInfo();
            try
            {
                GetCustomerInfoRequest lRequest = new GetCustomerInfoRequest();
                lRequest.CustomerId = pCustomerId;
                CPD.Test.ServiceReference1.GetCustomerInfoResponse lResponse = lClient.GetCustomerInfo(lRequest);

                lCustomerInfo = lResponse.GetCustomerInfoResult;
                return lCustomerInfo;
            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, "static ResultBiz", "GetCustomerInfo", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);
                return lCustomerInfo;
            }
        }


        private void BusinessResult(object sender, RoutedEventArgs e)
        {
            CPD.Test.ServiceReference1.CustomerInfo lCustomerInfo = GetCustomerInfo(108244);
            MessageBox.Show(lCustomerInfo.FullName);

        }
    }
}
