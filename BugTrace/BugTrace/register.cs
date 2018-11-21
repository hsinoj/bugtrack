using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace BugTrace
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void register_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            //setting configuration

            MySqlConnection con = new MySqlConnection("server=localhost;database = reporter;username =jonish;password = jonish"); //setting up a profile to establish connection between c# and mysql
            con.Open();

            //validation if it is empty
            //comparing password and confirm password
            if (rconfirm.Text != rpassword.Text)
            {
                MessageBox.Show("confirm password not matching with new passsword");
                rconfirm.Focus();

            }


            if (rname.Text == string.Empty)
            {
                MessageBox.Show("name is required");
            }
            else if (rmail.Text == string.Empty)
            {
                MessageBox.Show("mail is required");
            }
            else if (rusername.Text == string.Empty)
            {
                MessageBox.Show("username is required");
            }
            else if (rpassword.Text == string.Empty)
            {
                MessageBox.Show("password is required");
            }
            else if (rconfirm.Text == string.Empty)
            {
                MessageBox.Show("password is required");

            }
            else if (rgender.Text == string.Empty)
            {
                MessageBox.Show("gender is required");
            }
            else if (rrole.Text == string.Empty)
            {
                MessageBox.Show("role is required");
            }
            else if (rterms.Text == string.Empty)
            {
                MessageBox.Show("check is required");
            }

            else
            {
                //inserting the data for registration
                string qry = "insert into register(Name,Email,Username,Password,c_password,gender,role,terms) values " + "('" + rname.Text + "', '" + rmail.Text + "', '"
                    + rusername.Text + "','" + rpassword.Text + "','" + rconfirm.Text + "','" + rgender.Text + "', '" + rrole.Text + "','" + rterms.Text + "')";
                MySqlCommand cmd = new MySqlCommand(qry, con);

                /*
                 * try is a type of block statement which may raise exception at a runtime.
                 * Catch is a statement which handles the exception if try block gets error
                */
                try
                {
                    if (cmd.ExecuteNonQuery() == 1) //return the number of row affected
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
            con.Close(); //connection is closed
        }





        //validation


    

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Form1 f = new Form1(); //creating a constructor of class named form
            f.Show();             // showed the specific form 
            Visible = false;     //parent form are closed where as child form are displayed.
        }

        private void rname_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
