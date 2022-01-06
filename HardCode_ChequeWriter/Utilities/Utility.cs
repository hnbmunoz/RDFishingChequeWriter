using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HardCode_ChequeWriter.Utilities
{
    class Utility
    {
        public string Dateinwords(DateTime dateValue)
        {
           string monthCut = "";
           string dayCut = dateValue.Day.ToString();
           string yearCut = dateValue.Year.ToString();

            switch (dateValue.Month.ToString())
            {

                case "01":
                    monthCut = "January";
                    break;
                case "02":
                    monthCut = "February";
                    break;
                case "03":
                    monthCut = "March";
                    break;
                case "04":
                    monthCut = "April";
                    break;
                case "05":
                    monthCut = "May";
                    break;
                case "06":
                    monthCut = "June";
                    break;
                case "07":
                    monthCut = "July";
                    break;
                case "08":
                    monthCut = "August";
                    break;
                case "09":
                    monthCut = "September";
                    break;
                case "10":
                    monthCut = "October";
                    break;
                case "11":
                    monthCut = "November";
                    break;
                default:
                    monthCut = "December";
                    break;

            }
            return monthCut + " " + dayCut + "," + " " + yearCut;
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

        public string datedifference(DateTime dateFrom, DateTime dateTo, string returnCode)
        {
            int yearTotal = 0;
            int monthTotal = 0;
            int dayTotal = 0;

            int yearDateStart = dateFrom.Year;
            int monthDateStart = dateFrom.Month;
            int dayDateStart = dateFrom.Day;

            int yearDateEnd = dateTo.Year;
            int monthDateEnd = dateTo.Month;
            int dayDateEnd = dateTo.Day;

            
            yearTotal = yearDateEnd - yearDateStart;
            monthTotal = monthDateEnd - monthDateStart;
            
            if (monthTotal < 0)
            {
                monthTotal = monthTotal + 12;
                yearTotal = yearTotal - 1;
            }
            else if (monthTotal == 0)
            {
                if (yearDateEnd == yearDateStart)
                {
                    yearTotal = yearTotal;
                }
                else if (yearDateStart > yearDateEnd)
                {
                    yearTotal = yearTotal + 1;
                }
            }
            else
            {
                monthTotal = monthTotal;
            }

            // Computation for Day
            dayTotal = dayDateEnd - dayDateStart;

            if (dayTotal < 0)
            {
                monthTotal = monthTotal - 1;
                if (monthDateStart == 2)
                {
                    if (yearDateStart % 4 == 0 | yearDateEnd % 4 == 0)
                    {
                        dayTotal = dayTotal + 29;
                    }
                }
                else if (monthDateStart == 2)
                {
                    if (yearDateStart % 4 != 0 | yearDateEnd % 4 != 0)
                    {
                        dayTotal = dayTotal + 28;
                    }
                }
                if (monthDateStart == 1 | monthDateStart == 3 | monthDateStart == 5 | monthDateStart == 7 | monthDateStart == 8 | monthDateStart == 10 | monthDateStart == 12)
                {
                    dayTotal = dayTotal + 31;
                }
                else if (monthDateStart == 4 | monthDateStart == 6 | monthDateStart == 9 | monthDateStart == 11)
                {
                    dayTotal = dayTotal + 30;
                }
            }

            if (dayTotal >= 30)
            {
                monthTotal += 1;
                dayTotal = dayTotal - 30;
            }


            if (returnCode == "F")
            {
                return  yearTotal.ToString() + " year/s and " + monthTotal.ToString() + " month/s." + dayTotal.ToString() + " day/s."; // Complete Details of Gap                
            }            
            else if (returnCode == "D")
            {
                return  (dayTotal + (monthTotal * 30) + ((yearTotal * 12) * 30)).ToString(); // Total Gap in Days                
            }
            else if (returnCode == "M")
            {
                return monthTotal.ToString();
            }
            else if (returnCode == "Y")
            {
                return yearTotal.ToString();
            }
            else
            {
                return null;
            }

        }

        public string CurrencyFormat(decimal value)
        {         
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
