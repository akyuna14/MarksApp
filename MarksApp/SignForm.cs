using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace MarksApp
{
    public partial class SignForm : Form
    {
        DataBase database = new DataBase();

        public SignForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void SignForm_Load(object sender, EventArgs e)
        {
            passField.PasswordChar = '*';
            picpass1.Visible = false;
            loginField.MaxLength = 50;
            passField.MaxLength = 50;
        }

        private void loginField_TextChanged(object sender, EventArgs e)
        {

        }

        private void picpass1_Click(object sender, EventArgs e)
        {
            passField.UseSystemPasswordChar = true;
            picpass2.Visible = true;
            picpass1.Visible = false;
        }

        private void picpass2_Click(object sender, EventArgs e)
        {
            passField.UseSystemPasswordChar = false;
            picpass2.Visible = false;
            picpass1.Visible = true;
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            var loginUser = loginField.Text;
            var passUser = passField.Text;
            var userType = "";

            string querystring = $"insert into register(login_user, password_user, userType) values(`{loginUser}`, `{passUser}`, 'user')";

            SqlCommand command = new SqlCommand(querystring, database.GetConnection());

            if (command.ExecuteNonQuery() == 1) 
            {
                MessageBox.Show("Аккаунт успешно создан");
                LoginForm frm_login = new LoginForm();
                this.Hide();
                frm_login.Show();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан!");
            }
        }

        private Boolean checkUser()
        {
            var loginUser = loginField.Text;
            var passUser = passField.Text;
            var userType = "";

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select id_user, login_user, password_user, userType from register where login_user=`{loginUser}` and password_user=`{passUser}` and userType=`{userType}`";

            SqlCommand command = new SqlCommand(querystring, database.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
