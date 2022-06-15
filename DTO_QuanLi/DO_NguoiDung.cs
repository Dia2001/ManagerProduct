using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLi
{
    public class DO_NguoiDung
    {
        private string _tendangnhap;
        private string _matkhau;

        public DO_NguoiDung(string tendangnhap, string matkhau)
        {
            this.Tendangnhap = tendangnhap;
            this.Matkhau = matkhau;
        }
        public DO_NguoiDung()
        {

        }
        public string Tendangnhap
        {
            get
            {
                return _tendangnhap;
            }
            set
            {
                _tendangnhap = value;
            }
        }
        public string Matkhau
        {
            get
            {
                return _matkhau;
            }
            set
            {
                _matkhau = value;
            }
        }
    }
}
