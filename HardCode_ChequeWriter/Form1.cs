using HardCode_ChequeWriter.BusinessLogic;
using HardCode_ChequeWriter.EGRepository;
using HardCode_ChequeWriter.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardCode_ChequeWriter
{

    public partial class Form1 : Form
    {
        public Form1()
        {   //This is Reupload
            InitializeComponent();
            
        }

        DataTable Maindt = new DataTable();
        DataTable Chqdt = new DataTable();//Exclusive DataTable for Cheque Data
        ExactGlobeRepository EGRepo = new ExactGlobeRepository();
        Utility utilities = new Utility();
        PHPBankPolicy policy = new PHPBankPolicy();
        Dto dto = new Dto();

        private void btn_PrintCheque_Click(object sender, EventArgs e)
        {
            qryForCheque(txtChkNum.Text);
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            

        }

        #region ToMakeFormDraggable
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public void MovableFunction(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            MovableFunction(e);
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            MovableFunction(e);
        }
        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            MovableFunction(e);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            MovableFunction(e);
        }


        #endregion


        private void LoadComboBox()
        {
            //Default was set to 051  because of on load function 
            Maindt =  EGRepo.LoadComboBox("051");
            if (Maindt.Rows.Count > 0)
            { 
                drpdowncompany.DataSource = Maindt;
                drpdowncompany.DisplayMember = "name";
                drpdowncompany.ValueMember = "name";
            }
            getCompanyNamebyCode("051");
        }

        private void qryForCheque(string ChkNumber)
        {
            // Sample Parameters
            // 2176545 054  40,633.93 
            // 2176544 054  95,000.00 
            // 2176546 054  17,338.00 5
            // 1115178 051  5,000,000.00

            Chqdt =  EGRepo.qryForCheque(drpdowncompany.SelectedValue.ToString(), ChkNumber);

            if (Chqdt.Rows.Count > 0) 
            {
                string Payee = Chqdt.Rows[0]["OffsetName"].ToString();
                string amt = Chqdt.Rows[0]["AmountDC"].ToString();
                string date = Chqdt.Rows[0]["StatementDate"].ToString();

                dto.chequeAmount = policy.currencyFormat(amt);
                dto.chequeAmountWords = utilities.CurrencyFormat(Convert.ToDecimal(amt));
                dto.chequeDate = policy.dateFormat(date);
                dto.chequePayee = " ** " + Payee + " ** ";

            }
            else
            {
                MessageBox.Show("No Data Found");
            }
        }

        private void getCompanyNamebyCode(string CompCode) 
        {
            if (CompCode == "System.Data.DataRowView")
            {
                return;
            }
           label3.Text = EGRepo.getCompanyNamebyCode(CompCode);
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }

        private void drpdowncompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCompanyNamebyCode(drpdowncompany.SelectedValue.ToString());
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int setter = 250;
            int i = 0;

            e.Graphics.DrawString(dto.chequeDate, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(15, 5 ));
            e.Graphics.DrawString(dto.chequePayee, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(100, 25 ));
            //e.Graphics.DrawString(dto.chequeAmount, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(30, 29));
            //e.Graphics.DrawString(dto.chequeAmountWords, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, new Point(55, 48));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
