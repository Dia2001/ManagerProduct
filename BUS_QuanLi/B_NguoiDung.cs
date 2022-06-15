using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLi;
using DA_QuanLi;
namespace BUS_QuanLi
{
    public class B_NguoiDung
    {
        DA_NguoiDung da = new DA_NguoiDung();
        public bool loGin(DO_NguoiDung dt)
        {
            return da.dangNhap(dt);
        }
        public bool dangKi(DO_NguoiDung dt)
        {
            return da.dangki(dt);
        }
        public bool doiMatKhau(DO_NguoiDung dt)
        {
            return da.doiMatKhau(dt);
        }
    }
}
