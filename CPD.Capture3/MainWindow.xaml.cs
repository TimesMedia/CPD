using System;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CPD.Data;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace CPD.Capture3
{
    public partial class MainWindow : Window
    {
        Data.QuestionareDocTableAdapters.Survey2TableAdapter gSurveyAdapter = new Data.QuestionareDocTableAdapters.Survey2TableAdapter();
        Data.QuestionareDocTableAdapters.ModuleTableAdapter gModuleAdapter = new Data.QuestionareDocTableAdapters.ModuleTableAdapter();
        Data.QuestionareDocTableAdapters.ArticleTableAdapter gArticleAdapter = new Data.QuestionareDocTableAdapters.ArticleTableAdapter();
        Data.QuestionareDocTableAdapters.QuestionTableAdapter gQuestionAdapter = new Data.QuestionareDocTableAdapters.QuestionTableAdapter();
        Data.QuestionareDocTableAdapters.AnswerTableAdapter gAnswerAdapter = new Data.QuestionareDocTableAdapters.AnswerTableAdapter();

        Data.QuestionareDoc gQuestionareDoc;
        CollectionViewSource gSurveyViewSource;
        CollectionViewSource gModuleViewSource;

        List<string> gFacility = new List<string>() { "CPD", "Read only" };
 
        public MainWindow()
        {
            InitializeComponent();

            Settings.CPDConnectionString = global::CPD.Capture3.Properties.Settings.Default.CPDConnectionString;
            gSurveyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("SurveyViewSource")));
            gModuleViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("ModuleViewSource")));

            string lStage = "Before try";


            try
            {
                // Set the Status-strip
                string[] myStatusMessages;
                char[] charSeparators = new char[] { ';' };
                myStatusMessages = Settings.CPDConnectionString.Split(charSeparators, 10, StringSplitOptions.RemoveEmptyEntries);
                string myServer = "";
                string myDataBase = "";
                string myVersion = "";

                lStage = "Before foreach";

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

                lStage = "Before Currentversion";
                myVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();

                lStage = "Before Title";

                this.Title = "CPD question capture facility " + myServer + " on database " + myDataBase + " Version " + myVersion;
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "MainWindow", lStage);
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);
              
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gQuestionareDoc = ((QuestionareDoc)(this.FindResource("QuestionareDoc")));

            try
            {
                gSurveyAdapter.AttachConnection();
                gModuleAdapter.AttachConnection();
                gArticleAdapter.AttachConnection();
                gQuestionAdapter.AttachConnection();
                gAnswerAdapter.AttachConnection();
                LoadAll();
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "Window_Loaded", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show("Window_Loaded; " + ex.Message);
            }

        }

        private void LoadAll()
        {
            gQuestionareDoc.Answer.Clear();
            gQuestionareDoc.Question.Clear();
            gQuestionareDoc.Article.Clear();
            gQuestionareDoc.Module.Clear();
            gQuestionareDoc.Survey2.Clear();

            gSurveyAdapter.Fill(gQuestionareDoc.Survey2);
            gModuleAdapter.Fill(gQuestionareDoc.Module);
            gArticleAdapter.Fill(gQuestionareDoc.Article);
            gQuestionAdapter.Fill(gQuestionareDoc.Question);
            gAnswerAdapter.Fill(gQuestionareDoc.Answer);
            ListFacility.ItemsSource = gFacility;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAddSurvey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QuestionareDoc.Survey2Row lNewRow = gQuestionareDoc.Survey2.NewSurvey2Row();
                lNewRow.Naam = "ZZZ New Survey";
                lNewRow.IssueId = 0;
                gQuestionareDoc.Survey2.AddSurvey2Row(lNewRow);
                MessageBox.Show("OK a row has been added with the name ZZZ New Survey . Look at the bottom of the listing on the left.");
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonAddSurvey_Click", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

               MessageBox.Show("Error in ButtonAddSurvey_Click: " + ex.Message);
            }

        }
        private void ButtonUpdateSurvey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gSurveyAdapter.Update(gQuestionareDoc.Survey2);
                LoadAll();

                MessageBox.Show("Survey data updated successfully.");
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonUpdateSurvey_Click", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show("Update of Survey failed: " + ex.Message);
            }
        }

        private void SurveyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView lRowView = (DataRowView)gSurveyViewSource.View.CurrentItem;

                if (lRowView == null)
                {
                    return;
                }

                QuestionareDoc.Survey2Row lRow = (QuestionareDoc.Survey2Row)lRowView.Row;

                TextRange lTextRange;
                System.IO.FileStream lFileStream;

                if (!System.IO.File.Exists(lRow.XMLFileName))
                {
                    return;
                }

                lTextRange = new TextRange(gRichTextBox.Document.ContentStart, gRichTextBox.Document.ContentEnd);
                using (lFileStream = new System.IO.FileStream(lRow.XMLFileName, System.IO.FileMode.OpenOrCreate))
                {
                    lTextRange.Load(lFileStream, System.Windows.DataFormats.Rtf);
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
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "SurveyDataGrid_SelectionChanged", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show("Error in SurveyDataGrid_SelectionChanged: " + ex.Message);
            }
 
        }

        private void ButtonUpdateModule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gModuleAdapter.Update(gQuestionareDoc.Module);
                gQuestionareDoc.Module.AcceptChanges();

                gQuestionareDoc.Answer.Clear();
                gQuestionareDoc.Question.Clear();
                gQuestionareDoc.Article.Clear();
                gQuestionareDoc.Module.Clear();
               
                gModuleAdapter.Fill(gQuestionareDoc.Module);
                gArticleAdapter.Fill(gQuestionareDoc.Article);
                gQuestionAdapter.Fill(gQuestionareDoc.Question);
                gAnswerAdapter.Fill(gQuestionareDoc.Answer);

                MessageBox.Show("Module data updated successfully.");
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonUpdateModule_Click", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show("Update of Module data failed: " + ex.Message);
            }
        }

        private void ButtonUpdateArticle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gArticleAdapter.Update(gQuestionareDoc.Article);
                gQuestionareDoc.Answer.Clear();
                gQuestionareDoc.Question.Clear();
               
                gArticleAdapter.Fill(gQuestionareDoc.Article);
                gQuestionAdapter.Fill(gQuestionareDoc.Question);
                gAnswerAdapter.Fill(gQuestionareDoc.Answer);

                MessageBox.Show("Article data updated successfully.");
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonUpdateArticle_Click", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show("Add article failed: " + ex.Message);
            }
        }

        private void ButtonUpdateQuestion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gQuestionAdapter.Update(gQuestionareDoc.Question);
                gQuestionareDoc.Answer.Clear();
                gQuestionAdapter.Fill(gQuestionareDoc.Question);
                gAnswerAdapter.Fill(gQuestionareDoc.Answer);

                MessageBox.Show("Question data updated successfully.");
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonUpdateQuestion_Click", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show("Add question failed: " + ex.Message);
            }
        }

        private void ButtonUpdateAnswer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;
              
                gAnswerAdapter.Update(gQuestionareDoc.Answer);

                // Consolidate the new answers
                DataRowView lRowView = (DataRowView)gSurveyViewSource.View.CurrentItem;
                CPD.Data.QuestionareDoc.Survey2Row lRow = (QuestionareDoc.Survey2Row)lRowView.Row;

                if (!Consolidate(lRow.SurveyId))
                {
                    MessageBox.Show("There was a problem consolidating the new answers");
                    return;
                }
               
                MessageBox.Show("Answers successfully updated and consolidated for " + lRow.Naam);
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "ButtonUpdateAnswer_Click", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                MessageBox.Show("Add answer failed: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }

        }
    
        private bool Consolidate(int pSurveyId)
        {
            this.Cursor = Cursors.Wait;

            try
            {
                IEnumerable<int> lModuleIds = (IEnumerable<int>)gQuestionareDoc.Module.Where(a => a.SurveyId == pSurveyId).Select(b => b.ModuleId);
                IEnumerable<int> lArticleIds = (IEnumerable<int>)gQuestionareDoc.Article.Where(a => lModuleIds.Contains(a.ModuleId)).Select(b => b.ArticleId);
                IEnumerable<QuestionareDoc.QuestionRow> lQuestions = (IEnumerable<QuestionareDoc.QuestionRow>)gQuestionareDoc.Question.Where(a => lArticleIds.Contains(a.ArticleId)).ToList();

                foreach (QuestionareDoc.QuestionRow lQuestion in lQuestions)
                {
                    IEnumerable<QuestionareDoc.AnswerRow> lHits = (IEnumerable<QuestionareDoc.AnswerRow>)gQuestionareDoc.Answer.Where(a => a.QuestionId == lQuestion.QuestionId);

                    if (lHits.Count() == 0)
                    {
                        // No answer was changed
                        continue;
                    }

                    // Encode the answer
                    int lEncodedAnswer = 0;
                    int i = 0;
                    IEnumerable<QuestionareDoc.AnswerRow> lAnswers = (IEnumerable<QuestionareDoc.AnswerRow>)gQuestionareDoc.Answer.Where(a => a.QuestionId == lQuestion.QuestionId);

                    foreach (QuestionareDoc.AnswerRow lAnswer in lAnswers)
                    {
                        if (lAnswer.Correct)
                        {
                            lEncodedAnswer += (int)Math.Pow(2, i);
                        }
                        i++;
                    }

                    lQuestion.CorrectAnswer = lEncodedAnswer;
                }

                gQuestionAdapter.Update(gQuestionareDoc.Question);

                return true;
            }

            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "Consolidate", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return false;
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

      
    }
}
