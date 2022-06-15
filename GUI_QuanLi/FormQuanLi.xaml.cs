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
using System.Data.Linq;
namespace GUI_QuanLi
{
    /// <summary>
    /// Interaction logic for FormQuanLi.xaml
    /// </summary>
    public partial class FormQuanLi : Window
    {
        qlMatHangDataContext db = new qlMatHangDataContext();
        Table<MatHang> matHangs;
        Table<NhaCungCap> nhaCungCaps;
        Table<LoaiHang> loaiHangs;
        public FormQuanLi()
        {
            InitializeComponent();
        }
        public void loadDataMatHang()
        {
            matHangs = db.GetTable<MatHang>();
            var query = from mh in matHangs
                        select mh;
            datamathang.ItemsSource = query;
        }
        public void loadNhaCungCap()
        {
            nhaCungCaps = db.GetTable<NhaCungCap>();
            var query = from ncc in nhaCungCaps
                        select new
                        {
                            MaCT = ncc.MaCongTy,
                            TenCT = ncc.TenCongTy
                        };
            cptencongty.ItemsSource = query;
            cptencongty.DisplayMemberPath = "TenCT";
            cptencongty.SelectedValuePath = "MaCT";
        }
        public void loadLoaiHang()
        {
            loaiHangs = db.GetTable<LoaiHang>();
            var query = from lh in loaiHangs
                        select new
                        {
                            MaLH = lh.MaLoaihang,
                            TenLH = lh.TenLoaihang
                        };
            cptenloaihang.ItemsSource = query;
            cptenloaihang.DisplayMemberPath = "TenLH";
            cptenloaihang.SelectedValuePath = "MaLH";
        }
        public MatHang ktMaHang(string mah)
        {
            matHangs = db.GetTable<MatHang>();
            var query = from mh in matHangs
                        where mh.MaHang == mah
                        select mh;
            foreach (var mh in query)
            {
                if (mh != null)
                {
                    return mh;
                }
            }
            return null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadNhaCungCap();
            loadLoaiHang();
            loadDataMatHang();
            loadDataNCC();
            loadDataLH();
        }

        private void btnmathangthem_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaHang(txtmahang.Text) == null)
            {
                MatHang mh = new MatHang();
                mh.MaHang = txtmahang.Text;
                mh.TenHang = txttenhang.Text;
                mh.MaCongTy = cptencongty.SelectedValue.ToString();
                mh.MaLoaiHang = cptenloaihang.SelectedValue.ToString();
                mh.SoLuong = Convert.ToInt32(txtsoluong.Text);
                mh.GiaHang = Convert.ToDecimal(txtdongia.Text);
                matHangs.InsertOnSubmit(mh);
                db.SubmitChanges();
                MessageBox.Show("Thêm thành công.");
                loadDataMatHang();
            }
            else
            {
                MessageBox.Show("Mã mặt hàng bị trùng");
            }
        }

        private void btnmathangsua_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaHang(txtmahang.Text) != null)
            {
                MatHang mh = new MatHang();
                mh = db.MatHangs.Where(m => m.MaHang == txtmahang.Text).SingleOrDefault();
                mh.MaHang = txtmahang.Text;
                mh.TenHang = txttenhang.Text;
                mh.MaCongTy = cptencongty.SelectedValue.ToString();
                mh.MaLoaiHang = cptenloaihang.SelectedValue.ToString();
                mh.SoLuong = Convert.ToInt32(txtsoluong.Text);
                mh.GiaHang = Convert.ToDecimal(txtdongia.Text);
                db.SubmitChanges();
                loadDataMatHang();
            }
            else
            {
                MessageBox.Show("Mã hàng không tồn tại");
            }
        }

        private void btnmathangxoa_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaHang(txtmahang.Text) != null)
            {
                matHangs = db.GetTable<MatHang>();
                var query = from mh in matHangs
                            where mh.MaHang == txtmahang.Text
                            select mh;
                foreach (var mh in query)
                {
                    if (MessageBox.Show("Bạn có muốn xóa mat hang: " + txtmahang.Text, "Thông báo", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        db.MatHangs.DeleteOnSubmit(mh);
                    }
                    else
                    {

                    }
                }
                db.SubmitChanges();
                loadDataMatHang();
            }
            else
            {
                MessageBox.Show("Mặt hàng này không tồn tại");
            }
        }

        private void btnmathangthongke_Click(object sender, RoutedEventArgs e)
        {
            matHangs = db.GetTable<MatHang>();
            loaiHangs = db.GetTable<LoaiHang>();
            var query = from mh in matHangs
                        from lh in loaiHangs
                        where mh.MaLoaiHang == lh.MaLoaihang
                        group lh by lh.TenLoaihang into b
                        orderby b.Count() descending
                        let TenHang = b.Key
                        let SoLuong = b.Count()
                        select new
                        {
                            TenHang,
                            SoLuong,
                        };
           datamathang.ItemsSource = query;
        }

        private void btntim_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaHang(txtmahangtim.Text) != null)
            {
                matHangs = db.GetTable<MatHang>();
                var query = from mh in matHangs
                            where mh.MaHang == txtmahangtim.Text
                            select mh;
                datamathang.ItemsSource = query;
            }
            else
            {
                MessageBox.Show("Mã hàng không tồn tại");
            }
        }

        private void btnmathanglammoi_Click(object sender, RoutedEventArgs e)
        {
            txtmahang.Clear();
            txttenhang.Clear();
            cptencongty.Text = "";
            cptenloaihang.Text = "";
            txtsoluong.Clear();
            txtdongia.Clear();
            txtmahangtim.Clear();
            txtmahang.Focus();
            loadDataMatHang();
        }
        private void btnmathangthoat_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát ko", "Thông báo", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
        public void loadDataNCC()
        {
            nhaCungCaps = db.GetTable<NhaCungCap>();
            var query = from ncc in nhaCungCaps
                        select ncc;
            datancc.ItemsSource = query;
        }
        // Hàm kiểm tra nhà cung cấp có tồn tại
        public NhaCungCap ktMaCT(string mact)
        {
            nhaCungCaps = db.GetTable<NhaCungCap>();
            var query = from ncc in nhaCungCaps
                        where ncc.MaCongTy == mact
                        select ncc;
            foreach (var ncc in query)
            {
                if (ncc != null)
                {
                    return ncc;
                }
            }
            return null;
        }

        private void btnnccthem_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaCT(txtmacongty.Text) == null)
            {
                NhaCungCap ncc = new NhaCungCap();
                ncc.MaCongTy = txtmacongty.Text;
                ncc.TenCongTy = txttencongty.Text;
                ncc.TenGiaoDich = txttengaodich.Text;
                ncc.DiaChi = txtdiachi.Text;
                ncc.DienThoai = txtdienthoai.Text;
                ncc.Email = txtemail.Text;
                nhaCungCaps.InsertOnSubmit(ncc);
                db.SubmitChanges();
                loadDataNCC();
            }
            else
            {
                MessageBox.Show("Mã công ty bị trùng");
            }
        }

        private void btnnccsua_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaCT(txtmacongty.Text) != null)
            {
                NhaCungCap ncc = new NhaCungCap();
                ncc = db.NhaCungCaps.Where(a => a.MaCongTy == txtmacongty.Text).SingleOrDefault();
                ncc.MaCongTy = txtmacongty.Text;
                ncc.TenCongTy = txttencongty.Text;
                ncc.TenGiaoDich = txttengaodich.Text;
                ncc.DiaChi = txtdiachi.Text;
                ncc.DienThoai = txtdienthoai.Text;
                ncc.Email = txtemail.Text;
                db.SubmitChanges();
                loadDataNCC();
            }
            else
            {
                MessageBox.Show("Mã công ty không tồn tại");
            }
        }

        private void btnnccxoa_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaCT(txtmacongty.Text) != null)
            {
                nhaCungCaps = db.GetTable<NhaCungCap>();
                var query = from ncc in nhaCungCaps
                            where ncc.MaCongTy == txtmacongty.Text
                            select ncc;
                foreach (var ncc in query)
                {
                    if (MessageBox.Show("Bạn có muốn xóa công ty có mã: " + txtmacongty.Text, "Thông báo", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        db.NhaCungCaps.DeleteOnSubmit(ncc);
                    }
                    else
                    {

                    }
                }
                db.SubmitChanges();
                loadDataNCC();
            }
            else
            {
                MessageBox.Show("Mã công ty này không tồn tại");
            }
        }

        private void btnncclammoi_Click(object sender, RoutedEventArgs e)
        {
            txtmacongty.Clear();
            txttencongty.Clear();
            txttengaodich.Clear();
            txtdiachi.Clear();
            txtdienthoai.Clear();
            txtemail.Clear();
            txtdiachitim.Clear();
            txtmacongty.Focus();
            loadDataNCC();
        }

        private void btnncctim_Click(object sender, RoutedEventArgs e)
        {
            bool kt = false;
            string diachi = txtdiachitim.Text;
            nhaCungCaps = db.GetTable<NhaCungCap>();
            var query = from ncc in nhaCungCaps
                        where ncc.DiaChi.Trim() == diachi.Trim()
                        select ncc;
            foreach(var ncc in query)
            {
                if (ncc != null)
                {
                    kt = true;
                }
            }
            if (kt == true)
            {
                datancc.ItemsSource = query;
            }
            else
            {
                MessageBox.Show("Không có công ty nào ở địa chỉ: " + txtdiachitim.Text);
            }
        }
        public void loadDataLH()
        {
            loaiHangs = db.GetTable<LoaiHang>();
            var query = from lh in loaiHangs
                        select lh;
            dataloaihang.ItemsSource = query;
        }
        // Hàm kiểm tra mã loại hàng để xem loại hàng đó có trong bảng
        public LoaiHang ktMaLH(string malh)
        {
            loaiHangs = db.GetTable<LoaiHang>();
            var query = from lh in loaiHangs
                        where lh.MaLoaihang == malh
                        select lh;
            foreach (var lh in query)
            {
                if (lh != null)
                {
                    return lh;
                }
            }
            return null;
        }

        private void btnloaihangthem_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaLH(txtmaloaihang.Text) == null)
            {
                LoaiHang lh = new LoaiHang();
                lh.MaLoaihang = txtmaloaihang.Text;
                lh.TenLoaihang = txttenloaihang.Text;
                loaiHangs.InsertOnSubmit(lh);
                db.SubmitChanges();
                loadDataLH();
            }
            else
            {
                MessageBox.Show("Mã loại hàng bị trùng");
            }
        }

        private void btnloaihangsua_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaLH(txtmaloaihang.Text) != null)
            {
                LoaiHang lh = new LoaiHang();
                lh = db.LoaiHangs.Where(a => a.MaLoaihang == txtmaloaihang.Text).SingleOrDefault();
                lh.MaLoaihang = txtmaloaihang.Text;
                lh.TenLoaihang = txttenloaihang.Text;
                db.SubmitChanges();
                loadDataLH();
            }
            else
            {
                MessageBox.Show("Mã loại hàng không tồn tại");
            }
        }

        private void btnloaihangxoa_Click(object sender, RoutedEventArgs e)
        {
            if (ktMaLH(txtmaloaihang.Text) != null)
            {
                loaiHangs = db.GetTable<LoaiHang>();
                var query = from lh in loaiHangs
                            where lh.MaLoaihang == txtmaloaihang.Text
                            select lh;
                foreach (var lh in query)
                {
                    if (MessageBox.Show("Bạn có muốn xóa loại hàng có mã: " + txtmaloaihang.Text, "Thông báo", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        db.LoaiHangs.DeleteOnSubmit(lh);
                    }
                    else
                    {

                    }
                }
                db.SubmitChanges();
                loadDataLH();
            }
            else
            {
                MessageBox.Show("Mã loại hàng này không tồn tại");
            }
        }

        private void btnloaihanglammoi_Click(object sender, RoutedEventArgs e)
        {
            txtmaloaihang.Clear();
            txttenloaihang.Clear();
            loadDataLH();
        }
    }
}
