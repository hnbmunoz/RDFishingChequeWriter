using System;
using System.Collections.Generic;
using System.Text;

namespace HardCode_ChequeWriter.Utilities
{
    class ConnectionString
    { 
        public  string cstring(string companycode)
        {            
            return "Data Source = EG-Data1\\RDFG; Initial Catalog = " + companycode + "; User ID = hrpro; Password = hrpro";            
            //return "Data Source= LAPTOP-LD99UDNN\\MSSQLSERVER2008R;Initial Catalog= " + companycode + ";User ID=sa;Password=P@ssW0rd";
        }

    }
}
