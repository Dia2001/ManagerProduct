using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BUS_QuanLi;
using DTO_QuanLi;
namespace GUI_QuanLi
{
    /// <summary>
    /// Interaction logic for FormDangKy.xaml
    /// </summary>
    public partial class FormDangKy : Window
    {
        B_NguoiDung bus = new B_NguoiDung();
        public FormDangKy()
        {
            InitializeComponent();
        }

        private void btncomeback_Click(object sender, RoutedEventArgs e)
        {
            FormLogin f = new FormLogin();
            f.ShowDialog();
        }

        private void btncreate_Click(object sender, RoutedEventArgs e)
        {
            string username = txtusername.Text;
            string pass = pwpass.Password;
            string passconfig = pwConfirmpassword.Password;
            if (username != "" && pass != "" && passconfig != "")
            {
                DO_NguoiDung dt = new DO_NguoiDung(username, pass);
                if (pass.Equals(passconfig))
                {
                    if (bus.loGin(dt))
                    {
                        MessageBox.Show("Tên tài khoản đã tồn tại.");
                        txtusername.Focus();
                        txtusername.SelectAll();
                    }
                    else
                    {
                        if (bus.dangKi(dt))
                        {
                            MessageBox.Show("Đăng kí thành công.");
                        }
                        else
                        {
                            MessageBox.Show("Đăng kí không thành công.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không trùng.");
                    pwConfirmpassword.Focus();
                    pwConfirmpassword.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin vui lòng nhập lại.");
            }
        }
    }
}
