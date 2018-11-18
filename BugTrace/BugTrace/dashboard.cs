using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BugTrace
{
    public partial class dashboard : Form
    {
      
        string[] store = new string[6];
        public dashboard(string uname, string pword,string type)
        {
            InitializeComponent();


            MySqlConnection connection = new MySqlConnection("server=localhost; database=reporter; username=jonish; password =jonish "); //setting up a profile to establish connection between c# and mysql
            connection.Open();
            string sql = "select Name,Email,Username,Password,gender,role from register where Username ='" + uname + "' and Password = '" + pword + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

           
            while (reader.HasRows)
            {
                while (reader.Read())
                {

                    store[0] = reader["Name"].ToString();
                    store[1] = reader["Email"].ToString();
                    store[2] = reader["Username"].ToString();
                    store[3] = reader["Password"].ToString();
                    store[4] = reader["gender"].ToString();
                    store[5] = reader["role"].ToString();

                    textBox8.Text = store[0];
                    textBox9.Text = store[1];
                    textBox10.Text = store[2];
                    textBox11.Text = store[3];
                    textBox12.Text = store[4];
                    textBox13.Text = store[5];
                }
                reader.NextResult();
            }

            if (type.Equals("PROGRAMMER"))
            {
                profile.TabPages.RemoveByKey("buggy"); // Removes all the tabs: tabControl1.TabPages.Clear(); 
            }



        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;database = reporter;username =jonish;password = jonish"); //setting up a profile to establish connection between c# and mysql
            con.Open();
            

            MemoryStream mm = new MemoryStream();
            pimage.Image.Save(mm, pimage.Image.RawFormat);
            byte[] b = mm.ToArray();

            string qry = "insert into product(project_name,line_num_start,line_num_end,class_name,method,issued_date,description,source_file,image) " +
              "values " + "('" + pname.Text + "', '" + pstart.Text + "', '"
              + pend.Text + "','" + pclass.Text + "','" + pmethod.Text + "','" + pdate.Text + "', '" + pdesc.Text + "','" + psource.Text + "','" + b + "')";
            MySqlCommand cmd = new MySqlCommand(qry, con);


            string[] ware = new string[9];
            MySqlDataReader devour = cmd.ExecuteReader();
            while (devour.HasRows)
            {
                while (devour.Read())
                {

                    ware[0] = devour["project_name"].ToString();
                    ware[1] = devour["line_num_start"].ToString();
                    ware[2] = devour["line_num_end"].ToString();
                    ware[3] = devour["class_name"].ToString();
                    ware[4] = devour["method"].ToString();
                    ware[5] = devour["issued_date"].ToString();
                    ware[6] = devour["description"].ToString();
                    ware[7] = devour["source_file"].ToString();
                  //  ware[8] = devour["image"].ToString();

                    pname.Text = ware[0];
                    pstart.Text = ware[1];
                    pend.Text = ware[2];
                    pclass.Text = ware[3];
                    pmethod.Text = ware[4];
                    pdate.Text = ware[5];
                    pdesc.Text = ware[6];
                    psource.Text = ware[7];
                  //  pimage.Image =ware[8];


                }
                devour.NextResult();
            }



            try
            {
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("inserted");
                }
                else
                {
                    MessageBox.Show("not inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void buggy_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pimage_Click(object sender, EventArgs e)
        {
            //picture
        }

        private void label9_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();

                open.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    pimage.Image = Image.FromFile(open.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void pmethod_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void history_Click(object sender, EventArgs e)
        {

        }

        private void pdesc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
