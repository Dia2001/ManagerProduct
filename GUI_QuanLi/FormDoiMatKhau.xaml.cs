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
    /// Interaction logic for FormDoiMatKhau.xaml
    /// </summary>
    public partial class FormDoiMatKhau : Window
    {
        B_NguoiDung bus = new B_NguoiDung();
        public FormDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnchange_Click(object sender, RoutedEventArgs e)
        {
            string tk = txtusername.Text;
            string pass = pwpass.Password;
            string pasmoi = pwCurrentpassword.Password;
            string pasnlai = pwenternewpassword.Password;
            DO_NguoiDung dt = new DO_NguoiDung(tk, pasmoi);
            DO_NguoiDung ktra = new DO_NguoiDung(tk, pass);
            if(tk != "" && pass != "" && pasmoi != "" && pasnlai!=""){
                if (bus.loGin(ktra))
                {
                    if (pasmoi.Equals(pasnlai))
                    {
                        if (bus.doiMatKhau(dt))
                        {
                            MessageBox.Show("Đổi mật khẩu thành công");
                        }
                        else
                        {
                            MessageBox.Show("Đổi mật khẩu không thành công.");
                        }
                    }
                    else
                    {
                        pwenternewpassword.Focus();
                        pwenternewpassword.SelectAll();
                        MessageBox.Show("Mật khẩu mới không trùng vui lòng kiểm tra lại.");
                    }
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
            }
        }

        private void btncomeback_Click(object sender, RoutedEventArgs e)
        {
            FormLogin f = new FormLogin();
            f.ShowDialog();
        }
    }
}
