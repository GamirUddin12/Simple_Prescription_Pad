using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Simple_prescription
{
    public partial class Fee : Form
    {
        public Fee()
        {
            InitializeComponent();
            con.Open();
            string query = "select * from simple";
            sc = new SqlCommand(query, con);
            SqlDataReader DataRead = sc.ExecuteReader();
            DataRead.Read();

            string came1;


            if (DataRead.HasRows)
            {
                came1 = DataRead["Registration"].ToString();
                regtextBox.Items.Add(came1);




            }

            while (DataRead.Read())
            {
                came1 = DataRead["Registration"].ToString();
                regtextBox.Items.Add(came1);




            }

            //else { MessageBox.Show("This data not available"); }
            con.Close();


        }
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=E:\\Csharp\\Form\\Simple prescription\\Simple prescription\\simple_prescription.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        SqlCommand sc;
        private void viewbutton1_Click(object sender, EventArgs e)
        {
           

            con.Open();
            string sqlQuery = "select Name,Age,Date from simple where Registration= '" + regtextBox.Text + "' ";//rent is table name
            sc = new SqlCommand(sqlQuery, con);
            SqlDataReader DataRead = sc.ExecuteReader();
            DataRead.Read();
            if (DataRead.HasRows)
            {
                nametextBox.Text = DataRead[0].ToString();
                agetextBox.Text = DataRead[1].ToString();
                
                dateTimePicker1.Text = DataRead[2].ToString();
                
                // rxtext.Text = DataRead[5].ToString();

            }

            else { MessageBox.Show("This data not available"); }

            con.Close();

        }

        private void savebutton1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand sc = new SqlCommand("Insert into FEES (Reg,Name,Age,Date,Fee)values('" + regtextBox.Text + "','" + nametextBox.Text + "','" + agetextBox.Text + "','" + dateTimePicker1.Text + "','" + feetextBox.Text + "' ) ", con);
            object o = sc.ExecuteNonQuery();

            MessageBox.Show(o + " : Record has been inserted");
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlDataAdapter sc = new SqlDataAdapter(" SELECT *FROM FEES ", con);
            DataTable Data = new DataTable();
            sc.Fill(Data);
            dataGridView1.DataSource = Data;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.InitialDirectory = "C:";
            saveFileDialog1.Title = "Save as excel file";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Excel File (2007)|*.xls|Excel File(2010)|*.xls";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);

                ExcelApp.Columns.ColumnWidth = 20;

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {

                    ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;


                }


                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {


                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();





                    }



                }



                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();


            }
        }

        private void Fee_Load(object sender, EventArgs e)
        {

        }
    }
}
