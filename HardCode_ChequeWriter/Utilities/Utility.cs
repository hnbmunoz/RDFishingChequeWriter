using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HardCode_ChequeWriter.Utilities
{
    class Utility
    {
        public string Dateinwords(DateTime xdate)
        {
            string Monthcut, daycut, yearcut;
            
            daycut = xdate.Day.ToString(); 
            yearcut = xdate.Year.ToString();

            switch (xdate.Month.ToString())
            {

                case "01":
                    Monthcut = "January";
                    break;
                case "02":
                    Monthcut = "February";
                    break;
                case "03":
                    Monthcut = "March";
                    break;
                case "04":
                    Monthcut = "April";
                    break;
                case "05":
                    Monthcut = "May";
                    break;
                case "06":
                    Monthcut = "June";
                    break;
                case "07":
                    Monthcut = "July";
                    break;
                case "08":
                    Monthcut = "August";
                    break;
                case "09":
                    Monthcut = "September";
                    break;
                case "10":
                    Monthcut = "October";
                    break;
                case "11":
                    Monthcut = "November";
                    break;
                default:
                    Monthcut = "December";
                    break;

            }
            return  Monthcut + " " + daycut + "," + " " + yearcut;
        }

        public string NumberToWords(long number)
        {
            if (number == 0)
                return "ZERO";

            if (number < 0)
                return " " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000000) > 0)
            {
                words += NumberToWords(number / 1000000000) + " BILLION ";
                number %= 1000000000;
            }
            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " MILLION ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " HUNDRED ";
                number %= 100;
            }

            if (number > 0)
            {

                var unitsMap = new[] { "ZERO ", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
                var tensMap = new[] { "ZERO ", "TEN ", "TWENTY ", "THIRTY ", "FORTY ", "FIFTY ", "SIXTY ", "SEVENTY ", "EIGHTY ", "NINETY " };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public string datedifference(DateTime datefrom, DateTime dateto, string returnType)
        {
            string diff = " ";

            int ddatestart = 0;
            int ddateend = 0;
            int dmonstart = 0;
            int lengthservice1 = 0;
            int dyearstart = 0;
            int dyearend = 0;
            int dmonend = 0;
            int lengthservice2 = 0;
            int lengthservice3 = 0;

            int yrTot = 0;
            int mnTot = 0;
            int dyTot = 0;

            dyearstart = datefrom.Year;
            dmonstart = datefrom.Month;
            ddatestart = datefrom.Day;

            dyearend = dateto.Year;
            dmonend = dateto.Month;
            ddateend = dateto.Day;


            yrTot = dyearend - dyearstart;
            mnTot = dmonend - dmonstart;

            if (mnTot < 0)
            {
                mnTot = mnTot + 12;
                yrTot = yrTot - 1;
            }
            else if (mnTot == 0)
            {
                if (dyearend == dyearstart)
                {
                    yrTot = yrTot;
                }
                else if (dyearstart > dyearend)
                {
                    yrTot = yrTot + 1;
                }
            }
            else
            {

                mnTot = mnTot;



            }

            // Computation for Day
            dyTot = ddateend - ddatestart;

            if (dyTot < 0)
            {
                mnTot = mnTot - 1;
                if (dmonstart == 2)
                {
                    if (dyearstart % 4 == 0 | dyearend % 4 == 0)
                    {
                        dyTot = dyTot + 29;
                    }
                }
                else if (dmonstart == 2)
                {
                    if (dyearstart % 4 != 0 | dyearend % 4 != 0)
                    {
                        dyTot = dyTot + 28;
                    }
                }
                if (dmonstart == 1 | dmonstart == 3 | dmonstart == 5 | dmonstart == 7 | dmonstart == 8 | dmonstart == 10 | dmonstart == 12)
                {
                    dyTot = dyTot + 31;
                }
                else if (dmonstart == 4 | dmonstart == 6 | dmonstart == 9 | dmonstart == 11)
                {
                    dyTot = dyTot + 30;
                }
            }


            //If dytot
            if (dyTot >= 30)
            {
                mnTot += 1;
                dyTot = dyTot - 30;
            }


            if (returnType == "Full")
            {
                diff = yrTot.ToString() + " year/s and " + mnTot.ToString() + " month/s." + dyTot.ToString() + " day/s."; // Complete Details of Gap
                return diff;
            }
            else if (returnType == "Day")
            {
                int TotalDays = dyTot + (mnTot * 30) + ((yrTot * 12) * 30); // Total Gap in Days
                return TotalDays.ToString();
            }
            else if (returnType == "Month")
            {
                return mnTot.ToString();
            }
            else if (returnType == "Year")
            {
                return yrTot.ToString();
            }
            else
            {
                return null;
            }

        }

        public string CurrencyFormat(decimal value)
        {
            /// 1115178 051
            string converted = "";
            string[] Data = Math.Abs(value).ToString().Split('.');
            int charactercounter = 0;          

            for (int i = Data[0].Length - 1 ; i >= 0; i--)
            {
                if (charactercounter == 3)
                {
                    converted = "," + converted;
                    charactercounter = 0;
                    i++;
                }
                else
                {
                    converted = Data[0][i] + converted;
                    charactercounter += 1;
                }

            }

            if (Data.Length > 1)
            {
                return converted + '.' + Data[1];
            }
            else
            {
                return converted  +".00";
            }
        }

       
    }
}
