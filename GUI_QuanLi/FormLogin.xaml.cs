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
    /// Interaction logic for FormLogin.xaml
    /// </summary>
    public partial class FormLogin : Window
    {
        B_NguoiDung bus = new B_NguoiDung();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnSignin_Click(object sender, RoutedEventArgs e)
        {
            DO_NguoiDung d = new DO_NguoiDung(txtusername.Text, pwpass.Password);
            if (txtusername.Text != "" && pwpass.Password != "")
            {
                if (bus.loGin(d))
                {
                    FormQuanLi f = new FormQuanLi();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng.");
                    pwpass.Focus();
                    pwpass.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
            }
        }
        private void hdoimatkhau_Click(object sender, RoutedEventArgs e)
        {
            FormDoiMatKhau f = new FormDoiMatKhau();
            f.ShowDialog();
        }

        private void btncreate_Click(object sender, RoutedEventArgs e)
        {
            FormDangKy f = new FormDangKy();
            f.ShowDialog();
        }
    }
}
