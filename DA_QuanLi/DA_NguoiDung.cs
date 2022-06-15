using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTO_QuanLi;
using System.Configuration;
namespace DA_QuanLi
{
    public class DA_NguoiDung: DA_Connect
    {
        public bool dangNhap(DO_NguoiDung dt)
        {
            bool OK = false;
            SqlDataReader rdr = null;
            try
            {
                _coon.Open();
                SqlCommand cmd = new SqlCommand("select * from NguoiDung", _coon);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (dt.Tendangnhap.Trim().Equals(rdr["Tendangnhap"].ToString().Trim()) && dt.Matkhau.Trim().Equals(rdr["Matkhau"].ToString().Trim()))
                    {
                        OK = true;
                    }
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (_coon != null)
                {
                    _coon.Close();
                }
            }
            return OK;
        }
        public bool dangki(DO_NguoiDung dt)
        {
            bool OK = false;
            try
            {
                _coon.Open();
                string sql = string.Format("insert into NguoiDung values('" + dt.Tendangnhap + "','" + dt.Matkhau + "')");
                SqlCommand cmd = new SqlCommand(sql, _coon);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    OK = true;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                _coon.Close();
            }
            return OK;
        }
        public bool doiMatKhau(DO_NguoiDung dt)
        {
            bool OK = false;
            try
            {
                _coon.Open();
                string sql = string.Format("Update NguoiDung set Matkhau='" + dt.Matkhau + "' where Tendangnhap='" + dt.Tendangnhap + "'");
                SqlCommand cmd = new SqlCommand(sql, _coon);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    OK = true;
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                _coon.Close();
            }
            return OK;
        }
    }
}
