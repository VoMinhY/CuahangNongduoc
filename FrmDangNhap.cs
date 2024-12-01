using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CuahangNongduoc
{
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=cuahang.mdb;";
            string query = "SELECT COUNT(1) FROM DANG_NHAP WHERE TEN_DANG_NHAP=@username AND MAT_KHAU=@password";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@username", txtUser.Text);
                command.Parameters.AddWithValue("@password", txtPass.Text);

                connection.Open();
                int result = (int)command.ExecuteScalar();

                if (result == 1)
                {
                    //MessageBox.Show("Đăng nhập thành công!");
                    frmMain formMain = new frmMain();
                    formMain.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tên người dùng hoặc mật khẩu không đúng!", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
