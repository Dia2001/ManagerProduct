using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DA_QuanLi
{
   public class DA_Connect
    {
        protected SqlConnection _coon = new SqlConnection(@"Data Source=DESKTOP-M3IMJK3\SQLEXPRESS;Initial Catalog=QLMK;Integrated Security=True");
    }
}
