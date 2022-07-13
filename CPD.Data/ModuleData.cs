using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Reflection;
using System.Windows.Resources;

namespace CPD.Data
{
    public static class ModuleData
    {

        public static QuestionareDoc.AvailibleDataTable GetAvailibleModules(int CustomerId, int SurveyId)
        {
            QuestionareDoc.AvailibleDataTable lAvailible = new QuestionareDoc.AvailibleDataTable();

            SqlConnection lConnection = new SqlConnection(Settings.CPDConnectionString);
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adaptor = new SqlDataAdapter();
            lConnection.Open();
            Command.Connection = lConnection;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "[QuestionareDoc.Availible.FillBy]";
            SqlCommandBuilder.DeriveParameters(Command);
            Command.Parameters["@SurveyId"].Value = SurveyId;
            Command.Parameters["@CustomerId"].Value = CustomerId;
            Adaptor.SelectCommand = Command;
            Adaptor.Fill(lAvailible);
            return lAvailible;
        }

        public static QuestionareDoc.SurveyDisplayDataTable GetAvailibleSurveys(int CustomerId)
        {
            QuestionareDoc.SurveyDisplayDataTable lSurvey = new QuestionareDoc.SurveyDisplayDataTable();

            SqlConnection lConnection = new SqlConnection(Settings.CPDConnectionString);
            SqlCommand Command = new SqlCommand();
            SqlDataAdapter Adaptor = new SqlDataAdapter();
            lConnection.Open();
            Command.Connection = lConnection;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandText = "[QuestionareDoc.SurveyDisplay.FillBy]";
            SqlCommandBuilder.DeriveParameters(Command);
            Command.Parameters["@CustomerId"].Value = CustomerId;
            Adaptor.SelectCommand = Command;
            Adaptor.Fill(lSurvey);
            return lSurvey;
        }

        public static int GetIssueId(int ModuleId)
        {
            try
            {
                // Get the corresponding IssueId

                QuestionareDocTableAdapters.ModuleTableAdapter lModuleAdapter = new QuestionareDocTableAdapters.ModuleTableAdapter();
                return (int)lModuleAdapter.GetIssueId(ModuleId);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(1, ex.Message, "static ResultData", "GetIssueId", "");
                    throw new Exception("static ResultData" + " : " + "GetIssueId" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }

        public static string GetQuestions(int pModuleId)
        {

            try
            {
                ModuleDoc lModuleDoc = new ModuleDoc();
                CPD.Data.ModuleDocTableAdapters.ModuleTableAdapter lModuleTableAdapter = new Data.ModuleDocTableAdapters.ModuleTableAdapter();
                CPD.Data.ModuleDocTableAdapters.ArticleTableAdapter lArticleAdapter = new Data.ModuleDocTableAdapters.ArticleTableAdapter();
                CPD.Data.ModuleDocTableAdapters.QuestionTableAdapter lQuestionAdapter = new Data.ModuleDocTableAdapters.QuestionTableAdapter();
                lModuleTableAdapter.FillBy(lModuleDoc.Module, pModuleId);
                lArticleAdapter.FillBy(lModuleDoc.Article, pModuleId);
                lQuestionAdapter.FillBy(lModuleDoc.Question, pModuleId);


                // Get the data into string format
                MemoryStream lMemoryStream1 = new MemoryStream(0);
                MemoryStream lMemoryStream2 = new MemoryStream(0);
                MemoryStream lMemoryStream3 = new MemoryStream(0);

                lModuleDoc.WriteXml(lMemoryStream1, System.Data.XmlWriteMode.IgnoreSchema);

                // Replace the first line
                lMemoryStream1.Position = 0;
                StreamReader lReader = new StreamReader(lMemoryStream1);
                StreamWriter lWriter = new StreamWriter(lMemoryStream2);
                lReader.ReadLine(); // Read the first line.
                lWriter.WriteLine("<ModuleDoc>"); // Write another first line

                while (!lReader.EndOfStream)  // Copy the rest of the lines
                {
                    lWriter.WriteLine(lReader.ReadLine());
                }

                lWriter.Flush();
                return Encoding.UTF8.GetString(lMemoryStream2.GetBuffer(), 0, (int)lMemoryStream2.Length);


            }
            catch (Exception ex)
            {
                //Display all the exceptions

                Exception CurrentException = ex;
                int ExceptionLevel = 0;
                do
                {
                    ExceptionLevel++;
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, "static ModuleData", "GetQuestions", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return "Error in GetQuestions: " + ex.Message;
            }

        }

    }
}
