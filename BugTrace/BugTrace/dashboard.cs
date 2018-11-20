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
using ICSharpCode.TextEditor.Document;
using MySql.Data.MySqlClient;

namespace BugTrace
{
    public partial class dashboard : Form
    {
        string id;
       public string prid; 
        string[] store = new string[6];
        MySqlConnection connection = new MySqlConnection("server=localhost; database=reporter; username=jonish; password =jonish "); //setting up a profile to establish connection between c# and mysql
        
        public dashboard(string uname, string pword,string type,string id)
        {
            InitializeComponent();

            string file = Application.StartupPath;
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(file))
            {

                fsmp = new FileSyntaxModeProvider(file);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                csol.SetHighlighting("C/C++");

            }

            this.id = id;
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

                    textBox8.Enabled = false;
                    textBox9.Enabled = false;
                    textBox10.Enabled = false;
                    textBox11.Enabled = false;
                    textBox12.Enabled = false;
                    textBox13.Enabled = false;
                    
                }
                reader.NextResult();
            }

            if (type.Equals("PROGRAMMER"))
            {
                // Removes all the tabs: tabControl1.TabPages.Clear(); 
                profile.TabPages.RemoveByKey("useracc"); // Removes all the tabs: tabControl1.TabPages.Clear();
            }
            else if (type.Equals("TESTER"))
            {
                profile.TabPages.RemoveByKey("solution"); //removing solution
                profile.TabPages.RemoveByKey("useracc"); 
            }
            else
            {
                profile.TabPages.RemoveByKey("solution"); //removing solution
            }
            connection.Close();
            account();
            //buglist();

            

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           connection.Open();


            MemoryStream mm = new MemoryStream();
            pimage.Image.Save(mm, pimage.Image.RawFormat);
            byte[] b = mm.ToArray();

            string qry = "insert into product(project_name,line_num_start,line_num_end,class_name,method,issued_date,description,source_file,image) " +
              "values " + "('" + pname.Text + "', '" + pstart.Text + "', '"
              + pend.Text + "','" + pclass.Text + "','" + pmethod.Text + "','" + pdate.Text + "', '" + pdesc.Text + "','" + psource.Text + "','" + b + "')";
            MySqlCommand cmd = new MySqlCommand(qry,connection);


           


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
            connection.Close();
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

                open.Filter = "Choose Image(*.jpg; *.png; *." +"gif)|*.jpg; *.png; *.gif";
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

        private void button2_Click(object sender, EventArgs e)
        {
           


        }

        private void button3_Click(object sender, EventArgs e)
        {

            textBox8.Enabled = true;
            textBox9.Enabled = true;
            textBox10.Enabled = true;
            textBox11.Enabled = true;
            textBox12.Enabled = true;
            textBox13.Enabled = true;

            button3.Visible = false;
            button2.Visible = true;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            connection.Open();
            button3.Visible = true;
            button2.Visible = false;
            string sql = "update register set Name='"+textBox8.Text + "',Email='"+ textBox9.Text+ "',Username='" + textBox10.Text + "',Password='" + textBox11.Text + "',gender='" + textBox12.Text+ "',role='" + textBox13.Text+"' where register_id ='"+id+"'";
            MessageBox.Show("Upadted");
            MySqlCommand cmd = new MySqlCommand(sql,connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            connection.Close();


        }

        public void code()
        {
            string file = Application.StartupPath;
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(file))
            {

                fsmp = new FileSyntaxModeProvider(file);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                pdesc.SetHighlighting("C/C++");

            }
        }

        private void solution_Click(object sender, EventArgs e)
        {

        }

        private void useracc_Click(object sender, EventArgs e)
        {

        }

        public void account()
        {

            connection.Open();
   MySqlCommand cmd = new MySqlCommand("select Name,Email,Username,Gender,Role from register",connection);
            MySqlDataReader data = cmd.ExecuteReader();

            while (data.Read())
            {
                ListViewItem lvt = new ListViewItem(data["Name"].ToString());
                lvt.SubItems.Add(data["Email"].ToString());
                lvt.SubItems.Add(data["Username"].ToString());
                lvt.SubItems.Add(data["Gender"].ToString());
                lvt.SubItems.Add(data["Role"].ToString());
                listView1.Items.Add(lvt);    
            }
            connection.Close();
        }

       

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
