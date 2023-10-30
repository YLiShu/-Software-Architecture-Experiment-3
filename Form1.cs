using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;


namespace addressBook
{
    public partial class Form1 : Form
    {
        //数据库连接字符串
        private static string connString = "server=127.0.0.1;port=3306;database=softwaretest3;username=root;password=gh200262...; SslMode=none;";
        MySqlConnection conn = new MySqlConnection(connString);

        private bool isAdd;
        private int Fid;

        public Form1()
        {
            InitializeComponent();

            this.getInfo();
            //try {
            //   conn.Open();
            //    MessageBox.Show("连接成功");
            //} catch (Exception ex){
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void getInfo()
        {
            String sql = "select person_id, person_name, person_phone, person_address from person_list";
            //MessageBox.Show(sql);
            MySqlConnection cnn = new MySqlConnection(connString);
            cnn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, cnn);
            MySqlDataReader reader = cmd.ExecuteReader();
            try
            {
                this.listView1.Items.Clear();
                while (reader.Read())
                {
                    string[] subItems = new string[]{
                   reader.GetInt32(0).ToString(),
                   reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3)};
                    this.listView1.Items.Add(new ListViewItem(subItems));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count == 0)
                return;
            if (this.listView1.FocusedItem == null)
                return;
            ListViewItem item = listView1.SelectedItems[0];
            txtName.Text = item.SubItems[1].Text;
            txtPhone.Text = item.SubItems[2].Text;
            txtAddress.Text = item.SubItems[3].Text;
            this.Fid = int.Parse(item.SubItems[0].Text);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void enableInfo()
        {
            this.clearInfo();
            this.getInfo();
            this.txtAddress.Enabled = true;
            this.txtName.Enabled = true;
        }
        private void clearInfo()
        {
            this.listView1.Items.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.isAdd)
            {
                try
                {
                    addInfo(this.txtName.Text, this.txtPhone.Text, this.txtAddress.Text);
                    MessageBox.Show("成功添加联系人。");
    		        this.getInfo();
                    this.enableInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    changeInfo(Fid, this.txtName.Text, this.txtPhone.Text, this.txtAddress.Text);
                    MessageBox.Show("成功修改联系人信息。");
	                this.getInfo();
                    this.enableInfo();
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.enableInfo();
            //this.buttonEdit.Enabled = false;
            //this.buttonDel.Enabled = false;
            //this.isAdd = true;
            try
            {
                addInfo(this.txtName.Text, this.txtPhone.Text, this.txtAddress.Text);
                MessageBox.Show("成功添加联系人。");
                this.getInfo();
                this.enableInfo();
                this.txtName.Text = "";
                this.txtPhone.Text = "";
                this.txtAddress.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //添加
        public void addInfo(string name, string phone, string address)
        {
            string sql = "insert into person_list(person_name,person_phone,person_address) values("+"'"+name+"'"+","+"'"+phone+"'"+","+"'"+address+"'"+")";

            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //修改
        public void changeInfo(int id, string name, string phone, string address)
        {
            string sql = "update person_list set person_name='" + name + "',person_phone='" + phone + "',person_address = '"+address+"'";
            sql += "where person_id=" + id;
            MySqlConnection conn = new MySqlConnection(connString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private int getSelectID()
        {
            return this.Fid;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                changeInfo(Fid, this.txtName.Text, this.txtPhone.Text, this.txtAddress.Text);
                MessageBox.Show("成功修改联系人信息。");
                this.getInfo();
                this.enableInfo();
                this.txtName.Text = "";
                this.txtPhone.Text = "";
                this.txtAddress.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //删除
        private void button5_Click(object sender, EventArgs e)
        {
            int infoID = this.getSelectID();
            if (infoID == 0 || infoID == null)
            {
                MessageBox.Show("请先选中联系人信息！");
                return;
            }
            try
            {
                string sql = "delete from person_list where person_id=" + infoID.ToString();
                MySqlConnection conn = new MySqlConnection(connString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("删除联系人成功！");
                this.getInfo();
                this.enableInfo();
                this.txtName.Text = "";
                this.txtPhone.Text = "";
                this.txtAddress.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
