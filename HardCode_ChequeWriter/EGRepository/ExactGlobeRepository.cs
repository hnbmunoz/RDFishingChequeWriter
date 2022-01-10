using HardCode_ChequeWriter.BusinessLogic;
using HardCode_ChequeWriter.Models;
using HardCode_ChequeWriter.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HardCode_ChequeWriter.EGRepository
{
    class ExactGlobeRepository 
    {
        DataAccess dataaccess = new DataAccess();
        ConnectionString conn = new ConnectionString();
        DataTable maindt = new DataTable();
       
        PHPBankPolicy phpPolicy = new PHPBankPolicy();
        Utility utilities = new Utility();
        ChequeDto dto = new ChequeDto();

       
        public  DataTable LoadComboBox( string drpdownvalue)
        {           
            string query = "SELECT tbl1.name,tbl.name FROM(Select* FROM sys.databases where name like '%X%' or name like '%bak%'  or name like '%crew%') as tbl" +
              " right join" +
              " (SELECT * FROM sys.databases  where name like '%0%' or name like '%1%') as tbl1" +
              " ON tbl.name = tbl1.name" +
              " where tbl.name is  null" +
              " order by tbl1.name ";
            return dataaccess.getDatatable(query, conn.cstring(drpdownvalue), CommandType.Text);

        }
        public String getCompanyNamebyCode(string drpdownvalue)
        {
            
                string query = "Select bedrnm from bedryf";
                string companyname = "";
                maindt = dataaccess.getDatatable(query, conn.cstring(drpdownvalue), CommandType.Text);
                foreach (DataRow dr in maindt.Rows)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    companyname = dr["bedrnm"].ToString();
                }
                return companyname;
           
        }

        public ChequeDto getChequeData(string drpDownValue, string chkNumber, string bankPolicy)
        {
            string query = "SELECT BankTransactions.TransactionNumber, BankTransactions.AmountDC, BankTransactions.OffsetName, " +                         
                         "BankTransactions.ValueDate as DueDate,  BankTransactions.StatementDate,BankTransactions.EntryNumber," +
                         "cicmpy.crdnr,cicmpy.PayeeName" +
                         " FROM BankTransactions BankTransactions (NOLOCK)" +
                         "LEFT JOIN (SELECT payeeName,crdnr FROM cicmpy (NOLOCK)) cicmpy ON cicmpy.crdnr = BankTransactions.CreditorNumber" +
                         " WHERE Ltrim(Rtrim(BankTransactions.TransactionNumber)) = Ltrim(Rtrim('" + chkNumber + "')) AND Ltrim(Rtrim(BankTransactions.PaymentType)) = 'C' " +
                         "AND Ltrim(Rtrim(BankTransactions.PaymentMethod)) = 'C'";

            var result = dataaccess.getDatatable(query, conn.cstring(drpDownValue), CommandType.Text);

            if (result.Rows.Count > 0)
            {
                dto.chequeDate = phpPolicy.chkDateFormat(result.Rows[0]["StatementDate"].ToString(), bankPolicy);
                dto.chequeAmountWords = phpPolicy.chkAmountWordsFormat(result.Rows[0]["AmountDC"].ToString(), bankPolicy);
                dto.chequeAmount = utilities.CurrencyFormat(Convert.ToDecimal(result.Rows[0]["AmountDC"].ToString()));
                dto.chequePayee = " ** " + result.Rows[0]["OffsetName"].ToString() + " ** ";
            }

            return dto;            

        }



    }
}
