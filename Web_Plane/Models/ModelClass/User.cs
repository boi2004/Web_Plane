using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_Plane.Models.ModelClass
{
    public class User
    {
        public int CCCD { get; set; }
        public String Hovaten { get; set; }
        public int Sdt { get; set; }
        public String Email { get; set; }
        public String Diachi { get; set; }
        public int Loaikhachhang { get; set; }
        public DateTime Ngaysinh { get; set; }
        public bool GioiTinh { get; set; }
        public String QuocTich { get; set; }
        public String Password { get; set; }
        public Uri IMG { get; set; }


    }
}