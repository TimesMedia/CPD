using System.Data.SqlClient;
using System;
using CPD.Data;

namespace CPD.Data.ResultDocTableAdapters
{
    partial class ResultTableAdapter
    {
        private SqlConnection gConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {
               
                    gConnection.ConnectionString = Settings.CPDConnectionString;


                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = gConnection;
                }

                this.Adapter.UpdateCommand.Connection = gConnection;
                this.Adapter.InsertCommand.Connection = gConnection;
                this.Adapter.DeleteCommand.Connection = gConnection;
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
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "AttachConnection", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return false;
            }

        }

    }


    partial class ResultDetailTableAdapter
    {
        private SqlConnection gConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {
                
                gConnection.ConnectionString = Settings.CPDConnectionString;
             

                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = gConnection;
                }

                this.Adapter.UpdateCommand.Connection = gConnection;
                this.Adapter.InsertCommand.Connection = gConnection;
                this.Adapter.DeleteCommand.Connection = gConnection;
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
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "AttachConnection", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return false;
            }
        }
    }

    partial class HistoryTableAdapter
    {
        private SqlConnection gConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {

                gConnection.ConnectionString = Settings.CPDConnectionString;

                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = gConnection;
                }

                this.Adapter.UpdateCommand.Connection = gConnection;
                this.Adapter.InsertCommand.Connection = gConnection;
                this.Adapter.DeleteCommand.Connection = gConnection;
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
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "AttachConnection", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return false;
            }
        }
    }


    partial class AnswerTableAdapter
    {
        private SqlConnection gConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {
                gConnection.ConnectionString = Settings.CPDConnectionString;

                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = gConnection;
                }

                this.Adapter.UpdateCommand.Connection = gConnection;
                this.Adapter.InsertCommand.Connection = gConnection;
                this.Adapter.DeleteCommand.Connection = gConnection;
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
                    ExceptionData.WriteException(1, ExceptionLevel.ToString() + " " + CurrentException.Message, this.ToString(), "AttachConnection", "");
                    CurrentException = CurrentException.InnerException;
                } while (CurrentException != null);

                return false;
            }
        }
    }


}





namespace CPD.Data
{


    public partial class ResultDoc
    {
    }
}
