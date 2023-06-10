using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarksApp
{
    public partial class LoginForm : Form
    {
        DataBase database = new DataBase();

        public LoginForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            passField.PasswordChar = '*';
            picpass2.Visible = false;
            loginField.MaxLength = 50;
            passField.MaxLength = 50;
        }

        private void bEntry_Click(object sender, EventArgs e)
        {
            var loginUser = loginField.Text;
            var passUser = passField.Text;
            var userType = "";

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            //string querystring = $"select id_user, login_user, password_user, userType, FIO from register where login_user = '{loginUser}' and password_user = '{passUser}' and userType = '{userType}'";

            //SqlCommand command = new SqlCommand(querystring, database.GetConnection());

            //adapter.SelectCommand = command;
            //adapter.Fill(table);

            //var user = new CheckUser(table.Rows[0].ItemArray[1].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[3]));

            //if (table.Rows.Count == 1)
            //{
            //    CriteriaForm critForm = new CriteriaForm();
            //    critForm.Show();
            //    this.Hide();
            //}

            string query = $"select id_user, login_user, password_user, userType from register where login_user = '{loginUser}' and password_user = '{passUser}'";
            database.openConnection();
            SqlCommand command1 = new SqlCommand(query, database.GetConnection());
            
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.Read())
            {
                loginUser = (string)reader["login_user"];
                passUser = (string)reader["password_user"];
                userType = (string)reader["userType"];
            }


            if (userType == "teacher")
            {
                ListStudents lStudents = new ListStudents();
                lStudents.Show();
                this.Hide();
            }
            else if (userType == "user")
            {
                CriteriaForm critForm = new CriteriaForm();
                critForm.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Такого аккаунта не существует");
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SignForm frm_sign = new SignForm();
            frm_sign.Show();
            this.Hide();
        }

        private void pictpass2_Click(object sender, EventArgs e)
        {
            passField.UseSystemPasswordChar = false;
            picpass2.Visible = false;
            picpass1.Visible = true;
        }

        private void picpass1_Click(object sender, EventArgs e)
        {
            passField.UseSystemPasswordChar = true;
            picpass2.Visible = true;
            picpass1.Visible = false;
        }


        
    }
}
