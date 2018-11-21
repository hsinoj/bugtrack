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
    
    /// <summary>
    /// This is a program about the bug tracking.
    /// It is a console application where bug is tracked and solved by different users.
    /// In this program bug is added by tester and admin. Programmer solved the bug.
    /// </summary>
    public partial class dashboard : Form
    {
        /*
         * intiliazing the variables
         * establishing the connection for the dashboard
         */

        string id;
        public string prid;
        string[] store = new string[6];
        


        MySqlConnection connection = new MySqlConnection("server=localhost; database=reporter; username=jonish; password =jonish "); //setting up a profile to establish connection between c# and mysql

        /// <summary>
        /// getting the username
        /// getting the password 
        /// getting role for associating credentials
        /// to display the profile with the specified id
        /// </summary>
        /// <param name="uname">fetching username for login process from register table</param>
        /// <param name="pword">fetching password for login process from register table</param>
        /// <param name="type">for differentiating the role of different users</param>
        /// <param name="id">To display the profile of specified user</param>
        public dashboard(string uname, string pword, string type, string id)
        {
            InitializeComponent();

            string file = Application.StartupPath;
            FileSyntaxModeProvider fsmp;
            if (Directory.Exists(file))
            {

                fsmp = new FileSyntaxModeProvider(file);
                HighlightingManager.Manager.AddSyntaxModeFileProvider(fsmp);
                pdesc.SetHighlighting("C/C++");

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

            //method been calling
            account();
          //  bugDisplay();
          
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

            if (pname.Text == string.Empty)
            {
                MessageBox.Show("name is required");
            }
            else if (pstart.Text == string.Empty)
            {
                MessageBox.Show("range is required");
            }
            else if (pend.Text == string.Empty)
            {
                MessageBox.Show("range is required");
            }
            else if (pclass.Text == string.Empty)
            {
                MessageBox.Show("class is required");
            }
            else if (pmethod.Text == string.Empty)
            {
                MessageBox.Show("method is required");
            }
            else if (pdate.Text == string.Empty)
            {
                MessageBox.Show("date is required");
            }
            else if (pdesc.Text == string.Empty)
            {
                MessageBox.Show("code is required");
            }
            else if (aname.Text == string.Empty)
            {
                MessageBox.Show("author name is required");
            }
            else if (psource.Text == string.Empty)
            {
                MessageBox.Show("source is required");
            }
          //  else if (pimage.Text == string.Empty)
            //{
             //   MessageBox.Show("image is required");
            //}

            else
            {

              MemoryStream mm = new MemoryStream();
                pimage.Image.Save(mm, pimage.Image.RawFormat);
                byte[] b = mm.ToArray();
               string qry = "insert into product(project_name,line_num_start,line_num_end,class_name,method,issued_date,description,author,source_file,image) " +
                  "values " + "('" + pname.Text + "', '" + pstart.Text + "', '"
                  + pend.Text + "','" + pclass.Text + "','" + pmethod.Text + "','" + pdate.Text + "', '" + pdesc.Text + "','" + aname.Text+"','" + psource.Text + "','" + b + "')";
                MySqlCommand cmd = new MySqlCommand(qry, connection);
/*
                MemoryStream mm = new MemoryStream();
                pimage.Image.Save(mm, pimage.Image.RawFormat);
                byte[] b = mm.ToArray();

       


                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "insert into product(project_name,line_num_start,line_num_end,class_name,method,issued_date,description,author,source_file,image) " +"values (@pname,@pstart,@pend,@pmethod,@pdate,@pdesc,@anme,@psource,@pimage)";
                cmd.Parameters.AddWithValue("@pname",pname); 
                cmd.Parameters.AddWithValue("@pstart",pstart);
                cmd.Parameters.AddWithValue("@pend",pend );   
                cmd.Parameters.AddWithValue("@pclass", pclass);
                cmd.Parameters.AddWithValue("@pmethod", pmethod); 
                cmd.Parameters.AddWithValue("@pdate", pdate);   
                cmd.Parameters.AddWithValue("@pdesc", pdesc); 
                cmd.Parameters.AddWithValue("@anme", aname);    
                cmd.Parameters.AddWithValue("@psource", psource);
                cmd.Parameters.AddWithValue("@pimage", b);
                MessageBox.Show("hello");
                cmd.ExecuteNonQuery();
                */


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

                open.Filter = "Choose Image(*.jpg; *.png; *." + "gif)|*.jpg; *.png; *.gif";
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            connection.Open();
            button3.Visible = true;
            button2.Visible = false;
            string sql = "update register set Name='" + textBox8.Text + "',Email='" + textBox9.Text + "',Username='" + textBox10.Text + "',Password='" + textBox11.Text + "',gender='" + textBox12.Text + "',role='" + textBox13.Text + "' where register_id ='" + id + "'";
            MessageBox.Show("Upadted");
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            connection.Close();


        }

        //adding solution for the bug 


        private void solution_Click(object sender, EventArgs e)
        {

        }

        private void useracc_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// This method is used for displaying data to create a proifle for the users.
        /// ListView class is used to display collection of items.
        /// Listviewitem is list out the items of the class listview
        /// subitems represents listviewitems
        /// </summary>
        public void account()
        {

            connection.Open();
            MySqlCommand cmd = new MySqlCommand("select Name,Email,Username,Gender,Role from register", connection); //selecting column name from database 
            /*
             * MySqlDataReader send the row of datas
             * To create mysqldatareader ,execute reader method must used to send a command to the connection.
             * */

            MySqlDataReader data = cmd.ExecuteReader();

            while (data.Read())
            {
                ListViewItem lvt = new ListViewItem(data["Name"].ToString());
                lvt.SubItems.Add(data["Email"].ToString());
                lvt.SubItems.Add(data["Username"].ToString());
                lvt.SubItems.Add(data["Gender"].ToString());
                lvt.SubItems.Add(data["Role"].ToString());
                listView1.Items.Add(lvt);//adding every rows data in the listview
            }
            connection.Close();//connection is closed
        }

        public void solve()
        {
            connection.Open();
            string qry = "insert into fixer(Name,fixed_date,Project,code)values " + "('" + author.Text + "', '" + date.Text + "','" + prname.Text+"','"+csol.Text+"')";
            MySqlCommand cmd = new MySqlCommand(qry, connection);

            if (cmd.ExecuteNonQuery() == 1) //return the number of row affected
            {
                MessageBox.Show("inserted");
            }
            else
            {
                MessageBox.Show("not inserted");
            }
            connection.Close();
        }

        //displaying bug in the list

    /*   public void bugDisplay()
        {
            
            MySqlCommand cod = new MySqlCommand("select project_name,line_num_start,line_num_end,class_name,method,date_format(issued_date, '%Y-%m-%d'),description,author,source_file,image from product", connection);
 
            MySqlDataAdapter mda = new MySqlDataAdapter(cod);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            connection.Open();
            MySqlDataReader dat = cod.ExecuteReader();
            MessageBox.Show("hello");


            while (dat.Read())
            {
                pname.Text = .Rows[1]["project_name"].ToString();
                pstart.Text = dt.Rows[2]["line_num_start"].ToString();
                pend.Text = dt.Rows[3]["line_num_end"].ToString();
                pclass.Text = dt.Rows[4]["class_name"].ToString();
                pmethod.Text = dt.Rows[5]["method"].ToString();
                pdate.Text = dt.Rows[6]["issued_date"].ToString();
                pdesc.Text = dt.Rows[7]["description"].ToString();
                aname.Text = dt.Rows[8]["author"].ToString();
                psource.Text = dt.Rows[9]["source_file"].ToString();
                byte[] bt = (byte[])dt.Rows[10]["image"];
                MemoryStream m = new MemoryStream(bt);
                pimage.Image = Image.FromStream(m);
            }
            
            connection.Close();
            mda.Dispose();
        }*/


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void author_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {


        }




        private void pdesc_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {





        }

        private void usearch_TextChanged(object sender, EventArgs e)
        {

            
            String search_text = usearch.Text;
            
            
            MySqlCommand command = new MySqlCommand("select * from register where Name like '" + search_text + "%'", connection);
            connection.Open();
            MySqlDataReader daa = command.ExecuteReader();


            listView1.Items.Clear();
            while (daa.Read())
            {

                ListViewItem lvt = new ListViewItem(daa["Name"].ToString());
                lvt.SubItems.Add(daa["Email"].ToString());
                lvt.SubItems.Add(daa["Username"].ToString());
                lvt.SubItems.Add(daa["Gender"].ToString());
                lvt.SubItems.Add(daa["Role"].ToString());
                listView1.Items.Add(lvt);//adding every rows data in the listview
            }
            connection.Close();
        }

        private void psource_TextChanged(object sender, EventArgs e)
        {

        }
     

        private void date_TextChanged(object sender, EventArgs e)
        {

        }
    }

    }

