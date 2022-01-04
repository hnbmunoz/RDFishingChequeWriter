using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardCode_ChequeWriter.Utilities
{
    class DataAccess
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        System.Data.DataSet dset = new System.Data.DataSet();
        SqlParameter picture;
        MemoryStream ms = new MemoryStream();


        string cnstring = "";

        public DataTable getDatatable(string cmdtxt, string cnstring, CommandType cmdType, SqlParameter[] param = null)
        {
            try
            {
                if (param != null)
                {
                    foreach (SqlParameter p in param)
                    {
                        if (p == null)
                        {
                            continue;
                        }
                        else
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            SqlConnection connection = new SqlConnection(cnstring);
            string sql = cmdtxt;

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = sql;
                cmd.CommandType = cmdType;

                da.SelectCommand = cmd;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return dt;
        }


        public System.Data.DataSet getDataSet(string cmdtxt)
        {
            SqlConnection connection = new SqlConnection(cnstring);
            string sql = cmdtxt;
            System.Data.DataSet ds = new System.Data.DataSet();


            connection.Open();

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //   DataSet ds = new DataSet();
            da.Fill(ds);


            return ds;
        }


        public void Execute(string cmdtxt, string cnstring, CommandType cmdType, SqlParameter[] param = null)
        {



           try
            {
                if (param != null)
                {
                    foreach (SqlParameter p in param)
                    {
                        if (p == null)
                        {
                            continue;
                        }
                        else
                        {
                            cmd.Parameters.Add(p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            SqlConnection connection = new SqlConnection(cnstring);
            string sql = cmdtxt;
            //
            cmd.Connection = connection;
            cmd.CommandText = sql;
            cmd.CommandType = cmdType;
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();

        }
        public string roleid;
       
        public MemoryStream LoadImage(string cmdtxt)
        {

            SqlConnection connection = new SqlConnection(cnstring);
            connection.Open();
            string sqlQuery = cmdtxt;
            cmd = new SqlCommand(sqlQuery, connection);
            SqlDataReader DataRead = cmd.ExecuteReader();
            DataRead.Read();

            if (DataRead.HasRows)
            {
                byte[] images = (byte[])DataRead[0];


                if (images == null)
                {
                    MessageBox.Show("No Data Found");

                }

                else
                {
                    ms = new MemoryStream(images);


                }



            }


            connection.Close();
            return ms;

            //return images;
        }




        public void SaveDataWithImage(string cmdtxt, string imagelink)
        {
            SqlConnection connection = new SqlConnection(cnstring);

            try
            {


                connection.Open();

                string sqlQuery = cmdtxt;

                byte[] images = null;
                try
                {
                    FileStream Stream = new FileStream(imagelink, FileMode.Open, FileAccess.Read);
                    BinaryReader brs = new BinaryReader(Stream);
                    images = brs.ReadBytes((int)Stream.Length);
                }
                catch { }
                cmd = new SqlCommand(sqlQuery, connection);
                cmd.Parameters.Add(new SqlParameter("@imagedata", images));


                cmd.ExecuteNonQuery();
                connection.Close();
                //  MessageBox.Show("Data Successfully Saved");
            }
            catch
            {
                //   MessageBox.Show(" Data not Saved");

            }



        }



        public SqlDataAdapter getDataAdapter(string cmdtxt)
        {
            SqlConnection connection = new SqlConnection(cnstring);
            string sql = cmdtxt;
            //System.Data.DataSet ds = new System.Data.DataSet();



            connection.Open();

            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);


            return da;
        }



    }
}
