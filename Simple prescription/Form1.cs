using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Threading;
using System.Threading.Tasks;

namespace Simple_prescription
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(2000);
            t.Abort();
            InitializeComponent();
            con.Open();
            string query = "select * from Medicine";
            sc = new SqlCommand(query, con);
            SqlDataReader DataRead = sc.ExecuteReader();
            DataRead.Read();

            string came1;


            if (DataRead.HasRows)
            {
                came1 = DataRead["Drug"].ToString();
                Drugcombo.Items.Add(came1);
                



            }

            while (DataRead.Read())
            {
                came1 = DataRead["drug"].ToString();
                Drugcombo.Items.Add(came1);
               



            }

            //else { MessageBox.Show("This data not available"); }
            con.Close();
        }

        public void StartForm()
        {
            Application.Run(new Splash_screen());

        }

        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;AttachDbFilename=E:\\Csharp\\Form\\Simple prescription\\Simple prescription\\simple_prescription.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True");
        SqlCommand sc;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exitbutton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you would like to exit?", "Closing Program",
                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           e.Graphics.DrawString("Serial:" + regtext.Text, new Font("Lucida Calligraphy", 10, FontStyle.Bold), Brushes.Black, new Point(730, 20));
            //e.Graphics.DrawString(label1.Text, new Font("Century", 13, FontStyle.Bold), Brushes.DarkGoldenrod, new Point(10, 10));
            //e.Graphics.DrawString(label8.Text, new Font("SutonnyMJ", 10, FontStyle.Regular), Brushes.ForestGreen, new Point(350, 15));
            //e.Graphics.DrawString(label7.Text, new Font("SutonnyMJ", 13, FontStyle.Regular), Brushes.DarkGoldenrod, new Point(600, 15));
            //e.Graphics.DrawString("------------------------------------------------------------", new Font("Arial", 30, FontStyle.Regular), Brushes.ForestGreen, new Point(5, 130));
            e.Graphics.DrawString("Name:"+nametext.Text +"                                                Age:"+agetext.Text +"         Date:"+ dateTimePicker1.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 150));
            //e.Graphics.DrawString("------------------------------------------------------------", new Font("Arial", 30, FontStyle.Regular), Brushes.ForestGreen, new Point(5, 160));
            //e.Graphics.DrawString(complaintext.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(10, 160));
            //e.Graphics.DrawString("Rx", new Font("Lucida Calligraphy", 14, FontStyle.Bold), Brushes.Black, new Point(250, 200));
           

            Rectangle rect2 = new Rectangle(290, 240, 600, 900);

            StringFormat stringFormat1 = new StringFormat();
            stringFormat1.Alignment = StringAlignment.Near;
            stringFormat1.LineAlignment = StringAlignment.Near;

            e.Graphics.DrawString(rxtext.Text, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, rect2, stringFormat1);

            Rectangle rect1 = new Rectangle(10, 200, 200, 900);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Near;

            // Draw the text and the surrounding rectangle.
            e.Graphics.DrawString(complaintext.Text, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, rect1, stringFormat);
            
        }

        private void printbutton_Click(object sender, EventArgs e)
        {
           printDocument1.Print();
            
        }

        private void addbutton_Click(object sender, EventArgs e)// add drug to rx
        {
            string d = prepcombo.Text + Drugcombo.Text + strengthcombo.Text+" "+ Dosecombo.Text + dotcombo.Text + Daycombo.Text + Durationcombo.Text;
            rxtext.Text = rxtext.Text + d + Environment.NewLine + Environment.NewLine;
        }

        private void advicebutton_Click(object sender, EventArgs e)
        {
            string s = Advicecombo.Text;
            rxtext.Text = rxtext.Text + s + Environment.NewLine;
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand sc = new SqlCommand("Insert into simple (Registration,Name,Age,Mobile,Date,History)values('" + regtext.Text + "','" + nametext.Text + "','" + agetext.Text + "' ,'" + mobiletext.Text + "','" + dateTimePicker1.Text + "','" + complaintext.Text + "' ) ", con);
            object o = sc.ExecuteNonQuery();

            MessageBox.Show(o + " : Record has been inserted");
            con.Close();
        }

        private void viewbutton_Click(object sender, EventArgs e)//view
        {
            con.Open();
            string sqlQuery = "select Name,Age,Mobile,Date,History from simple where Registration= '" + regtext.Text + "' ";//rent is table name
            sc = new SqlCommand(sqlQuery, con);
            SqlDataReader DataRead = sc.ExecuteReader();
            DataRead.Read();
            if (DataRead.HasRows)
            {
                nametext.Text = DataRead[0].ToString();
                agetext.Text = DataRead[1].ToString();
                mobiletext.Text = DataRead[2].ToString();
                dateTimePicker1.Text = DataRead[3].ToString();
                complaintext.Text = DataRead[4].ToString();
               // rxtext.Text = DataRead[5].ToString();

            }

            else { MessageBox.Show("This data not available"); }

            con.Close();

        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand sc = new SqlCommand("Insert into Medicine (Drug,Strength)values('" + Drugcombo.Text + "','" + strengthcombo.Text + "' ) ", con);
            object o = sc.ExecuteNonQuery();

            MessageBox.Show(o + " : Record has been inserted");
            con.Close();
        }

        private void Drugcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string sqlQuery = "select Strength from Medicine where Drug= '" + Drugcombo.Text + "' ";
            sc = new SqlCommand(sqlQuery, con);
            SqlDataReader DataRead = sc.ExecuteReader();
            DataRead.Read();
            if (DataRead.HasRows)
            {
                strengthcombo.Text = DataRead[0].ToString();
              

            }

            else { MessageBox.Show("This data not available"); }

            con.Close();

        }

        private void upbutton_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlDataAdapter sc = new SqlDataAdapter("UPDATE Medicine SET Strength='" + strengthcombo.Text + "' where Drug = '" + Drugcombo.Text + "' ", con);
            sc.SelectCommand.ExecuteNonQuery();

            MessageBox.Show(" Record has been Updated");
            con.Close();
        }

        private void feeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fee S = new Fee();
            S.Show();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)//update history
        {
            var result = MessageBox.Show("Are you sure you would like to Update?", "Updating History & Investigation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                con.Open();

                SqlDataAdapter sc = new SqlDataAdapter("UPDATE simple SET Name= '" + nametext.Text + "', Age= '" + agetext.Text + "', Mobile= '" + mobiletext.Text + "', Date= '" + dateTimePicker1.Text + "', History='" + complaintext.Text + "' where Registration = '" + regtext.Text + "' ", con);
                sc.SelectCommand.ExecuteNonQuery();

                MessageBox.Show(" Record has been Updated");
                con.Close();
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            nametext.Clear();
            agetext.Clear();
            mobiletext.Clear();
            rxtext.Clear();
            regtext.Clear();
            complaintext.Clear();
            con.Open();
            string sqlQuery = "select com from complain where id= '" + 1 + "' ";//complain is table name
            sc = new SqlCommand(sqlQuery, con);
            SqlDataReader DataRead = sc.ExecuteReader();
            DataRead.Read();
            if (DataRead.HasRows)
            {
               complaintext.Text = DataRead[0].ToString();
              
                

            }

            else { MessageBox.Show("This data not available"); }

           con.Close();


           
            
        }

     

      
    }
}
