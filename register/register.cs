using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace register
{
    public partial class register : Form
    {
        Class1 dataBase = new Class1();

        public register()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text;
            var password = textBox2.Text;

            string querystrng = $"insert into Users (login_user, password_user) values('{login}' , '{password}')";

            SqlCommand command = new SqlCommand(querystrng, dataBase.getConnection());
            dataBase.openConnection();
            if (checkUser1())
            {
                return;
            }
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан");
               // login frm_login = new ();
                this.Hide();
              //  frm_login.ShowDialog();
                Close();
            }
            else
            {

                MessageBox.Show("Аккаунт не создан");
            }
            dataBase.closeConnection();
        }
        private Boolean checkUser1()
        {
            var loginUser = textBox1.Text;
            var passUser = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select * from Users where login_user = '{loginUser}' and password_user = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());
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
