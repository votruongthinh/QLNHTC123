using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQuanLyTiecCuoi
{
    public class Tiec
    {
        public int IDDatTiec { get; set; }
        public int IDKH { get; set; }
        public DateTime NgayDatTiec { get; set; }
        public DateTime NgayToChuc { get; set; }
        public int MaCa { get; set; }
        public int IDSanh { get; set; }
        public int TienDatCoc { get; set; }
        public int SoLuongBan { get; set; }
        public List<MonAn> MonAns { get; set; }
        public List<DichVu> DichVus { get; set; }
    }
}
