using HardCode_ChequeWriter.EGRepository;
using HardCode_ChequeWriter.Models;
using HardCode_ChequeWriter.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace HardCode_ChequeWriter.BusinessLogic
{
    class ChqWriter
    {
        DataTable Maindt = new DataTable();
        DataTable Chqdt = new DataTable();//Exclusive DataTable for Cheque Data

        ExactGlobeRepository EGRepo = new ExactGlobeRepository();

        Utility utilities = new Utility();
        PHPBankPolicy phpPolicy = new PHPBankPolicy();
       

        public ChequeDto getChequeData( string drpDownValue, string chkNumber, string bankpolicy)
        {

           return EGRepo.getChequeData(drpDownValue, chkNumber, bankpolicy);
            
        }
        

    }
}
