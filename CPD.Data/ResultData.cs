using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace CPD.Data
{
    public class ResultData
    {
         public static int GetAttempt(int CustomerId, int ModuleId)
         {   
            ResultDocTableAdapters.ResultTableAdapter lResultAdapter = new ResultDocTableAdapters.ResultTableAdapter();
            lResultAdapter.AttachConnection();
             try
             {
                 return (Int32.Parse(lResultAdapter.Attempt(CustomerId, ModuleId).ToString())); 
             }
             catch (Exception ex)
             {
                 if (ex.InnerException == null)
                 {
                     ExceptionData.WriteException(1, ex.Message, "static ResultData", "GetAttempt", "");
                     throw new Exception( "static ResultData" + " : " + "GetAttempt" + " : ", ex);
                 }
                 else
                 {
                     throw ex; // Just bubble it up
                 }
             }
         }


         public static void Quit(int ResultId)
         {   
            ResultDocTableAdapters.ResultTableAdapter lResultAdapter = new ResultDocTableAdapters.ResultTableAdapter();
            lResultAdapter.AttachConnection();
            try
             {

                 lResultAdapter.Quit(ResultId);
             }
             catch (Exception ex)
             {
                 if (ex.InnerException == null)
                 {
                     ExceptionData.WriteException(1, ex.Message, "static ResultData", "Quit", "");
                     throw new Exception("static ResultData" + " : " + "Quit" + " : ", ex);
                 }
                 else
                 {
                     throw ex; // Just bubble it up
                 }
             }
            
         }


        public static bool BuzyWithTest(int CustomerId)
        {
            ResultDoc.HistoryDataTable lHistory = new ResultDoc.HistoryDataTable();
            GetCurrentTest(CustomerId, ref lHistory);
            if (lHistory.Count == 0)
            {
                // You are not buzy with any test
                return false;
            }
            else
            {
                // You are buzy with at least one test
                return true;
            }
        }


        public static bool GetCurrentTest(int CustomerId, ref  ResultDoc.HistoryDataTable pHistory)
        {   
            SqlConnection lConnection = new SqlConnection(global::CPD.Data.Properties.Settings.Default.CPDConnectionString);
            try
            {
                // You are storing state information into the history table, reading from the result table.
                // But the history exist only in memory, not in the database.
                
                SqlCommand Command = new SqlCommand();
                SqlDataAdapter Adaptor = new SqlDataAdapter();
                lConnection.Open();
                Command.Connection = lConnection;
                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "[ResultDoc.History.FillBy]";
                SqlCommandBuilder.DeriveParameters(Command);
                Command.Parameters["@By"].Value = "CurrentTest";
                Command.Parameters["@Id"].Value = CustomerId;
                
                Adaptor.SelectCommand = Command;
                pHistory.Clear();
                Adaptor.Fill(pHistory);
                if (pHistory.Count == 1)
                {
                    return true;
                }
                else
                {
                    //ExceptionData.WriteException(5, "Current test count for customer " + CustomerId.ToString() + " = " + pHistory.Count.ToString(), "static ResultData", "GetCurrentTest", "");
                    return false;
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
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, "static ResultData", "GetCurrentTest", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return false;
            }
            finally
            {
                lConnection.Close();
            }

        }



        public static ResultDoc.HistoryDataTable GetHistory(int CustomerId)
        {
            SqlConnection lConnection = new SqlConnection(global::CPD.Data.Properties.Settings.Default.CPDConnectionString);
            try
            {
                ResultDoc.HistoryDataTable lHistory = new ResultDoc.HistoryDataTable();

                // Load it here


                SqlCommand Command = new SqlCommand();
                SqlDataAdapter Adaptor = new SqlDataAdapter();
                lConnection.Open();
                Command.Connection = lConnection;
                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "[ResultDoc.History.FillBy]";
                SqlCommandBuilder.DeriveParameters(Command);
                Command.Parameters["@By"].Value = "History";
                Command.Parameters["@Id"].Value = CustomerId;

                Adaptor.SelectCommand = Command;
                Adaptor.Fill(lHistory);
                return lHistory;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(1, ex.Message, "static ResultData", "GetHistory", "");
                    throw new Exception("static ResultData" + " : " + "GetHistory" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
            finally
            {
                lConnection.Close();
            }
        }


        public static ResultDoc.HistoryDataTable GetByResultId(int ResultId)
        {
            SqlConnection lConnection = new SqlConnection(global::CPD.Data.Properties.Settings.Default.CPDConnectionString);
            try
            {
                ResultDoc.HistoryDataTable lHistory = new ResultDoc.HistoryDataTable();

                // Load it here


                SqlCommand Command = new SqlCommand();
                SqlDataAdapter Adaptor = new SqlDataAdapter();
                lConnection.Open();
                Command.Connection = lConnection;
                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "[ResultDoc.History.FillBy]";
                SqlCommandBuilder.DeriveParameters(Command);
                Command.Parameters["@By"].Value = "ResultId";
                Command.Parameters["@Id"].Value = ResultId;

                Adaptor.SelectCommand = Command;
                Adaptor.Fill(lHistory);
                return lHistory;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(1, ex.Message, "static ResultData", "GetByResultId", "");
                    throw new Exception("static ResultData" + " : " + "GetByResultId" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
            finally
            {
                lConnection.Close();
            }
        }

        public static int Initialise(int CustomerId, int ModuleId)
        {    
            ResultDocTableAdapters.ResultTableAdapter lResultAdapter = new ResultDocTableAdapters.ResultTableAdapter();
            ResultDocTableAdapters.ResultDetailTableAdapter lResultDetailAdapter = new ResultDocTableAdapters.ResultDetailTableAdapter();
            QuestionareDocTableAdapters.ArticleTableAdapter lArticleAdapter = new QuestionareDocTableAdapters.ArticleTableAdapter();
            QuestionareDocTableAdapters.QuestionTableAdapter lQuestionAdapter = new QuestionareDocTableAdapters.QuestionTableAdapter();
            lResultAdapter.AttachConnection();
            lResultDetailAdapter.AttachConnection();
            lArticleAdapter.AttachConnection();
            lQuestionAdapter.AttachConnection();
            try
            {
                // Local objects

                ResultDoc lResultDoc = new ResultDoc();

                QuestionareDoc lQuestionareDoc = new QuestionareDoc();

                short CurrentAttempt = (short)lResultAdapter.Attempt(CustomerId, ModuleId);

                // Exit if you are too far

                if (CurrentAttempt == 2)
                {
                    throw new Exception("You cannot initialise more than one attempt.");
                }

                // Create a single Result row

                ResultDoc.ResultRow lResultRow = lResultDoc.Result.NewResultRow();
                lResultRow.CustomerId = CustomerId;
                lResultRow.ModuleId = ModuleId;
                lResultRow.Datum = DateTime.Now;
                lResultRow.Attempt = CurrentAttempt;
                lResultRow.Attempt++;
                lResultRow.Pass = false;
                lResultDoc.Result.AddResultRow(lResultRow);
                lResultAdapter.Update(lResultRow);
                lResultDoc.Result.AcceptChanges();


                // Create the child ResultDetail rows
                lArticleAdapter.FillBy(lQuestionareDoc.Article, ModuleId);
                lQuestionAdapter.FillBy(lQuestionareDoc.Question, ModuleId);

                foreach (QuestionareDoc.QuestionRow lQuestionRow in lQuestionareDoc.Question)
                {
                    ResultDoc.ResultDetailRow lResultDetailRow = lResultDoc.ResultDetail.NewResultDetailRow();
                    lResultDetailRow.ResultId = lResultRow.ResultId;
                    lResultDetailRow.QuestionId = lQuestionRow.QuestionId;
                    lResultDetailRow.Datum = DateTime.Now;
                    lResultDetailRow.Answer = 0;
                    lResultDoc.ResultDetail.AddResultDetailRow(lResultDetailRow);
                }

                lResultDetailAdapter.Update(lResultDoc.ResultDetail);
                lResultDoc.ResultDetail.AcceptChanges();

                return lResultRow.ResultId;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(typeof(WarningException) == ex.GetType() ? 3 : 1, ex.Message, "static ResultData", "Initialise", "");
                    throw new Exception("static ResultData" + " : " + "Initialise" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }


        private static int GetCorrectAnswersByQuestion(int QuestionId)
        {
            CPD.Data.ResultDocTableAdapters.AnswerTableAdapter lAnswerAdapter = new ResultDocTableAdapters.AnswerTableAdapter();
            lAnswerAdapter.AttachConnection();
            try
            {
                ResultDoc.AnswerDataTable lAnswer = new ResultDoc.AnswerDataTable();

                lAnswerAdapter.FillBy(lAnswer, "QuestionId", QuestionId);


                if (lAnswer.Count == 0)
                {
                    throw new Exception("There are no answers for question with id = " + QuestionId.ToString());
                }

                int Answer = 0;
                int i = 0;

                foreach (ResultDoc.AnswerRow lAnswerRow in lAnswer)
                {
                    if (!lAnswerRow.IsCorrectNull() && lAnswerRow.Correct)
                    {
                        Answer = Answer + (int)Math.Pow(2, i);
                    }
                    i++;
                }

                return Answer;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(typeof(WarningException) == ex.GetType() ? 3 : 1, ex.Message, "static ResultData", "GetCorrectAnswersByQuestion", "");
                    throw new Exception("static ResultData" + " : " + "GetCorrectAnswersByQuestion" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }


        public static void SetCorrectAnswerBySurvey(int SurveyId)
        {
            QuestionareDocTableAdapters.QuestionTableAdapter lQuestionAdapter = new QuestionareDocTableAdapters.QuestionTableAdapter();
            lQuestionAdapter.AttachConnection();
            try
            {
                //Get all the relevant questions


                QuestionareDoc.QuestionDataTable lQuestion = lQuestionAdapter.GetBy("SurveyId", SurveyId);

                if (lQuestion.Count == 0)
                {
                    throw new Exception("There are no questions for survey with id = " + SurveyId.ToString());
                }

                foreach (QuestionareDoc.QuestionRow lQuestionRow in lQuestion)
                {
                    lQuestionRow.CorrectAnswer = GetCorrectAnswersByQuestion(lQuestionRow.QuestionId);
                }

                lQuestionAdapter.Update(lQuestion);
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(typeof(WarningException) == ex.GetType() ? 3 : 1, ex.Message, "static ResultData", "SetCorrectAnswerBySurvey", "");
                    throw new Exception("static ResultData" + " : " + "SetCorrectAnswerBySurvey" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }


        public static ResultDoc.AnswerDataTable GetAnswer(int CustomerId, int QuestionId)
        {
            ResultDocTableAdapters.AnswerTableAdapter lAnswerAdapter = new ResultDocTableAdapters.AnswerTableAdapter();
            lAnswerAdapter.AttachConnection();
            try
            {
                // Read in the potential answers options

                ResultDoc.AnswerDataTable lAnswer = new ResultDoc.AnswerDataTable();
                ResultDoc.HistoryDataTable lHistory = new ResultDoc.HistoryDataTable();

                lAnswerAdapter.FillBy(lAnswer, "QuestionId", QuestionId);

                // Apply the actual answers and pass that on

                ResultDocTableAdapters.ResultDetailTableAdapter lDetailAdapter = new ResultDocTableAdapters.ResultDetailTableAdapter();

                if (!GetCurrentTest(CustomerId, ref lHistory))
                {
                    return lAnswer;
                }

                int Number = 0;
                try
                {
                    Number = (int)lDetailAdapter.GetAnswer("QuestionId", lHistory[0].ResultId, QuestionId);
                }
                catch
                {

                }

                for (int i = lAnswer.Count - 1; i >= 0; i--)
                {
                    if (Number >= (int)Math.Pow(2, i))
                    {
                        lAnswer[i].Correct = true;
                        Number = Number - (int)Math.Pow(2, i);
                    }
                    else
                    {
                        lAnswer[i].Correct = false;
                    }
                }

                return lAnswer;
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(1, ex.Message, "static ResultData", "GetAnswer", "");
                    throw new Exception("static ResultData" + " : " + "GetAnswer" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
            finally
            {
                lAnswerAdapter.Connection.Close();
            }
        }


        public static void SetAnswer(int CustomerId, int QuestionId, int Answer)
        {
            try
            {
                // Get all the answers

                ResultDoc.HistoryDataTable lHistory = new ResultDoc.HistoryDataTable();
                if (GetCurrentTest(CustomerId, ref lHistory))
                {
                    ResultDocTableAdapters.ResultDetailTableAdapter lDetailAdapter = new ResultDocTableAdapters.ResultDetailTableAdapter();
                    lDetailAdapter.AttachConnection();
                    lDetailAdapter.SetAnswer(lHistory[0].ResultId, QuestionId, Answer);
                    lDetailAdapter.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null)
                {
                    ExceptionData.WriteException(typeof(WarningException) == ex.GetType() ? 3 : 1, ex.Message, "static ResultData", "SetAnswer", "");
                    throw new Exception("static ResultData" + " : " + "SetAnswer" + " : ", ex);
                }
                else
                {
                    throw ex; // Just bubble it up
                }
            }
        }

        public static int Mark(int ResultId)
        {
            ResultDocTableAdapters.ResultTableAdapter lResultAdapter = new ResultDocTableAdapters.ResultTableAdapter();
            lResultAdapter.AttachConnection();

            int Test = (int)lResultAdapter.Mark(ResultId);
            //lResultAdapter.Connection.Close();
            return Test;
        }
     }
}
