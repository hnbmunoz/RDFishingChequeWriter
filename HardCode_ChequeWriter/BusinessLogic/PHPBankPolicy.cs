﻿using HardCode_ChequeWriter.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HardCode_ChequeWriter.BusinessLogic
{
    class PHPBankPolicy
    {
        Utility utilities = new Utility();

        // Standard PHP Format 
        public string defaultCurrencyFormat(string amount)
        {
            string[] Data = amount.Split('.');

            
            if (Data.Length == 2) // error ?!?
            {
                string thcent = Data[1].ToString();//for centavos like .04
                string finalcent;
                string newcent;
                long number = Convert.ToInt64(Data[0]);
                int cent = Convert.ToInt32(Data[1]);

                int len = thcent.Length;

                if (len == 1)
                {
                    cent = cent * 10;
                    finalcent = Convert.ToString(cent);
                }
                else
                {
                    finalcent = thcent;
                }

                if (Convert.ToInt32(thcent.Substring(0, 1)) == 0)
                {
                    newcent = "0" + Convert.ToString(cent);
                }
                else
                {
                    newcent = Convert.ToString(cent);
                }
                string words = utilities.NumberToWords(number);
                return  "**" + words + ' ' + "AND " + newcent + "/100" + "  ONLY" + "**";
            }
            else
            {
                long number = Convert.ToInt64(Data[0]);
                string words = utilities.NumberToWords(number);
                return "**" + words + "  ONLY" + "**";
            }

        }

        public string defaultDateFormat(string date)
        {
           return utilities.Dateinwords(Convert.ToDateTime(date));
        }
        // Standard PHP Format 

        // Format for specific banks
        public string bdoDateFormat(string date)
        {
            return "";
        }


        public string penbankDateFormat(string date)
        {
            return "";
        }
        //

        public string chkDateFormat(string date, string policy)
        {
            if (policy == "default")
            {
                return this.defaultDateFormat(date);
            }
            else if (policy == "bdo")
            {
                return this.bdoDateFormat(date);
            }
            else if (policy == "penbank")
            {
                return this.penbankDateFormat(date);
            }
            else
            {
                return this.defaultDateFormat(date);
            }
        }

        public string chkAmountWordsFormat(string amount, string policy)
        {
            if (policy == "default")
            {
                return this.defaultCurrencyFormat(amount);
            }
            //else if (policy == "bdo") //Modify Statement incase of additional policy in format
            //{
            //    return this.bdoDateFormat(date);
            //}
            //else if (policy == "penbank")
            //{
            //    return this.penbankDateFormat(date);
            //}
            else
            {
                return this.defaultCurrencyFormat(amount);
            }
        }

    }
}
