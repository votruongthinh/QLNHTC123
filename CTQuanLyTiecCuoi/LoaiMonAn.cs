using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQuanLyTiecCuoi
{
    public class LoaiMonAn
    {
        public string MaLoaiMA { get; set; }
        public string TenLoaiMA { get; set; }
        public int tag=0;
        public List<MonAn> dsma = new List<MonAn>();
    }
}
