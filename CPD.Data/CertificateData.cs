using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CPD.Data
{
    public struct Certificate
    {
        public int CustomerId;
        public string Customer;
        public string CouncilNumber;
        public string Module;
        public string Publication;
        public string AccreditationNumber;
        public int NormalPoints;
        public int? EthicsPoints;
        public string EMailAddress;
        public DateTime Datum;
        public string AccreditationNumber2;
    }

    public class CertificateData
    {

        public static Certificate GetCertificate(int ResultId)
        {   
            SqlConnection lConnection = new SqlConnection(Settings.CPDConnectionString);
            try
            {
                CertificateDoc.CertificateDataTable lCertificate = new CertificateDoc.CertificateDataTable();

                // Get the first part from the CPD database

                SqlCommand Command = new SqlCommand();
                SqlDataAdapter Adaptor = new SqlDataAdapter();
                lConnection.Open();
                Command.Connection = lConnection;
                Command.CommandType = CommandType.StoredProcedure;
                Command.CommandText = "[CertificateData.GetCertificate]";
                SqlCommandBuilder.DeriveParameters(Command);
                Command.Parameters["@ResultId"].Value = ResultId;

                Adaptor.SelectCommand = Command;
                Adaptor.Fill(lCertificate);

                Certificate lCertificateStruct = new Certificate();


                if (lCertificate.Count == 0)
                {
                    return lCertificateStruct;
                }

                var lContext = new MimsDataContext(Settings.MIMSConnectionString);  // This is the live CPD database.

                var lCustomerInfoQuery = from lValues in lContext.MIMS_DataContext_CustomerInfo(lCertificate[0].CustomerId)
                                         select lValues;
                MIMS_DataContext_CustomerInfoResult lCustomerInfo = lCustomerInfoQuery.Single();

                if (lCustomerInfo.FullName.Length != 0)
                {
                    lCertificate[0].Customer = lCustomerInfo.FullName??"NoName";
                    lCertificate[0].CouncilNumber = lCustomerInfo.CouncilNumber ?? "No council number";
                    lCertificate[0].EMailAddress = lCustomerInfo.EmailAddress ?? "NoEmail";
                }
                else
                {
                    throw new Exception("I could not find a customer with id = " + lCertificate[0].ToString());
                }

                // Copy the datatable to a Certificate struct
                            

                lCertificateStruct.CustomerId = lCertificate[0].CustomerId;
                lCertificateStruct.Customer = lCertificate[0].Customer;
                if (!lCertificate[0].IsCouncilNumberNull())
                {
                    lCertificateStruct.CouncilNumber = lCertificate[0].CouncilNumber;
                }

                if (!lCertificate[0].IsModuleNull())
                {
                    lCertificateStruct.Module = lCertificate[0].Module;
                }
                lCertificateStruct.Publication = lCertificate[0].Publication;
                lCertificateStruct.AccreditationNumber = lCertificate[0].AccreditationNumber;
                lCertificateStruct.NormalPoints = lCertificate[0].NormalPoints;
                if (!lCertificate[0].IsEthicsPointsNull() )
                {
                    lCertificateStruct.EthicsPoints = lCertificate[0].EthicsPoints;
                }      
                
                lCertificateStruct.EMailAddress = lCertificate[0].EMailAddress;
                lCertificateStruct.Datum = lCertificate[0].Datum;
                if (!lCertificate[0].IsAccreditationNumber2Null())
                {
                    lCertificateStruct.AccreditationNumber2 = lCertificate[0].AccreditationNumber2;
                }
                
                return lCertificateStruct;
            }
            catch (Exception Ex)
            {
                // Record the event in the Exception table
                ExceptionData.WriteException(1, Ex.Message, "static CertificateData", "GetCertificate", "ResultId = " + ResultId.ToString());
                throw Ex;
            }
            finally
            {
                lConnection.Close();
            }
        }



        public static void RecordEmailSuccess(int pResultId)
        {
           
            try
            {
                var lContext = new CPDDataContext(Settings.CPDConnectionString);  // This is the live MIMS database.

                lContext.ResultDoc_Result_IssueCertificate(pResultId, DateTime.Now);

            }
            catch (Exception Ex)
            {
                // Record the event in the Exception table
                ExceptionData.WriteException(1, Ex.Message, "static CertificateData", "RecordEmailSuccess", "ResultId = " + pResultId.ToString());
                throw Ex;
            }
           
        }






    }
}
