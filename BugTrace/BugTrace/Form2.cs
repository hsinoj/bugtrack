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
    public partial class Form2 : Form
    {
        int abc;
        string[] store = new string[10];
        MySqlConnection connection = new MySqlConnection("server=localhost; database=reporter; username=jonish; password =jonish "); //setting up a profile to establish connection between c# and mysql
        public Form2(int abc)
        {
            InitializeComponent();
            
            this.abc = abc;
            getBug();
            


             
         }

        public void getBug()
        {

            connection.Open();
            string sql = "select project_name,line_num_start,line_num_end,class_name,method,issued_date,description,author,source_file,image from product where  productct_id = '" + abc + "'";
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            MessageBox.Show(sql);


            while (reader.HasRows)
            {
                while (reader.Read())
                {

                    store[0] = reader["project_name"].ToString();
                    store[1] = reader["line_num_start"].ToString();
                    store[2] = reader["line_num_end"].ToString();
                    store[3] = reader["class_name"].ToString();
                    store[4] = reader["method"].ToString();
                    store[5] = reader["issued_date"].ToString();
                    store[6] = reader["description"].ToString();
                    store[7] = reader["author"].ToString();
                    store[8] = reader["source_file"].ToString();
                    // store[9] = reader["image"].ToString();


                    pname.Text = store[0];
                    pstart.Text = store[1];
                    pend.Text = store[2];
                    pclass.Text = store[3];
                    pmethod.Text = store[4];
                    pdate.Text = store[5];
                    pdesc.Text = store[6];
                    aname.Text = store[7];
                    psource.Text = store[8];

                    byte[] img = (byte[])reader["image"];
                    MemoryStream ms = new MemoryStream(img);
                    pimage.Image = Image.FromStream(ms);
                }
                reader.NextResult();
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost; database=reporter; username=jonish; password =jonish "); //setting up a profile to establish connection between c# and mysql
            connection.Open();

            string sql = "update product set project_name='" + pname.Text + "',line_num_start='" + pstart.Text + "',line_num_end='" + pend.Text + "',class_name='" + pclass.Text + "',method='" + pmethod.Text + "',issued_date='" + pdate.Text + "',description='" + pdesc + "',author='" + aname + "',source_file='" + psource + "',image='" + pimage + "' where productct_id =" + abc + "";
            MessageBox.Show("Upadted");
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            connection.Close();
        }
    }
    }


