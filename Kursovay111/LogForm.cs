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

namespace Kursovay111
{
    public partial class LogForm : Form
    {
        DBConnect dbConnection = new DBConnect();

        public LogForm()
        {
            InitializeComponent();
        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = txtName.Text;
            string userPass = txtPassword.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select user_id, user_login, user_password from tableUserProfile where user_login = '{userName}' and user_password = '{userPass}'";

            SqlCommand command = new SqlCommand(querystring, dbConnection.GetConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли!");
                mainForm mainForm = new mainForm();
                this.Hide();
                mainForm.ShowDialog();
                this.Show();

            }
            else
                MessageBox.Show("Неверный логин и/или пароль!");
        }
    }
}
