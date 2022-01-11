using HardCode_ChequeWriter.BusinessLogic;
using HardCode_ChequeWriter.EGRepository;
using HardCode_ChequeWriter.Models;
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
        {
            InitializeComponent();            
        }

        DataTable Maindt = new DataTable();            
        ChqWriter chqwrite = new ChqWriter();//Main Business Logic of Cheque Writer        
        ChequeDto chqPrintModel = new ChequeDto();

        bool print = true;
        string Payee = "";
        int date_X, date_Y, paccount_X, paccount_Y, payee_X, payee_Y, amount_X, amount_Y, words_X, words_Y;
        string bankpolicy = "default";        

        private void btn_PrintCheque_Click(object sender, EventArgs e)
        {
            print = true;           
            qryForCheque(txtChkNum.Text);           
        }

        private void btnQry_Click(object sender, EventArgs e)
        {
            print = false;            
            qryForCheque(txtChkNum.Text); 
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
            Maindt = chqwrite.getComboBox("051");
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

            
            chqPrintModel = chqwrite.getChequeData(drpdowncompany.SelectedValue.ToString(), ChkNumber,bankpolicy);

            if (chqPrintModel.chequePayee != null)
            {

                if (print == true)
                {
                    this.Width = 483;
                    label5.Visible = false;
                    label6.Visible = false;
                    label7.Visible = false;
                    label8.Visible = false;

                    printDialog1.Document = printDocument1;
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
                else
                {
                    label5.Text = "Amount : " + chqPrintModel.chequeAmount;// Proper
                    label6.Text = chqPrintModel.chequeAmountWords;
                    label7.Text = "Payee : " + chqPrintModel.chequePayee;
                    label8.Text = "Date : " + chqPrintModel.chequeDate;

                    this.Width = 834;
                    label5.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    label8.Visible = true;                   

                }

            }
            else
            {
                MessageBox.Show("No Data Found");

                this.Width = 483;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
            }
        }

        private void getCompanyNamebyCode(string CompCode) 
        {
            if (CompCode == "System.Data.DataRowView")
            {
                return;
            }
           label3.Text = chqwrite.getCompanyNamebyCode(CompCode);
        }

        private void defaultChqTemplate()
        {
            date_X = 600;
            date_Y = 35;

            paccount_X = 40;
            paccount_Y = 45;

            payee_X = 150;
            payee_Y = 70;

            amount_X = 600;
            amount_Y = 70;

            words_X = 150;
            words_Y = 95;
            
        }

        private void bdoChqTemplate()
        {
            date_X = 565;
            date_Y = 23;

            paccount_X = 40;
            paccount_Y = 45;

            payee_X = 150;
            payee_Y = 65;

            amount_X = 600;
            amount_Y = 65;

            words_X = 150;
            words_Y = 91;
        }

        private void penbankChqTemplate()
        {
            date_X = 567;
            date_Y = 30;

            paccount_X = 40;
            paccount_Y = 45;

            payee_X = 150;
            payee_Y = 65;

            amount_X = 600;
            amount_Y = 65;

            words_X = 150;
            words_Y = 91;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            radioButton1.Checked = true;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
        }

        private void drpdowncompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCompanyNamebyCode(drpdowncompany.SelectedValue.ToString());
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                defaultChqTemplate();
            }
            if (radioButton2.Checked == true)
            {
                bdoChqTemplate();
            }
            if (radioButton3.Checked == true)
            {
                penbankChqTemplate();
            }

            e.Graphics.DrawString(chqPrintModel.chequeDate, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(date_X, date_Y));
            e.Graphics.DrawString(Payee, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(paccount_X, paccount_Y));
            e.Graphics.DrawString(chqPrintModel.chequePayee, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(payee_X, payee_Y));
            e.Graphics.DrawString(chqPrintModel.chequeAmount, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(amount_X, amount_Y));
            e.Graphics.DrawString(chqPrintModel.chequeAmountWords, new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(words_X, words_Y));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                bankpolicy = "default";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                bankpolicy = "bdo";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                bankpolicy = "penbank";
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                Payee = "For Payee's Account";
            }
            else
            {
                Payee = "";
            }

        }
    }
}
