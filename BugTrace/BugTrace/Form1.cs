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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BugTrace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
    }
       //initiliazing variables
        public string rol;
        public string uid;

        MySqlConnection conn = new MySqlConnection("server=localhost;database = reporter;username =jonish;password = jonish"); //setting up a profile to establish connection between c# and mysql

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            conn.Open(); //opening connection for user login

            //validation checking whether it is empy or not
          
            if (username.Text == string.Empty)
            {
                MessageBox.Show("username is required");
            }
            else if (password.Text == string.Empty)
            {
                MessageBox.Show("password is required");
            }

            else
            {
                /*executing a select query for
                 * 
                 *
                 *  */
                MySqlCommand com = new MySqlCommand("select Username,Password,role,register_id from register where username ='" + username.Text + "' and password='" + password.Text + "'", conn);
                
                MySqlDataReader rd = com.ExecuteReader();
                while (rd.Read())
                {
                    rol = rd["role"].ToString();
                    uid = rd["register_id"].ToString();
                    
                }
                if (rol.Equals("TESTER"))
                {
                    dashboard d = new dashboard(username.Text, password.Text,"TESTER",uid);
                    d.Show();
                    Visible = false;
                }
                else if (rol.Equals("PROGRAMMER"))
                {
                    dashboard d = new dashboard(username.Text, password.Text,"PROGRAMMER",uid);
                    d.Show();
                    Visible = false;
                }
                else
                {
                    dashboard d = new dashboard(username.Text, password.Text, "ADMIN", uid);
                    d.Show();
                    Visible = false;
                }
            }

           

        }
        

        private void button2_Click(object sender, EventArgs e)
        {
        
            register reg = new register();
            
            reg.Show();
            Visible = false;
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            IWebDriver driver = new ChromeDriver();
            // Launch the ToolsQA WebSite
            driver.Url = ("https://github.com/hsinoj/bugtrack");

            // Type Name in the FirstName text box      
            //driver.FindElement(By.Name("firstname")).SendKeys("Lakshay");
            driver.FindElement(By.Id("login_field")).SendKeys("wraithk94@gmail.com");

            //Type LastName in the LastName text box
            driver.FindElement(By.Id("password")).SendKeys("Jonish1234");

            // Click on the Submit button
            driver.FindElement(By.Name("commit")).Click();
        }
    }
}
