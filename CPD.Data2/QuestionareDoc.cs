using System.Data.SqlClient;
using System;
using CPD.Data;

namespace CPD.Data2.QuestionareDocTableAdapters
{




    partial class Survey2TableAdapter
    {
        private SqlConnection lConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {
                // Set the connectionString for this object

                lConnection.ConnectionString = Settings.CPDConnectionString;


                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = lConnection;
                }

                this.Adapter.UpdateCommand.Connection = lConnection;
                this.Adapter.InsertCommand.Connection = lConnection;
                this.Adapter.DeleteCommand.Connection = lConnection;

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


    partial class ModuleTableAdapter
    {
        private SqlConnection lConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {
                // Set the connectionString for this object

                lConnection.ConnectionString = Settings.CPDConnectionString;


                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = lConnection;
                }

                this.Adapter.UpdateCommand.Connection = lConnection;
                this.Adapter.InsertCommand.Connection = lConnection;
                this.Adapter.DeleteCommand.Connection = lConnection;

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




    partial class ArticleTableAdapter
    {
        private SqlConnection lConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {
                // Set the connectionString for this object

                lConnection.ConnectionString = Settings.CPDConnectionString;


                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = lConnection;
                }

                this.Adapter.UpdateCommand.Connection = lConnection;
                this.Adapter.InsertCommand.Connection = lConnection;
                this.Adapter.DeleteCommand.Connection = lConnection;

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




    partial class QuestionTableAdapter
    {
        private SqlConnection lConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {
                // Set the connectionString for this object

                lConnection.ConnectionString = Settings.CPDConnectionString;


                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = lConnection;
                }

                this.Adapter.UpdateCommand.Connection = lConnection;
                this.Adapter.InsertCommand.Connection = lConnection;
                this.Adapter.DeleteCommand.Connection = lConnection;

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
        private SqlConnection lConnection = new SqlConnection();

        public bool AttachConnection()
        {
            try
            {
                // Set the connectionString for this object

                lConnection.ConnectionString = Settings.CPDConnectionString;


                // Replace the designer's connection with yor own one.
                foreach (SqlCommand myCommand in CommandCollection)
                {
                    myCommand.Connection = lConnection;
                }

                this.Adapter.UpdateCommand.Connection = lConnection;
                this.Adapter.InsertCommand.Connection = lConnection;
                this.Adapter.DeleteCommand.Connection = lConnection;

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
namespace CPD.Data2
{


    partial class QuestionareDoc
    {
        partial class ModuleDataTable
        {
        }
    }
}
