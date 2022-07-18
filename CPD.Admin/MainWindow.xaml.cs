using System;
using System.IO;
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
using CPD.Data;
using CPD.Business;
using System.Data;

namespace CPD.Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Globals
            private int gCustomerId = 0;
            //private string gCertificatePrefix = "c:\\Subs\\CPDCertificate";
        private enum  Tabs
        {
            History = 0,
            Log = 1,
            Display = 2
        }


        private Data.ResultDoc gResultDoc;
            private Data.ResultDocTableAdapters.HistoryTableAdapter gHistoryAdapter = new Data.ResultDocTableAdapters.HistoryTableAdapter();
            private Data.ResultDocTableAdapters.ResultTableAdapter gResultAdapter = new Data.ResultDocTableAdapters.ResultTableAdapter(); 
            private CollectionViewSource gHistoryViewSource;

        #endregion

        #region Window management
        public MainWindow()
        {
            InitializeComponent();

            Settings.CPDConnectionString = global::CPD.Admin.Properties.Settings.Default.CPDConnectionString;
            Settings.MIMSConnectionString = global::CPD.Admin.Properties.Settings.Default.MIMSConnectionString; // Used by MIMS applications.
 
            try
            {

                // Set the Status-strip
                string[] myStatusMessages;
                char[] charSeparators = new char[] { ';' };
                myStatusMessages = Settings.CPDConnectionString.Split(charSeparators, 10, StringSplitOptions.RemoveEmptyEntries);
                string myServer = "";
                string myDataBase = "";
                string myVersion = "";
                foreach (string myMember in myStatusMessages)
                {
                    if (myMember.StartsWith("Data Source"))
                    {
                        myServer = myMember.Substring(12);
                    }

                    if (myMember.StartsWith("Initial Catalog"))
                    {
                        myDataBase = myMember.Substring(16);
                    }
                }

                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {

                    myVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }

                this.Title = "CPD on " + myServer + " on database " + myDataBase + " Version " + myVersion;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Title " + ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           gResultDoc = ((CPD.Data.ResultDoc)(this.FindResource("resultDoc")));
           gHistoryViewSource = (CollectionViewSource)this.FindResource("historyViewSource");
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            historyDataGrid.Height = this.ActualHeight - 200;
            //PDFViewer.Width = this.ActualWidth - 100;
            //DisplayFlowDocument.Height = this.ActualHeight - 50;
        }

        #endregion

        #region Search options

        private void buttonSelectCustomerId(object sender, RoutedEventArgs e)
        {
           ElicitNumber lElicitNumber = new ElicitNumber("What is the CustomerId?");
           lElicitNumber.ShowDialog();
           if (lElicitNumber.IntegerAnswer == 0)
                {
                    return;
                }

           gCustomerId = lElicitNumber.IntegerAnswer;
           labelCustomerId.Content = gCustomerId.ToString();
           gResultDoc.History.Clear();
           gHistoryAdapter.FillBy(gResultDoc.History, "History", gCustomerId, null);
           if (gResultDoc.History.Count == 0)
           {
               MessageBox.Show("Nothing found.");
           }
        }

        private void buttonSelectFromDate(object sender, RoutedEventArgs e)
        {
            try
            {
                gResultDoc.History.Clear();
                gHistoryAdapter.FillBy(gResultDoc.History, "FromDate", 0, DateFrom.SelectedDate);
                if (gResultDoc.History.Count == 0)
                {
                    MessageBox.Show("Nothing found.");
                }
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "buttonSelectFromDate", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return;
            }
        }

        #endregion

        #region Gestures and context menu

        private void Click_Mark(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.DataRowView lRowView = (System.Data.DataRowView)gHistoryViewSource.View.CurrentItem;
                if (lRowView != null)
                {
                    ResultDoc.HistoryRow lRow = (ResultDoc.HistoryRow)lRowView.Row;
                    ResultData.Mark(lRow.ResultId);
                    gResultDoc.History.Clear();
                    gHistoryAdapter.FillBy(gResultDoc.History, "History", gCustomerId, null);;
                    MessageBox.Show("Result marked.");
                }
            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ClickMark", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show(ex.Message);
            }
        }
        private void Click_Reset(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.DataRowView lRowView = (System.Data.DataRowView)gHistoryViewSource.View.CurrentItem;
                if (lRowView != null)
                {
                    ResultDoc.HistoryRow lRow = (ResultDoc.HistoryRow)lRowView.Row;

                    if (lRow.Verdict == "Passed")
                    {
                        MessageBox.Show( "A passed test cannot be reset.");
                        return;
                    }

                    gResultAdapter.Reset(gCustomerId, lRow.ModuleId);
                    gHistoryAdapter.FillBy(gResultDoc.History, "History", gCustomerId, null);
                    MessageBox.Show("Done");
                }
            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ClickReset", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show(ex.Message);
            }
        }

        private void Click_Render(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.DataRowView lRowView = (System.Data.DataRowView)gHistoryViewSource.View.CurrentItem;
                if (lRowView == null)
                {
                    throw new Exception("No row found.");
                }

                ResultDoc.HistoryRow lRow = (ResultDoc.HistoryRow)lRowView.Row;

                if (lRow.Verdict == "Failed")
                {
                    MessageBox.Show("According to my records you did not pass this test. If you think you have a case, please contact MIMS at 011 280 5533");
                    return;
                }

                CPDCertificate lCertificateDisplay = new CPDCertificate();
               
                {
                    string lResult;

                    if ((lResult = lCertificateDisplay.Render(lRow.ResultId)) != "OK")
                    {
                        LogListBox.Items.Add("Error rendering certificate for " + lRowView["CustomerId"] + " ResultId= " + lRowView["ResultId"]);
                        MessageBox.Show(lResult);
                        return;
                    }

                    else
                    {
                        LogListBox.Items.Add("Certificate rendered for " + lRowView["CustomerId"] + " ResultId= " + lRowView["ResultId"]);
                        DisplayFlowDocument.Content = lCertificateDisplay;
                        gTabControl.SelectedIndex = (int)Tabs.Display;
                        
                        //DisplayPDF();
                    }

                }
            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ClickRender", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show(ex.Message);
            }
        }

        private void Click_Email(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.DataRowView lRowView = (System.Data.DataRowView)gHistoryViewSource.View.CurrentItem;
                if (lRowView == null)
                {
                    throw new Exception("No row found.");
                }

                ResultDoc.HistoryRow lRow = (ResultDoc.HistoryRow)lRowView.Row;

                if (lRow.Verdict == "Failed")
                {
                    MessageBox.Show("According to my records you did not pass this test. If you think you have a case, please contact MIMS at 011 280 5533");
                    return;
                }

                CPDCertificate lCertificateDisplay = new CPDCertificate();

                {
                    string lResult;
                    if ((lResult = lCertificateDisplay.Render(lRow.ResultId)) != "OK")
                    {
                        return;
                    }
                }

                {
                    string lResult2;

                    if ((lResult2 = lCertificateDisplay.EmailCertificate(lRow.ResultId)) != "OK")
                    {
                        LogListBox.Items.Add("Error emailing certificate for " + lRowView["CustomerId"] + " ResultId= " + lRowView["ResultId"]);
                        MessageBox.Show(lResult2);
                        return;
                    }
                    else
                    {
                        LogListBox.Items.Add("Certificate emailed for " + lRowView["CustomerId"] + " ResultId= " + lRowView["ResultId"]);
                        DisplayFlowDocument.Content = lCertificateDisplay;

                        lRow.DateIssued = DateTime.Now;
                        gResultAdapter.RecordIssueOfCertificate(lRow.ResultId, lRow.DateIssued);
                        MessageBox.Show("Done.");
                    }
                }
            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "Click_Email", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show(ex.Message);
            }
        }

        //private void DisplayPDF()
        //{
        //    try
        //    {

        //        System.Data.DataRowView lRowView = (System.Data.DataRowView)gHistoryViewSource.View.CurrentItem;
        //        if (lRowView == null)
        //        {
        //            throw new Exception("No row found.");
        //        }

        //        ResultDoc.HistoryRow lRow = (ResultDoc.HistoryRow)lRowView.Row;

        //        if (File.Exists(gCertificatePrefix + lRow.ResultId.ToString() + ".pdf"))
        //        {
        //            PDFViewer.Navigate("file:///" + gCertificatePrefix + lRow.ResultId.ToString() + ".pdf");
        //        }
        //        else
        //        {
        //            MessageBox.Show("File not rendered yet.");
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        //Display all the exceptions

        //        Exception CurrentException = ex;
        //        int ExceptionLevel = 0;
        //        do
        //        {
        //            ExceptionLevel++;
        //            ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "Update", "");
        //            CurrentException = CurrentException.InnerException;
        //        } while (CurrentException != null);

        //        MessageBox.Show(ex.Message);
        //    }
        //}
            


        //private void HistoryDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    DisplayPDF();
        //}

        #endregion

        #region Batch operations

        private void ButtonSelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (DataRowView lRowView in gHistoryViewSource.View)
            {
                ResultDoc.HistoryRow lRow = (ResultDoc.HistoryRow)lRowView.Row;


                if (lRow.Verdict == "Unsuccessful")
                {
                    continue;
                }
                else
                {
                    historyDataGrid.SelectedItems.Add(lRowView);
                }
            }
        }

        private void ButtonDeselectAll_Click(object sender, RoutedEventArgs e)
        {
            historyDataGrid.UnselectAll();
        }

        private void buttonRenderAndEmailSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;

 

                foreach (DataRowView lDataRowView in historyDataGrid.SelectedItems)
                {
                    CPD.Data.ResultDoc.HistoryRow lRow = (ResultDoc.HistoryRow)lDataRowView.Row;

                    if (lRow.Verdict != "Passed")
                    {
                        continue;
                    }

                    CPD.Business.CPDCertificate lCertificate = new Business.CPDCertificate();

                    {
                        string lResult;
                        if ((lResult = lCertificate.Render(lRow.ResultId)) != "OK")
                        {
                            LogListBox.Items.Add("Error rendering certificate for " + lRow.CustomerId.ToString() + " ResultId= " + lRow.ResultId.ToString());

                            return;
                        }
                        else 
                        {
                            LogListBox.Items.Add("Certificate rendered for " + lRow.CustomerId.ToString() + " ResultId= " + lRow.ResultId.ToString());

                        }
                    }

                    {
                        string lResult;
                        if ((lResult = lCertificate.EmailCertificate(lRow.ResultId)) != "OK")
                        {
                            LogListBox.Items.Add("Error emailing certificate for " + lRow.CustomerId.ToString() + " ResultId= " + lRow.ResultId.ToString() + " " + lResult);

                            return;
                        }
                        else
                        {
                            LogListBox.Items.Add("Certificate emailed for " + lRow.CustomerId.ToString() + " ResultId= " + lRow.ResultId.ToString());
                            //DisplayFlowDocument.Content = lCertificateDisplay;
                            lRow.DateIssued = DateTime.Now;
                            gResultAdapter.RecordIssueOfCertificate(lRow.ResultId, lRow.DateIssued);
                        }
                    }
                }  // End of loop
                   
                MessageBox.Show("Done.");
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "buttonEmailSelected", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return;
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        #endregion

        private void buttonTest(object sender, RoutedEventArgs e)
        {
            string lResult = ModuleData.GetQuestions(156);
            MessageBox.Show(lResult);
        }
    }
}
